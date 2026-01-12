import { useNavigate, Link } from "react-router-dom";
import { useState, useEffect } from "react";
import styles from "./CompleteProfileRedirect.module.css";

export default function CompleteProfileRedirect({children, redirectPath}) {
    const startCountState = 5;
    const [countdown, setCountdown] = useState(startCountState);
    const navigate = useNavigate();

    useEffect(() => {
        const timer = setInterval(() => {
            setCountdown((prev) => {
                if (prev <= 1) {
                    clearInterval(timer);
                    navigate(redirectPath); 
                    return 0;
                }
                return prev - 1;
            });
        }, 1000);

        return () => clearInterval(timer);
        
    }, [navigate]);
    

    return (
        <div className={styles.pageWrapper}>
            <div className={styles.container}>
                <div className={styles.redirectCard}>
                    {/* Animated Icon */}
                    <div className={styles.redirectIconWrapper}>
                        <div className={styles.pulseCircle}></div>
                        <div className={styles.pulseCircle} style={{ animationDelay: '0.5s' }}></div>
                        <i className="bi bi-person-fill-exclamation"></i>
                    </div>

                    {/* Title */}
                    <h1 className={styles.title}>Complete Your Profile</h1>
                    
                    {/* Description */}
                    <p className={styles.description}>
                        {children}
                    </p>

                    {/* Countdown */}
                    <div className={styles.countdownBox}>
                        <div className={styles.countdownCircle}>
                            <svg className={styles.countdownSvg} viewBox="0 0 100 100">
                                <circle 
                                    className={styles.countdownTrack} 
                                    cx="50" 
                                    cy="50" 
                                    r="45"
                                />
                                <circle 
                                    className={styles.countdownProgress} 
                                    cx="50" 
                                    cy="50" 
                                    r="45"
                                    style={{ 
                                        strokeDasharray: `${283 * (countdown / startCountState)} 283`,
                                        transition: 'stroke-dasharray 1s linear'
                                    }}
                                />
                            </svg>
                            <span className={styles.countdownNumber}>{countdown}</span>
                        </div>
                        <p className={styles.countdownText}>
                            Redirecting to profile editor...
                        </p>
                    </div>

                    {/* Redirect animation */}
                    <div className={styles.redirectAnimation}>
                        <i className="bi bi-arrow-right"></i>
                        <i className="bi bi-arrow-right"></i>
                        <i className="bi bi-arrow-right"></i>
                    </div>

                    {/* Manual Link */}
                    <Link to={redirectPath} className={styles.manualLink}>
                        or click here to go now
                    </Link>
                </div>
            </div>
        </div>
    );
}