// Centralized API Client for ProjetoES
// Handles all communication with the backend API

const ApiClient = (() => {
    // Configuration
    const API_BASE_URL = window.location.protocol === 'https:'
        ? 'https://localhost:7266'
        : 'http://localhost:5091';
    const TOKEN_KEYS = ['authToken']; // localStorage/sessionStorage keys to check

    const decodeTokenClaims = (token) => {
        const payload = token?.split('.')[1];
        if (!payload) return null;

        const base64 = payload.replace(/-/g, '+').replace(/_/g, '/');
        const paddedBase64 = base64 + '='.repeat((4 - base64.length % 4) % 4);
        const json = atob(paddedBase64);
        return JSON.parse(json);
    };

    // Get the JWT token from storage (check both localStorage and sessionStorage)
    const getAuthToken = () => {
        for (const key of TOKEN_KEYS) {
            const token = localStorage.getItem(key) || sessionStorage.getItem(key);
            if (token) return token;
        }
        return null;
    };

    // Build request headers with auth token if available
    const getHeaders = (contentType = 'application/json') => {
        const headers = {
            'Content-Type': contentType
        };

        const token = getAuthToken();
        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }

        return headers;
    };

    // Handle API response and errors
    const handleResponse = async (response) => {
        const contentType = response.headers.get('content-type');
        let data = null;

        if (contentType?.includes('application/json')) {
            data = await response.json();
        } else {
            data = await response.text();
        }

        if (!response.ok) {
            const error = new Error(data?.message || data || `HTTP ${response.status}`);
            error.status = response.status;
            error.data = data;
            throw error;
        }

        return data;
    };

    // Generic fetch wrapper
    const request = async (method, endpoint, body = null) => {
        const url = `${API_BASE_URL}${endpoint}`;
        const options = {
            method,
            headers: getHeaders()
        };

        if (body) {
            options.body = JSON.stringify(body);
        }

        const response = await fetch(url, options);
        return handleResponse(response);
    };

    // Public methods
    return {
        // Generic methods
        get: (endpoint) => request('GET', endpoint),
        post: (endpoint, body) => request('POST', endpoint, body),
        put: (endpoint, body) => request('PUT', endpoint, body),
        delete: (endpoint) => request('DELETE', endpoint),

        // Auth methods
        login: (email, password) =>
            request('POST', '/api/auth/login', { email, password }),

        register: (email, password, primeiroNome, ultimoNome) =>
            request('POST', '/api/auth/register', {
                email,
                password,
                primeiroNome,
                ultimoNome
            }),

        // Films methods
        getFilmes: () => request('GET', '/api/filmes'),
        getFilmeById: (id, festivalId = null) => request('GET', festivalId ? `/api/filmes/${id}/festival/${festivalId}` : `/api/filmes/${id}`),
        getFilmesByFestival: (festivalId) => request('GET', `/api/filmes/festival/${festivalId}`),
        searchTmdbMovies: (query) => request('GET', `/api/filmes/tmdb/pesquisa?query=${encodeURIComponent(query)}`),
        getTmdbMovieDetails: (tmdbId) => request('GET', `/api/filmes/tmdb/detalhes/${tmdbId}`),
        createFilme: (filme) => request('POST', '/api/filmes', filme),

        getTrailerTmdb: (tmdbId) => request('GET', `/api/filmes/tmdb/trailer/${tmdbId}`),

        // Cart methods
        getCarts: () => request('GET', '/api/carrinhos'),
        getCartByUser: (userId) => request('GET', `/api/carrinhos/${userId}`),
        createCart: (utilizadorId) =>
            request('POST', '/api/carrinhos', { utilizadorId }),
        addToCart: (carrinhoId, filmeId, quantidade, tipoAcesso, precoUnitario, festivalId) =>
            request('POST', '/api/carrinhos/adicionar-item', {
                carrinhoId,
                filmeId,
                quantidade,
                tipoAcesso,
                precoUnitario,
                festivalId
            }),
        removeFromCart: (itemId) =>
            request('DELETE', `/api/carrinhos/remover-item/${itemId}`),

        // Checkout methods
        createStripeSession: () =>
            request('POST', '/api/checkout/stripe/session'),
        processCheckout: (metodoPagamento) =>
            request('POST', '/api/checkout', { metodoPagamento }),
        getCheckoutHistory: () =>
            request('GET', '/api/checkout/historico'),
        getCheckoutOrder: (id) =>
            request('GET', `/api/checkout/${id}`),

        // Festivals methods
        getFestivais: () => request('GET', '/api/festivais'),
        getFestivaisADecorrer: () => request('GET', '/api/festivais/a-decorrer'),
        getFestivaisProximos: () => request('GET', '/api/festivais/proximos'),
        getFestivaisDisponiveisParaFilmes: () => request('GET', '/api/festivais/disponiveis-para-filmes'),
        getFestivalById: (id) => request('GET', `/api/festivais/${id}`),

        // Utility methods
        isAuthenticated: () => getAuthToken() !== null,
        getToken: () => getAuthToken(),
        setToken: (token, useLocalStorage = true) => {
            const storage = useLocalStorage ? localStorage : sessionStorage;
            storage.setItem('authToken', token);

            // Also decode and persist user id and role for easier client-side checks
            try {
                const claims = decodeTokenClaims(token);
                if (!claims) return;
                const userId = claims.sub ?? claims.nameid ?? claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
                const role = claims.role ?? claims["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ?? null;
                const userName = claims.name ?? claims.unique_name ?? claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] ?? claims.email ?? claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"] ?? null;

                if (userId) storage.setItem('userId', String(userId));
                if (role) storage.setItem('userRole', String(role));
                if (userName) storage.setItem('userName', String(userName));
            } catch (e) {
                // ignore decoding errors
            }
        },
        clearToken: () => {
            localStorage.removeItem('authToken');
            sessionStorage.removeItem('authToken');
            localStorage.removeItem('userId');
            sessionStorage.removeItem('userId');
            localStorage.removeItem('userRole');
            sessionStorage.removeItem('userRole');
            localStorage.removeItem('userName');
            sessionStorage.removeItem('userName');
        },
        getCurrentUserId: () => {
            const token = getAuthToken();
            if (!token) return null;

            const claims = decodeTokenClaims(token);
            if (!claims) return null;
            const userId = claims.sub ?? claims.nameid ?? claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
            const parsed = Number(userId);
            return Number.isFinite(parsed) ? parsed : null;
        },
        getCurrentUserRole: () => {
            const token = getAuthToken();
            if (!token) return null;

            const claims = decodeTokenClaims(token);
            if (!claims) return null;
            return claims.role ?? claims["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ?? null;
        },
        getCurrentUserName: () => {
            const token = getAuthToken();
            if (!token) return null;

            const claims = decodeTokenClaims(token);
            if (!claims) return null;

            return claims.name ?? claims.unique_name ?? claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] ?? claims.email ?? claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"] ?? null;
        },
        renderAuthNavigation: (containerSelector = '.nav-actions') => {
            const container = document.querySelector(containerSelector);
            if (!container) return;

            const isAuthenticated = ApiClient.isAuthenticated();

            if (!isAuthenticated) {
                container.innerHTML = `
                    <i class="fas fa-shopping-cart cart-icon"></i>
                    <a class="btn btn-signin" href="/Login">Sign in</a>
                    <a class="btn btn-register" href="/Register">Register</a>
                `;
                return;
            }

            const userName = ApiClient.getCurrentUserName() || 'Utilizador';
            const role = ApiClient.getCurrentUserRole();
            const adminLink = role === 'Administrador'
                ? '<a class="btn btn-register" href="/AdicionarFilme">Importar Filme</a>'
                : '';

            container.innerHTML = `
                <i class="fas fa-shopping-cart cart-icon"></i>
                ${adminLink}
                <span class="btn btn-signin" style="cursor: default; pointer-events: none;">${userName}</span>
                <button type="button" class="btn btn-register" data-logout-btn>Logout</button>
            `;

            const logoutButton = container.querySelector('[data-logout-btn]');
            if (logoutButton) {
                logoutButton.addEventListener('click', () => {
                    ApiClient.clearToken();
                    window.location.href = '/';
                });
            }
        }
    };
})();

// Export for use in pages
window.ApiClient = ApiClient;
