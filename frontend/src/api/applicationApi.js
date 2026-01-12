import api from "./axios";

export const applyInternship = async (internshipData) => api.post("/student/applications", internshipData);
export const getStudentApplications = async () => api.get("/student/applications");
export const getApplicationDetail = async (id) => api.get(`/student/applications/${id}`);
export const makeReview = async (applicationId, reviewNotes) => api.post(`/student/applications/${applicationId}/review`, JSON.stringify(reviewNotes),
  {
    headers: { "Content-Type": "application/json" }
  });
export const rejectApplication = async (applicationId, rejectionReason) => api.post(`/student/applications/${applicationId}/reject`, JSON.stringify(rejectionReason),
  {
    headers: { "Content-Type": "application/json" }
  });