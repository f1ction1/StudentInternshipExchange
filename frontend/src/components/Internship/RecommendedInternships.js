import { useNavigate } from "react-router-dom";
import styles from "./RecommendedInternships.module.css";

// Mock data - замініть на реальні дані з API
const mockRecommendations = [
    {
        id: 1,
        title: "Frontend Developer Intern",
        employerName: "TechCorp Solutions",
        cityName: "Warsaw",
        countryName: "Poland",
        isRemote: true,
        matchScore: 95
    },
    {
        id: 2,
        title: "UX/UI Design Internship",
        employerName: "Creative Studios",
        cityName: "Krakow",
        countryName: "Poland",
        isRemote: false,
        matchScore: 92
    },
    {
        id: 3,
        title: "Data Science Intern",
        employerName: "DataFlow Inc",
        cityName: "Berlin",
        countryName: "Germany",
        isRemote: true,
        matchScore: 89
    }
];

export default function RecommendedInternships({ data }) {
    const navigate = useNavigate();

    return (
        <div className={styles.container}>
            <div className={styles.header}>
                <div>
                    <h2 className={styles.title}>
                        <i className="bi bi-stars me-2"></i>
                        Recommended for You
                    </h2>
                    <p className={styles.subtitle}>
                        Based on your preferences and similar users' choices
                    </p>
                </div>
                <button 
                    className={styles.viewAllBtn}
                    onClick={() => navigate('/internships/recommended')}
                >
                    View All Recommendations
                    <i className="bi bi-arrow-right ms-2"></i>
                </button>
            </div>

            <div className={styles.grid}>
                {data.map((internship) => (
                    <div
                        key={internship.id}
                        className={styles.card}
                        onClick={() => navigate(`/internships/${internship.id}`)}
                    >
                        <div className={styles.matchBadge}>
                            <i className="bi bi-stars me-1"></i>
                            {internship.score}%
                        </div>
                        
                        <h3 className={styles.cardTitle}>{internship.title}</h3>
                        
                        <div className={styles.cardInfo}>
                            <div className={styles.employer}>
                                <i className="bi bi-building me-2"></i>
                                {internship.employerName}
                            </div>
                            <div className={styles.location}>
                                <i className="bi bi-geo-alt me-2"></i>
                                {internship.cityName}, {internship.countryName}
                            </div>
                            {internship.isRemote && (
                                <div className={styles.remote}>
                                    <i className="bi bi-laptop me-2"></i>
                                    Remote
                                </div>
                            )}
                        </div>

                        <button className={styles.viewBtn}>
                            View Details
                            <i className="bi bi-arrow-right ms-2"></i>
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
}