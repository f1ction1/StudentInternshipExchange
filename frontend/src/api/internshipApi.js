import api from "./axios";

const EMPLOYER_INTERNSHIP_URL = '/employer';

export const getInternshipDictionaries = () => api.get("/internship-dictionaries"); 

// EMPLOYER
export const addInternship = (data) => api.post(EMPLOYER_INTERNSHIP_URL + "/internships/add", data);

export const updateInternship = (data) => api.patch(EMPLOYER_INTERNSHIP_URL + "/internships/edit", data);

export const getEmployerInternships = () => api.get(EMPLOYER_INTERNSHIP_URL + "/internships");

export const getEmployerIntershipInfo = (id) => api.get(EMPLOYER_INTERNSHIP_URL + `/internships/${id}/`)

// Internships

export const getInternshipsList = (queryParams) => api.get("/internships?" + queryParams)

export const getInternshipDetails = (internshipId) => api.get("/internships/" + internshipId)

export const addInternshipToFavorite = (internshipId) => api.post(`internships/${internshipId}/favorite`);

export const removeInternshipFromFavorite = (internshipId) => api.delete(`internships/${internshipId}/favorite`);

export const getLikedInternships = async () => api.get('/internships/favorites');