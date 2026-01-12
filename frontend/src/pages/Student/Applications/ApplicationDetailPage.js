import { useLoaderData, useNavigate, Link } from "react-router-dom";
import styles from "./ApplicationDetailPage.module.css"
import { getApplicationDetail } from "../../../api/applicationApi";

export default function ApplicationDetailPage() {
    const application = useLoaderData();
    const navigate = useNavigate();

    const getStatusInfo = () => {
        switch(application.status) {
            case 'Rejected':
                return {
                    className: styles.statusBadgeRejected,
                    icon: 'bi bi-x-circle-fill',
                    text: 'Rejected'
                };
            case 'Reviewed':
                return {
                    className: styles.statusBadgeReviewed,
                    icon: 'bi bi-eye-fill',
                    text: 'Reviewed'
                };
            default: // Applied
                return {
                    className: styles.statusBadgePending,
                    icon: 'bi bi-hourglass-split',
                    text: 'Pending Review'
                };
        }
    };

    const statusInfo = getStatusInfo();

    return (
        <div className={styles.pageWrapper}>
        <div className={styles.container}>
            {/* Back Button */}
            <button onClick={() => navigate(-1)} className={styles.backButton}>
            <i className="bi bi-arrow-left"></i>
            Back to Applications
            </button>

            {/* Main Card */}
            <div className={styles.detailCard}>
            {/* Header */}
            <div className={styles.header}>
                <div className={styles.headerContent}>
                <h1 className={styles.title}>Application Details</h1>
                <p className={styles.subtitle}>
                    Track the status of your application
                </p>
                </div>
                <div className={statusInfo.className}>
                <i className={statusInfo.icon}></i>
                {statusInfo.text}
                </div>
            </div>

            {/* Rejection Notice */}
            {application.status === 'Rejected' && (
                <div className={styles.rejectionNotice}>
                <div className={styles.rejectionIcon}>
                    <i className="bi bi-info-circle"></i>
                </div>
                <div className={styles.rejectionContent}>
                    <h3 className={styles.rejectionTitle}>Application Not Selected</h3>
                    {application.rejectionReason ? (
                    <>
                        <p className={styles.rejectionLabel}>Employer's feedback:</p>
                        <p className={styles.rejectionReason}>{application.rejectionReason}</p>
                    </>
                    ) : (
                    <p className={styles.rejectionText}>
                        Unfortunately, the employer has decided not to move forward with your application at this time. 
                        Keep applying to other opportunities!
                    </p>
                    )}
                </div>
                </div>
            )}

            {/* Internship Info */}
            <div className={styles.internshipSection}>
                <h2 className={styles.sectionTitle}>Position Applied For</h2>
                <div className={styles.internshipCard}>
                <h3 className={styles.internshipTitle}>
                    {application.internshipTitle}
                </h3>
                <div className={styles.internshipInfo}>
                    <div className={styles.infoItem}>
                    <i className="bi bi-building"></i>
                    <span>{application.companyName}</span>
                    </div>
                    <div className={styles.infoItem}>
                    <i className="bi bi-geo-alt"></i>
                    <span>{application.companyLocation}</span>
                    </div>
                </div>
                <Link 
                    to={`/internships/${application.internshipId}`}
                    className={styles.viewInternshipLink}
                >
                    View Internship Details
                    <i className="bi bi-arrow-right"></i>
                </Link>
                </div>
            </div>

            {/* Application Timeline */}
            <div className={styles.timelineSection}>
                <h2 className={styles.sectionTitle}>Application Status</h2>
                <div className={styles.timeline}>
                {application.statusHistory.map((historyItem, index) => {
                    let icon, title;
                    
                    switch(historyItem.status) {
                    case 'Applied':
                        icon = 'bi bi-send-check';
                        title = 'Application Submitted';
                        break;
                    case 'Reviewed':
                        icon = 'bi bi-eye';
                        title = 'Application Viewed';
                        break;
                    case 'Rejected':
                        icon = 'bi bi-x-circle';
                        title = 'Application Rejected';
                        break;
                    default:
                        icon = 'bi bi-circle';
                        title = historyItem.status;
                    }

                    return (
                    <div key={index} className={styles.timelineItem}>
                        <div className={styles.timelineDot}></div>
                        <div className={styles.timelineContent}>
                        <div className={styles.timelineIcon}>
                            <i className={icon}></i>
                        </div>
                        <div className={styles.timelineText}>
                            <h4 className={styles.timelineTitle}>{title}</h4>
                            <p className={styles.timelineDate}>{historyItem.timestamp}</p>
                        </div>
                        </div>
                    </div>
                    );
                })}

                {/* Next Steps - only show if not rejected */}
                {application.status !== 'Rejected' && (
                    <div className={styles.timelineItem}>
                    <div className={`${styles.timelineDot} ${styles.timelineDotGray}`}></div>
                    <div className={styles.timelineContent}>
                        <div className={`${styles.timelineIcon} ${styles.timelineIconGray}`}>
                        <i className="bi bi-info-circle"></i>
                        </div>
                        <div className={styles.timelineText}>
                        {application.status === 'Reviewed' ? (
                            <>
                            <h4 className={styles.timelineTitle}>Next Steps</h4>
                            <p className={styles.timelineInfo}>
                                The employer has reviewed your application and may contact you if they decide to proceed.
                            </p>
                            </>
                        ) : (
                            <>
                            <h4 className={styles.timelineTitle}>Pending Review</h4>
                            <p className={styles.timelineInfo}>
                                Employers typically review applications within the first 2-7 days.
                            </p>
                            </>
                        )}
                        </div>
                    </div>
                    </div>
                )}
                </div>
            </div>

            {/* Cover Letter */}
            {application.coverLetter && (
                <div className={styles.coverLetterSection}>
                <h2 className={styles.sectionTitle}>Your Cover Letter</h2>
                <div className={styles.coverLetterCard}>
                    <div className={styles.coverLetterIcon}>
                    <i className="bi bi-file-text"></i>
                    </div>
                    <p className={styles.coverLetterText}>
                    {application.coverLetter}
                    </p>
                </div>
                </div>
            )}

            </div>
        </div>
        </div>
    );
};

export async function loader( {params} ) {
    try {
        const applicationId = params.applicationId;
        const result = await getApplicationDetail(applicationId);
        const data = result.data;
        return data;
    } catch {
        throw "Something goes wrong in ApplicationDetailLoader";
    }
    
}