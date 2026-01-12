import { useLoaderData, useNavigate, useParams, useRouteLoaderData } from "react-router-dom";
import { getInternshipDetails } from "../../api/internshipApi";
import styles from './InternshipDetailPage.module.css';
import { useEffect } from "react";
import { addViewedInteraction } from "../../api/recommendationApi";

export default function InternshipDetailPage() {
    const internship = useLoaderData();
    const navigate = useNavigate();
    const params = useParams();
    const token = useRouteLoaderData('internships-root');

    useEffect(() => {
        if(token) {
            addViewedInteraction(params.internshipId);
        }
    }, [token]);

    return (
        <div className={styles.detailPage}>
        <div className={styles.container}>
            {/* Back Button */}
            <div className={styles.backButtonWrapper}>
            <button className={styles.backButton} onClick={() => {navigate(-1)}}>
                <i className="bi bi-arrow-left"></i>
                Back
            </button>
            </div>

            {/* Main Card */}
            <div className={styles.detailCard}>
            {/* Job Title */}
            <h1 className={styles.jobTitle}>{internship.title}</h1>

            {/* Company Info */}
            <div className={styles.companyInfo}>
                <div className={styles.infoItem}>
                <i className="bi bi-building"></i>
                <span><strong>{internship.employerName}</strong></span>
                </div>
                <div className={styles.infoItem}>
                <i className="bi bi-geo-alt"></i>
                <span>{internship.location}</span>
                </div>
                <div className={styles.infoItem}>
                <i className="bi bi-briefcase"></i>
                <span>Internship</span>
                </div>
            </div>

            {/* Skills Section */}
            {internship.skills && internship.skills.length > 0 && (
                <div className={styles.skillsSection}>
                <h2 className={styles.sectionTitle}>Required Skills</h2>
                <div className={styles.skillsContainer}>
                    {internship.skills.map((skill, index) => (
                    <span className={styles.skillBadge} key={index}>
                        <i className="bi bi-check-circle"></i>
                        {skill}
                    </span>
                    ))}
                </div>
                </div>
            )}

            {/* Description Section */}
            <div className={styles.descriptionSection}>
                <h2 className={styles.sectionTitle}>About this internship</h2>
                <p className={styles.descriptionText}>{internship.description}</p>
            </div>

            {/* Apply Section */}
            <div className={styles.applySection}>
                <button className={styles.applyButton} onClick={() => {navigate("apply")}}>
                    <i className="bi bi-send"></i>
                    Apply Now
                </button>
                <button className={styles.saveButton}>
                    <i className="bi bi-star"></i>
                    Save for later
                </button>
                <button className={styles.dislikeButton}>
                    <i className="bi bi-hand-thumbs-down"></i>
                    Not interested
                </button>
            </div>
            </div>
        </div>
        </div>
    );
} 

export async function loader({params}) {
    const internshipId = params.internshipId;
    //mock return
    // return {
    //     title: "Title",
    //     employerId: 1,
    //     employerName: "Employer Name",
    //     location: "Kyiv, Ukraine",
    //     skills: ["C#", "React", "Bla BLa"],
    //     description: "sdgsagggergrwrgrgrgwrgwrtrwrwrrw"
    // }
    try { 
        const response = await getInternshipDetails(internshipId);
        const internship = response.data;
        console.log(internship)
        return internship;
    } catch (ex) {
        console.log(ex);
        throw "Something goes wront in InternshipDetailPage"
    }
} 