import api from "./axios";

export const addViewedInteraction = async (internshipId) => api.post(`/interactions/${internshipId}`, JSON.stringify("Viewed"),
  {
    headers: { "Content-Type": "application/json" }
  });

export const getColaborativeRecommendations = async (take) => api.get(`/interactions/test/${take}`);
