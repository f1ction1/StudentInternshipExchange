import { useEffect, useState } from "react";
import { useLoaderData, useNavigate } from "react-router-dom";
import { getLikedInternships, addInternshipToFavorite, removeInternshipFromFavorite } from "../../../api/internshipApi";
import styles from "./LikedInternships.module.css"

export default function LikedInternships() {
    const navigate = useNavigate();
    const loaderData = useLoaderData();
    const [liked, setLiked] = useState({});
    const [loadingLiked, setLoadingLiked] = useState({});
    console.log(loaderData.internships)
    useEffect(() => {
        if (loaderData?.internships) {
            const newLiked = loaderData.internships.reduce((acc, item) => {
                acc[item.internshipId] = true;
                return acc;
            }, {});
            setLiked(newLiked);
        }
    }, [loaderData?.internships]);

    if(loaderData.code && loaderData.code === 'Internships.NotFound') {
        return <h1>No internships found</h1>
    }

    const handleLikeClick = async (internshipId, e) => {
        e.stopPropagation();

        const isLiked = liked[internshipId];

        setLiked(prev => ({
            ...prev,
            [internshipId]: !isLiked
        }));

        setLoadingLiked(prev => ({
            ...prev,
            [internshipId]: true
        }));

        try {
            if (isLiked) {
                await removeInternshipFromFavorite(internshipId);
            } else {
                await addInternshipToFavorite(internshipId);
            }
        } catch (ex) {
            setLiked(prev => ({
                ...prev,
                [internshipId]: isLiked
            }));
            //addToast('error', 'Something went wrong while liking this internship');
            throw "Something went wrong while liking this internship";
        } finally {
            setLoadingLiked(prev => ({
                ...prev,
                [internshipId]: false
            }));
        }
    };

    return (
        <div className={styles.container}>
            {/* Header */}
            <div className={styles.pageHeader}>
                <h1 className={styles.pageTitle}>
                    My Liked
                </h1>
                <span className={styles.likedCount}>{loaderData.internships.length}</span>
            </div>
            <div className={styles.listContainer}>
                {loaderData.internships.map((i) => (
                    <div
                        key={i.internshipId}
                        className={styles.card}
                        onClick={() => navigate(`/internships/${i.internshipId}`)}
                    >
                        <div className={styles.cardContent}>
                            <h3 className={styles.cardTitle}>{i.title}</h3>
                            <div className={styles.cardInfo}>
                                <div className={styles.employer}>
                                    <i className="bi bi-building me-2"></i>
                                    {i.employerName}
                                </div>
                                <div className={styles.location}>
                                    <i className="bi bi-geo-alt me-2"></i>
                                    {i.location}
                                    {i.isRemote && (
                                        <span className={styles.remoteBadge}>Remote</span>
                                    )}
                                </div>
                            </div>
                        </div>

                        <div className={styles.actions}>
                            <div
                                className={`${styles.actionBtn} ${liked[i.internshipId] ? styles.liked : ''}`}
                                onClick={(e) => handleLikeClick(i.internshipId, e)}
                            >
                                {loadingLiked[i.internshipId] ? (
                                    <span
                                        className="spinner-border spinner-border-sm"
                                        role="status"
                                    />
                                ) : (
                                    <i className={`bi ${liked[i.internshipId] ? 'bi-star-fill' : 'bi-star'}`}></i>
                                )}
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}

export async function loader() {
    try {
        const respose = await getLikedInternships();
        return {internships: respose.data};
    } catch(err) {
        const message = err.response?.data?.name;
        const code = err.response?.data?.code;
        if(code && code === "Internships.NotFound") {
            return {message, code};
        }
        throw "Something goes wrong while fetching liked internships"
    }
}