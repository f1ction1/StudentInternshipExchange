import api from "./axios";

//deprecated, should be refactored
export const IsCompleteEmployerProfile = async (userId) => await api.get(`User/employer/${userId}/is-complete`);

export const IsCompleteProfile = async () => await api.get('User/profile/is-complete');

export const getStudentCv = async (studentId) => await api.get(`User/student/cv/${studentId}`)