import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import styles from './ApplySuccessPage.module.css';

export default function ApplySuccessPage() {
  const navigate = useNavigate();
  const { internshipId } = useParams();
  const [showConfetti, setShowConfetti] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setShowConfetti(false);
    }, 3000);

    return () => clearTimeout(timer);
  }, []);

  const handleBackToInternship = () => {
    navigate(`/internships/${internshipId}`);
  };

  const handleViewApplications = () => {
    navigate('/student/profile/applications');
  };

  return (
    <div className={styles.pageWrapper}>
      {/* Confetti Animation */}
      {showConfetti && (
        <div className={styles.confettiContainer}>
          {[...Array(50)].map((_, i) => (
            <div
              key={i}
              className={styles.confetti}
              style={{
                left: `${Math.random() * 100}%`,
                animationDelay: `${Math.random() * 3}s`,
                backgroundColor: ['#3b82f6', '#10b981', '#f59e0b', '#ef4444', '#8b5cf6'][Math.floor(Math.random() * 5)]
              }}
            />
          ))}
        </div>
      )}

      <div className={styles.container}>
        <div className={styles.successCard}>
          {/* Success Icon */}
          <div className={styles.iconWrapper}>
            <div className={styles.successIcon}>
              <i className="bi bi-check-lg"></i>
            </div>
            <div className={styles.iconCircle}></div>
            <div className={styles.iconCircle} style={{ animationDelay: '0.3s' }}></div>
          </div>

          {/* Title */}
          <h1 className={styles.title}>
            <span className={styles.emoji}>🎉</span>
            Your application was submitted!
          </h1>

          {/* Success Messages */}
          <div className={styles.messagesList}>
            <div className={styles.messageItem}>
              <div className={styles.messageIcon}>
                <i className="bi bi-check-circle-fill"></i>
              </div>
              <p className={styles.messageText}>
                Thank you for applying. Your application has been successfully sent to the employer.
              </p>
            </div>
            
            <div className={styles.messageItem}>
              <div className={styles.messageIcon}>
                <i className="bi bi-bell-fill"></i>
              </div>
              <p className={styles.messageText}>
                We'll notify you when the employer reviews your application.
              </p>
            </div>
          </div>

          {/* Info Box */}
          <div className={styles.infoBox}>
            <div className={styles.infoIcon}>
              <i className="bi bi-info-circle-fill"></i>
            </div>
            <div className={styles.infoContent}>
              <h3 className={styles.infoTitle}>What's next?</h3>
              <p className={styles.infoText}>
                Employers typically review applications within 2-7 days. 
                Keep an eye on your email and check your application status regularly.
              </p>
            </div>
          </div>

          {/* Action Buttons */}
          <div className={styles.buttonGroup}>
            <button 
              onClick={handleBackToInternship}
              className={styles.primaryButton}
            >
              <i className="bi bi-arrow-left"></i>
              Back to Internship
            </button>
            <button 
              onClick={handleViewApplications}
              className={styles.secondaryButton}
            >
              <i className="bi bi-list-check"></i>
              View My Applications
            </button>
          </div>

          {/* Additional Actions */}
          <div className={styles.additionalActions}>
            <button 
              onClick={() => navigate('/internships')}
              className={styles.linkButton}
            >
              Browse more internships
              <i className="bi bi-arrow-right"></i>
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};