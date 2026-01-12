import { data, useLoaderData, useNavigate } from "react-router-dom";
import { applyInternship, getStudentApplications } from "../../../api/applicationApi";
import styles from "./ApplicationsPage.module.css"

export default function ApplicationsPage() {
    const {error, data: applications} = useLoaderData(); 
    const navigate = useNavigate();

    const handleBrowseClick = () => {
        navigate('/internships');
    };

    return (
    <div className={styles.applicationsPage}>
      <div className={styles.container}>
        {/* Header */}
        <div className={styles.pageHeader}>
          <h1 className={styles.pageTitle}>
            My Applications
            <span className={styles.applicationsCount}>{applications.length}</span>
          </h1>
          <p className={styles.pageSubtitle}>
            Track all your internship applications in one place
          </p>
        </div>

        {/* Empty State */}
        {applications.length === 0 ? (
          <div className={styles.emptyState}>
            <div className={styles.emptyIcon}>
              <i className="bi bi-inbox"></i>
            </div>
            <h2 className={styles.emptyTitle}>No applications yet</h2>
            <p className={styles.emptyText}>
              Start your journey by applying to internships that match your interests!
            </p>
            <button onClick={handleBrowseClick} className={styles.browseButton}>
              <i className="bi bi-search"></i>
              Browse Internships
            </button>
          </div>
        ) : (
          /* Applications Grid */
          <div className={styles.applicationsGrid}>
            {applications.map((app) => (
              <div 
                key={app.id} 
                onClick={() => navigate(app.id)}
                className={styles.applicationCard}
              >
                <div className={styles.cardHeader}>
                  <div>
                    <h3 className={styles.cardTitle}>
                      {app.internshipTitle}
                    </h3>
                  </div>
                  <div className={styles.cardTime}>
                    <i className="bi bi-clock"></i>
                    {app.relativeTime}
                  </div>
                </div>

                <div className={styles.cardInfo}>
                  {app.companyName && (
                    <div className={styles.infoItem}>
                      <i className="bi bi-building"></i>
                      <span>{app.companyName}</span>
                    </div>
                  )}
                  {app.companyLocation && (
                    <div className={styles.infoItem}>
                      <i className="bi bi-geo-alt"></i>
                      <span>{app.companyLocation}</span>
                    </div>
                  )}
                </div>

                <div className={styles.cardFooter}>
                  {app.status === "Applied" ? <span className={`${styles.statusBadge} ${styles.statusPending}`}>
                    <i className="bi bi-hourglass-split"></i>
                    Pending Review
                  </span> : null}
                  {app.status === "Reviewed" ? <span className={`${styles.statusBadge} ${styles.statusReviewed}`}>
                    <i className="bi bi-eye-fill"></i>
                    Reviewed
                  </span> : null}
                  {app.status === "Rejected" ? <span className={`${styles.statusBadge} ${styles.statusRejected}`}>
                    <i className="bi bi-x-circle-fill"></i>
                    Rejected
                  </span> : null}
                  <span className={styles.viewDetails}>
                    Click to view details
                    <i className="bi bi-arrow-right"></i>
                  </span>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}

export async function loader() {
    try {
        const response = await getStudentApplications();
        const data = response.data;
        return {error: null, data};
    } catch(err) {
        const message = err.response?.data?.name;
        const code = err.response?.data?.code;
        if(message && code) {
            return {error: {message, code}, data: null};
        }
        throw "Something goes wrong while geting applications";
    }
}