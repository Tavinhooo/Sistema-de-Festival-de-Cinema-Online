// Centralized API Client for ProjetoES
// Handles all communication with the backend API

const ApiClient = (() => {
    // Configuration
    const API_BASE_URL = 'http://localhost:5091';
    const TOKEN_KEYS = ['authToken']; // localStorage/sessionStorage keys to check

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
        getFilmeById: (id) => request('GET', `/api/filmes/${id}`),

        // Cart methods
        getCarts: () => request('GET', '/api/carrinhos'),
        getCartByUser: (userId) => request('GET', `/api/carrinhos/${userId}`),
        createCart: (utilizadorId) =>
            request('POST', '/api/carrinhos', { utilizadorId }),
        addToCart: (carrinhoId, filmeId, quantidade, tipoAcesso, precoUnitario) =>
            request('POST', '/api/carrinhos/adicionar-item', {
                carrinhoId,
                filmeId,
                quantidade,
                tipoAcesso,
                precoUnitario
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
        getFestivalById: (id) => request('GET', `/api/festivais/${id}`),

        // Utility methods
        isAuthenticated: () => getAuthToken() !== null,
        getToken: () => getAuthToken(),
        setToken: (token, useLocalStorage = true) => {
            const storage = useLocalStorage ? localStorage : sessionStorage;
            storage.setItem('authToken', token);
        },
        clearToken: () => {
            localStorage.removeItem('authToken');
            sessionStorage.removeItem('authToken');
        }
    };
})();

// Export for use in pages
window.ApiClient = ApiClient;
