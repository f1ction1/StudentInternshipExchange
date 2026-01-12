import { useLoaderData, Link, Form, useNavigate, useActionData, redirect } from "react-router-dom";
import styles from './InternshipApplyPage.module.css';
import Unauthenticated from "../../components/Unauthenticated";
import { IsCompleteProfile } from "../../api/userApi";
import { getTokenClaims, tokenLoader } from "../../util/auth";
import { useEffect, useState, useCallback } from "react";
import CompleteProfileRedirect from "../../components/CompleteProfileRedirect";
import { applyInternship } from "../../api/applicationApi";
import { ToastContainer } from "../../components/Toast";



export default function InternshipApplyPage() {
    const {token, isCompleteProfile} = useLoaderData();
    const navigate = useNavigate();
    const updateProfileLink = "/student/profile/data";
    const actionData = useActionData();

    const [toasts, setToasts] = useState([]);

    const addToast = useCallback((type, message) => {
        const id = Date.now() + Math.random();
        setToasts((prev) => [...prev, { id, type, message }]);
    }, []);

    const removeToast = useCallback((id) => {
        setToasts((prev) => prev.filter((toast) => toast.id !== id));
    }, []);

    useEffect(() => {
        if(actionData?.message && actionData.code === "Application.AlreadyApplied") {
            addToast('warning', actionData.message);
        }
    }, [actionData, addToast]);

    if(!token) {
        return (
            <Unauthenticated>
                To apply for this internship, you need to log in to your account 
                or create a new one. Join our platform to unlock all features!
            </Unauthenticated>
        );
    }
    if(!isCompleteProfile) {
        return (
            <CompleteProfileRedirect redirectPath={updateProfileLink}>
                Before applying for internships, please complete your profile 
                with all required information. This helps employers learn more about you!
            </CompleteProfileRedirect>
        );
    }

     return (
        <>
        <ToastContainer toasts={toasts} removeToast={removeToast}/>
        <Form method="post" className={styles.pageWrapper}>
            <div className={styles.container}>
                <div className={styles.formCard}>
                    {/* Back Button */}
                    <div onClick={() => navigate(-1)} className={styles.backButton}>
                        <i className="bi bi-arrow-left"></i>
                        Back
                    </div>

                    {/* Header */}
                    <div className={styles.formHeader}>
                        <h1 className={styles.formTitle}>Apply for Internship</h1>
                        <p className={styles.formSubtitle}>
                            Complete your application by writing a cover letter
                        </p>
                    </div>

                    {/* Profile Info Notice */}
                    <div className={styles.profileNotice}>
                        <div className={styles.noticeIcon}>
                            <i className="bi bi-info-circle-fill"></i>
                        </div>
                        <div className={styles.noticeContent}>
                            <h3 className={styles.noticeTitle}>Your Profile Information</h3>
                            <p className={styles.noticeText}>
                                Your profile data (CV, contact details, education) 
                                will be automatically shared with the employer.
                                <Link to={updateProfileLink} className={styles.noticeLink}>
                                    Update your profile
                                </Link> if you want to make any changes.
                            </p>
                        </div>
                    </div>

                    {/* Application Form */}
                    <div className={styles.formSection}>
                        <label htmlFor="coverLetter" className={styles.label}>
                            Cover Letter <span className={styles.optional}>(Optional)</span>
                        </label>
                        <p className={styles.labelHint}>
                            Tell the employer why you're a great fit for this position
                        </p>
                        <textarea
                            id="coverLetter"
                            className={styles.textarea}
                            rows="12"
                            placeholder={
                                "Dear Hiring Manager,\n\n" +
                                "I am writing to express my strong interest in the [Position] at [Company]...\n\n" +
                                "• Why you're interested in this specific internship\n" +
                                "• Relevant skills and experiences\n" +
                                "• What you hope to learn and contribute\n\n" +
                                "Thank you for considering my application."
                            }
                            name="coverLetter"
                        ></textarea>
                        <div className={styles.charCount}>
                            <i className="bi bi-pencil"></i>
                            Tip: Aim for 250-400 words
                        </div>
                    </div>

                    {/* Quick Tips */}
                    <div className={styles.tipsBox}>
                        <h4 className={styles.tipsTitle}>
                            <i className="bi bi-lightbulb"></i>
                            Cover Letter Tips
                        </h4>
                        <ul className={styles.tipsList}>
                            <li>Be specific about why you want THIS internship</li>
                            <li>Highlight relevant skills and experiences</li>
                            <li>Show enthusiasm and genuine interest</li>
                        </ul>
                    </div>

                    {/* Action Buttons */}
                    <div className={styles.formActions}>
                        <button className={styles.submitButton} type="submit">
                            <i className="bi bi-send-fill"></i>
                            Submit Application
                        </button>
                        <div onClick={() => navigate(-1)} className={styles.cancelButton}>
                            Cancel
                        </div>
                    </div>

                    {/* Footer Note */}
                    <p className={styles.footerNote}>
                        By submitting this application, you agree to share your profile 
                        information with the employer and consent to be contacted regarding 
                        this opportunity.
                    </p>
                </div>
            </div>
        </Form>
        </>
    );
}

export async function loader() {
    try {
        const token = tokenLoader();
        if(!token) {
            return {token: null, isCompleteProfile: null};
        }
        const tokenClaims = await getTokenClaims();
        if(tokenClaims.role !== 'student') {
            throw "Only students can apply internships";
        }
        const response = await IsCompleteProfile();
        const data = response.data;
        return {token , isCompleteProfile: data};
    } catch(ex) {
        throw "Something goes wrong in INternshipApplyPage loader " + ex;
    }
}

export async function action( {request, params} ) {
    const data = await request.formData();
    const coverLetter = data.get('coverLetter');
    const internshipData = {
        internshipId: params.internshipId,
        coverLetter
    }
    try {
        const response = await applyInternship(internshipData);
        return redirect('success');
    } catch(err) {
        const message = err.response?.data?.name || "Something went wrong while applying internships";
        const code = err.response?.data?.code || "404";

        return {message, code};
    }
}


