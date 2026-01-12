import api from "./axios";

export const getApplicants = async (intrenshipId) => api.get(`orchestration/applicants/${intrenshipId}`);