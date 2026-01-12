import { useState } from "react";
import { useNavigate, useLoaderData } from "react-router-dom";
import styles from "./ApplicantsList.module.css";
import { getApplicants } from "../../../api/orchestrationApi";
import { getStudentCv } from "../../../api/userApi";
// Додаємо rejectApplication до імпорту
import { makeReview, rejectApplication } from "../../../api/applicationApi"; 

const statusConfig = {
    0: { label: "New", color: "#0d6efd", icon: "bi-file-earmark-text" },
    1: { label: "Reviewed", color: "#198754", icon: "bi-check-circle" },
    // Додаємо статус Rejected
    2: { label: "Rejected", color: "#dc3545", icon: "bi-x-circle" }
};

export async function loader( {params} ) {
    try {
        const intrenshipId = params.internshipId;
        const response = await getApplicants(intrenshipId);
        return {applicants: response.data};
    } catch(err) {
        const message = err.response?.data?.name;
        const code = err.response?.data?.code;
        if(code && code === "Applications.NotFound") {
            return {message, code}
        }
        
        throw "Something goes wrong while tried to load Applicants"
    }
}

export default function ApplicantsList() {
    const data = useLoaderData();
    const navigate = useNavigate();
    
    const [selectedApplicant, setSelectedApplicant] = useState(null);
    const [filterStatus, setFilterStatus] = useState("all");
    const [searchTerm, setSearchTerm] = useState("");
    const [showCvViewer, setShowCvViewer] = useState(false);
    
    // States for Review
    const [reviewNotes, setReviewNotes] = useState("");
    const [isSavingNotes, setIsSavingNotes] = useState(false);
    
    // States for CV
    const [cvData, setCvData] = useState(null);
    const [isLoadingCv, setIsLoadingCv] = useState(false);

    // States for Rejection (NEW)
    const [showRejectModal, setShowRejectModal] = useState(false);
    const [rejectionReason, setRejectionReason] = useState("");
    const [isRejecting, setIsRejecting] = useState(false);

    if(data.code && data.code === "Applications.NotFound") {
        return (
            // ... (Код Empty State залишається без змін) ...
            <div className={styles.container}>
                <div className={styles.header}>
                    <button className={styles.backBtn} onClick={() => navigate('/employer/internships')}>
                        <i className="bi bi-arrow-left me-2"></i> Back
                    </button>
                </div>
                <div className={styles.emptyState}>
                    <h2>No Applications Yet</h2>
                </div>
            </div>
        );
    }

    const applications = data.applicants.applications;
    const internship = data.applicants.internshipInfo;

    const filteredApplicants = applications.filter(applicant => {
        const matchesStatus = filterStatus === "all" || applicant.status == filterStatus;
        const matchesSearch = 
            applicant.firstName.toLowerCase().includes(searchTerm.toLowerCase()) ||
            applicant.lastName.toLowerCase().includes(searchTerm.toLowerCase()) ||
            applicant.email.toLowerCase().includes(searchTerm.toLowerCase()) ||
            applicant.university.toLowerCase().includes(searchTerm.toLowerCase());
        return matchesStatus && matchesSearch;
    });

    // --- Handlers ---

    const handleMarkAsReviewed = async () => {
        if (!selectedApplicant || selectedApplicant.status === 1) return;
        
        setIsSavingNotes(true);
        try {
            let rN = reviewNotes === "" ? "No review notes" : reviewNotes;
            await makeReview(selectedApplicant.applicationId, rN);
            
            // Оновлюємо локальний стан без перезавантаження
            selectedApplicant.status = 1;
            selectedApplicant.reviewNotes = rN;
            // Примушуємо React перерендерити список (хоча мутація об'єкта не ідеальна практика, для простоти тут працює)
            setSelectedApplicant({...selectedApplicant}); 
        } catch (error) {
            console.error("Error marking as reviewed:", error);
        } finally {
            setIsSavingNotes(false);
        }
    };

    // --- NEW: Reject Logic ---
    const openRejectModal = () => {
        setRejectionReason(""); // Скидаємо поле при відкритті
        setShowRejectModal(true);
    };

    const handleConfirmReject = async () => {
        if (!selectedApplicant) return;

        setIsRejecting(true);
        try {
            // Викликаємо API
            await rejectApplication(selectedApplicant.applicationId, rejectionReason);
            
            // Оновлюємо локальний стан
            selectedApplicant.status = 2; // Rejected
            selectedApplicant.rejectionReason = rejectionReason;
            
            // Закриваємо модалку
            setShowRejectModal(false);
            // Оновлюємо UI
            setSelectedApplicant({...selectedApplicant});
        } catch (error) {
            console.error("Error rejecting applicant:", error);
            alert("Failed to reject applicant");
        } finally {
            setIsRejecting(false);
        }
    };

    const handleOpenCv = async () => {
        if (!selectedApplicant) return;
        setIsLoadingCv(true);
        setShowCvViewer(true);
        try {
            const response = await getStudentCv(selectedApplicant.userId);
            setCvData(response.data.cvFile);
        } catch (error) {
            console.error("Error loading CV:", error);
            alert("Failed to load CV.");
            setShowCvViewer(false);
        } finally {
            setIsLoadingCv(false);
        }
    };

    const handleCloseCvViewer = () => {
        setShowCvViewer(false);
        setCvData(null);
    };

    const handleApplicantSelect = (applicant) => {
        setSelectedApplicant(applicant);
        setReviewNotes(applicant.reviewNotes || "");
    };

    return (
        <div className={styles.container}>
            {/* Header section (unchanged) */}
            <div className={styles.header}>
                <div>
                    <button className={styles.backBtn} onClick={() => navigate('/employer/internships')}>
                        <i className="bi bi-arrow-left me-2"></i> Back to Internships
                    </button>
                    <h1 className={styles.title}>{internship.title}</h1>
                    <p className={styles.subtitle}>{internship.employerName} • {internship.companyLocation}</p>
                </div>
                {/* Stats section (unchanged) */}
                <div className={styles.stats}>
                     <div className={styles.statCard}>
                        <div className={styles.statNumber}>{applications.length}</div>
                        <div className={styles.statLabel}>Total</div>
                    </div>
                </div>
            </div>

            {/* Filters section (unchanged) */}
            <div className={styles.filters}>
                <div className={styles.searchBox}>
                    <i className="bi bi-search"></i>
                    <input type="text" placeholder="Search..." value={searchTerm} onChange={(e) => setSearchTerm(e.target.value)} />
                </div>
                <div className={styles.statusFilters}>
                    <button className={`${styles.filterBtn} ${filterStatus === "all" ? styles.active : ""}`} onClick={() => setFilterStatus("all")}>
                        All ({applications.length})
                    </button>
                    {Object.entries(statusConfig).map(([key, config]) => (
                        <button
                            key={key}
                            className={`${styles.filterBtn} ${filterStatus === key ? styles.active : ""}`}
                            onClick={() => setFilterStatus(key)}
                            style={{ color: filterStatus === key ? config.color : undefined, borderColor: filterStatus === key ? config.color : undefined }}
                        >
                            {config.label} ({applications.filter(a => a.status == key).length})
                        </button>
                    ))}
                </div>
            </div>

            <div className={styles.content}>
                {/* List section (mostly unchanged) */}
                <div className={styles.applicantsList}>
                    {filteredApplicants.map((applicant) => {
                        const status = statusConfig[applicant.status];
                        return (
                            <div
                                key={applicant.userId}
                                className={`${styles.applicantCard} ${selectedApplicant?.userId === applicant.userId ? styles.selected : ""}`}
                                onClick={() => handleApplicantSelect(applicant)}
                            >
                                {/* ... Applicant Card Content (Avatar, Name, Status Badge) ... */}
                                <div className={styles.applicantHeader}>
                                    <div className={styles.applicantAvatar}>{applicant.firstName[0]}{applicant.lastName[0]}</div>
                                    <div className={styles.applicantInfo}>
                                        <h3>{applicant.firstName} {applicant.lastName}</h3>
                                        <p>{applicant.university}</p>
                                    </div>
                                    <div className={styles.statusBadge} style={{ backgroundColor: status.color }}>
                                        <i className={`bi ${status.icon} me-1`}></i> {status.label}
                                    </div>
                                </div>
                            </div>
                        );
                    })}
                </div>

                {/* --- Details Panel --- */}
                {selectedApplicant && (
                    <div className={styles.detailsPanel}>
                        <div className={styles.detailsHeader}>
                            <h2>Applicant Details</h2>
                            <button className={styles.closeBtn} onClick={() => setSelectedApplicant(null)}>
                                <i className="bi bi-x-lg"></i>
                            </button>
                        </div>

                        <div className={styles.detailsContent}>
                            {/* Personal Info & Education (Unchanged) */}
                            <div className={styles.section}>
                                <div className={styles.detailsAvatar}>
                                    {selectedApplicant.firstName[0]}{selectedApplicant.lastName[0]}
                                </div>
                                <h3 className={styles.detailsName}>{selectedApplicant.firstName} {selectedApplicant.lastName}</h3>
                                {/* ... Contact Info ... */}
                            </div>

                             {/* Cover Letter & CV Button (Unchanged) */}
                            <div className={styles.section}>
                                <h4 className={styles.sectionTitle}>Cover Letter</h4>
                                <p className={styles.coverLetter}>{selectedApplicant.coverLetter || "No cover letter"}</p>
                                <button className={styles.viewCvBtn} onClick={handleOpenCv} disabled={isLoadingCv}>
                                    {isLoadingCv ? "Loading..." : "View CV"}
                                </button>
                            </div>

                            {/* --- Action Buttons & Notes Area --- */}
                            
                            {/* Якщо статус REJECTED - показуємо причину */}
                            {selectedApplicant.status === 2 && (
                                <div className={`${styles.section} mt-3`}>
                                    <div className="alert alert-danger">
                                        <h5 className="alert-heading"><i className="bi bi-x-circle me-2"></i>Application Rejected</h5>
                                        <hr/>
                                        <p className="mb-0"><strong>Reason:</strong> {selectedApplicant.rejectionReason || "No reason provided."}</p>
                                    </div>
                                </div>
                            )}

                            {/* Notes Area (Тільки якщо не Rejected або якщо є що показати) */}
                            {selectedApplicant.status !== 2 && (
                                <div className={styles.section}>
                                    <h4 className={styles.sectionTitle}>Internal Notes</h4>
                                    <textarea
                                        className={styles.notesTextarea}
                                        placeholder="Add internal notes (not visible to candidate)..."
                                        value={reviewNotes}
                                        onChange={(e) => setReviewNotes(e.target.value)}
                                        rows={3}
                                    />
                                </div>
                            )}

                            {/* Buttons Toolbar */}
                            <div className={styles.section} style={{display: 'flex', gap: '10px'}}>
                                
                                {/* Mark as Reviewed Button */}
                                {selectedApplicant.status === 0 && (
                                    <button 
                                        className={styles.markReviewedBtn}
                                        onClick={handleMarkAsReviewed}
                                        disabled={isSavingNotes || isRejecting}
                                        style={{flex: 1}}
                                    >
                                        {isSavingNotes ? "Saving..." : <><i className="bi bi-check-circle me-2"></i>Mark Reviewed</>}
                                    </button>
                                )}

                                {/* Reject Button (Visible if NOT already rejected) */}
                                {selectedApplicant.status !== 2 && (
                                    <button 
                                        className="btn btn-outline-danger"
                                        onClick={openRejectModal}
                                        disabled={isRejecting || isSavingNotes}
                                        style={{flex: 1}}
                                    >
                                        <i className="bi bi-x-circle me-2"></i>Reject
                                    </button>
                                )}
                            </div>

                            {/* Reviewed Info Badge */}
                            {selectedApplicant.status === 1 && (
                                <div className={styles.reviewedInfo}>
                                    <i className="bi bi-check-circle-fill me-2"></i> This candidate is currently reviewed
                                </div>
                            )}
                        </div>
                    </div>
                )}

                {/* --- Modals --- */}
                
                {/* CV Viewer Modal (Unchanged) */}
                {showCvViewer && (
                     <div className={styles.cvViewerOverlay} onClick={handleCloseCvViewer}>
                        {/* ... CV Modal Content ... */}
                        <div className={styles.cvViewerModal}><iframe src={cvData} className={styles.cvIframe} /></div>
                     </div>
                )}

                {/* --- NEW: Reject Confirmation Modal --- */}
                {showRejectModal && (
                    <div className={styles.cvViewerOverlay} style={{zIndex: 1060}}>
                        <div className={styles.cvViewerModal} style={{maxWidth: '500px', height: 'auto', padding: '20px'}} onClick={(e) => e.stopPropagation()}>
                            <div className="d-flex justify-content-between align-items-center mb-3">
                                <h3 className="m-0 text-danger">Reject Candidate</h3>
                                <button className={styles.closeBtn} onClick={() => setShowRejectModal(false)}>
                                    <i className="bi bi-x-lg"></i>
                                </button>
                            </div>
                            
                            <p className="text-muted mb-3">
                                Are you sure you want to reject <strong>{selectedApplicant?.firstName} {selectedApplicant?.lastName}</strong>? 
                                This action cannot be undone and the candidate will be notified.
                            </p>

                            <div className="mb-3">
                                <label className="form-label fw-bold">Reason for Rejection (Visible to Candidate)</label>
                                <textarea 
                                    className="form-control" 
                                    rows="4"
                                    placeholder="e.g. Unfortunately, we are looking for someone with more experience in C#..."
                                    value={rejectionReason}
                                    onChange={(e) => setRejectionReason(e.target.value)}
                                ></textarea>
                            </div>

                            <div className="d-flex justify-content-end gap-2">
                                <button className="btn btn-secondary" onClick={() => setShowRejectModal(false)}>Cancel</button>
                                <button 
                                    className="btn btn-danger" 
                                    onClick={handleConfirmReject}
                                    disabled={isRejecting}
                                >
                                    {isRejecting ? <span className="spinner-border spinner-border-sm me-2"/> : <i className="bi bi-x-circle me-2"></i>}
                                    Confirm Rejection
                                </button>
                            </div>
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
}