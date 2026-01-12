import styles from './Unauthenticated.module.css';
import { Link, useNavigate, useLocation } from 'react-router-dom';


export default function Unauthenticated({ children }) {
    const navigate = useNavigate();
    const location = useLocation();

    return (
        <div className={styles.pageWrapper}>
            <div className={styles.container}>
                <div className={styles.authCard}>
                    {/* Icon */}
                    <div className={styles.iconWrapper}>
                        <i className="bi bi-shield-lock"></i>
                    </div>

                    {/* Title */}
                    <h1 className={styles.title}>Authentication Required</h1>
                    
                    {/* Description */}
                    <p className={styles.description}>
                        {children}
                    </p>

                    {/* Buttons */}
                    <div className={styles.buttonGroup}>
                        <Link to={`/auth/login?redirect=${location.pathname}`} className={styles.loginButton}>
                            <i className="bi bi-box-arrow-in-right"></i>
                            Log In
                        </Link>
                        <Link to={`/auth/registration/student?redirect=${location.pathname}`} className={styles.registerButton}>
                            <i className="bi bi-person-plus"></i>
                            Create Account
                        </Link>
                    </div>

                    {/* Additional Info */}
                    <div className={styles.infoBox}>
                        <i className="bi bi-info-circle"></i>
                        <p>
                            Registration is free and takes less than a minute. 
                            Get access to hundreds of internship opportunities!
                        </p>
                    </div>

                    {/* Back Link */}
                    <div className={styles.backLink} onClick={() => {navigate(-1)}}>
                        <i className="bi bi-arrow-left"></i>
                        Back
                    </div>
                </div>
            </div>
        </div>
    );
}