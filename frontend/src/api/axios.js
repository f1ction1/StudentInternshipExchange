import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7244/api",
  withCredentials: true, 
});

// Перехоплення токенів, якщо треба
api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

export default api;