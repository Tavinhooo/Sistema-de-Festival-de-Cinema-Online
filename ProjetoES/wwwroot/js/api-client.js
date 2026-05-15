// Centralized API Client for ProjetoES
// Handles all communication with the backend API

const ApiClient = (() => {
  // Configuration
  const API_BASE_URL =
    window.location.protocol === "https:"
      ? "https://localhost:7266"
      : "http://localhost:5091";
  const TOKEN_KEYS = ["authToken"]; // localStorage/sessionStorage keys to check

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
  const getHeaders = (contentType = "application/json") => {
    const headers = {
      "Content-Type": contentType,
    };

    const token = getAuthToken();
    if (token) {
      headers["Authorization"] = `Bearer ${token}`;
    }

    return headers;
  };

  // Handle API response and errors
  const handleResponse = async (response) => {
    const contentType = response.headers.get("content-type");
    let data = null;

    if (contentType?.includes("application/json")) {
      data = await response.json();
    } else {
      data = await response.text();
    }

    if (!response.ok) {
      const error = new Error(
        data?.message || data || `HTTP ${response.status}`,
      );
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
      headers: getHeaders(),
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
    get: (endpoint) => request("GET", endpoint),
    post: (endpoint, body) => request("POST", endpoint, body),
    put: (endpoint, body) => request("PUT", endpoint, body),
    delete: (endpoint) => request("DELETE", endpoint),

    // Auth methods
    login: (email, password) =>
      request("POST", "/api/auth/login", { email, password }),

    register: (email, password, primeiroNome, ultimoNome) =>
      request("POST", "/api/auth/register", {
        email,
        password,
        primeiroNome,
        ultimoNome,
      }),

    // Films methods
    getFilmes: () => request("GET", "/api/filmes"),
    getFilmeById: (id, festivalId = null) =>
      request(
        "GET",
        festivalId
          ? `/api/filmes/${id}/festival/${festivalId}`
          : `/api/filmes/${id}`,
      ),
    getFilmesByFestival: (festivalId) =>
      request("GET", `/api/filmes/festival/${festivalId}`),
    searchTmdbMovies: (query) =>
      request(
        "GET",
        `/api/filmes/tmdb/pesquisa?query=${encodeURIComponent(query)}`,
      ),
    getTmdbMovieDetails: (tmdbId) =>
      request("GET", `/api/filmes/tmdb/detalhes/${tmdbId}`),
    createFilme: (filme) => request("POST", "/api/filmes", filme),

    getCurrentUserRole: () => {
      const token = getAuthToken();
      if (!token) return null;
      const claims = decodeTokenClaims(token);
      if (!claims) return null;
      return claims.role ?? claims["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ?? null;
    },

    logout: () => {
      localStorage.removeItem("authToken");
      sessionStorage.removeItem("authToken");
      window.location.href = "/Login";
    },

    renderAuthNavigation: (containerId) => {
      const container = document.getElementById(containerId);
      if (!container) return;

      const token = getAuthToken();
      if (!token) {
        container.innerHTML = `
      <a class="btn btn-login" href="/Login">Login</a>
      <a class="btn btn-register" href="/Register">Registar</a>
    `;
        return;
      }

      const claims = decodeTokenClaims(token);
      const role = claims?.role ?? null;
      const name = claims?.name ?? claims?.unique_name ?? "Utilizador";

      const adminLink = role === "Administrador"
        ? '<a class="btn btn-register" href="/AdminPanel">Painel Admin</a>'
        : "";

      container.innerHTML = `
    ${adminLink}
    <span class="btn btn-login" style="cursor:default;">${name}</span>
    <button class="btn btn-register" onclick="ApiClient.logout()">Logout</button>
  `;
    },

    // Cart methods
    getCarts: () => request("GET", "/api/carrinhos"),
    getCartByUser: (userId) => request("GET", `/api/carrinhos/${userId}`),
    createCart: (utilizadorId) =>
      request("POST", "/api/carrinhos", { utilizadorId }),
    addToCart: (
      carrinhoId,
      filmeId,
      quantidade,
      tipoAcesso,
      precoUnitario,
      festivalId,
    ) =>
      request("POST", "/api/carrinhos/adicionar-item", {
        carrinhoId,
        filmeId,
        quantidade,
        tipoAcesso,
        precoUnitario,
        festivalId,
      }),
    removeFromCart: (itemId) =>
      request("DELETE", `/api/carrinhos/remover-item/${itemId}`),

    // Checkout methods
    createStripeSession: () => request("POST", "/api/checkout/stripe/session"),
    processCheckout: (metodoPagamento) =>
      request("POST", "/api/checkout", { metodoPagamento }),
    getCheckoutHistory: () => request("GET", "/api/checkout/historico"),
    getCheckoutOrder: (id) => request("GET", `/api/checkout/${id}`),

    // Festivals methods
    getFestivais: () => request("GET", "/api/festivais"),
    getFestivalById: (id) => request("GET", `/api/festivais/${id}`),
    filtrarFestivais: (
      nome = null,
      dataInicio = null,
      dataFim = null,
      local = null,
    ) => {
      const params = new URLSearchParams();
      if (nome) params.append("nome", nome);
      if (dataInicio) params.append("dataInicio", dataInicio);
      if (dataFim) params.append("dataFim", dataFim);
      if (local) params.append("local", local);
      const queryString = params.toString();
      return request(
        "GET",
        `/api/festivais${queryString ? "?" + queryString : ""}`,
      );
    },
    getFestivaisADecorrer: () => request("GET", "/api/festivais/a-decorrer"),
    getFestivaisProximos: () => request("GET", "/api/festivais/proximos"),
    getFestivaisDisponiveisParaFilmes: () =>
      request("GET", "/api/festivais/disponiveis-para-filmes"),

    // Utility methods
    isAuthenticated: () => getAuthToken() !== null,
    getToken: () => getAuthToken(),
    setToken: (token, useLocalStorage = true) => {
      const storage = useLocalStorage ? localStorage : sessionStorage;
      storage.setItem("authToken", token);
    },
    clearToken: () => {
      localStorage.removeItem("authToken");
      sessionStorage.removeItem("authToken");
    },
    getCurrentUserId: () => {
      const token = getAuthToken();
      if (!token) return null;

      const claims = decodeTokenClaims(token);
      if (!claims) return null;

      const userId = claims.sub ?? claims.nameid ?? claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
      const parsed = Number(userId);
      return Number.isFinite(parsed) ? parsed : null;
    }
  };
})();

// Export for use in pages
window.ApiClient = ApiClient;