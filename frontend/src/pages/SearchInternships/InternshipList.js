import { useState, useCallback, useEffect } from "react";
import { OverlayTrigger, Tooltip } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { ToastContainer } from "../../components/Toast";
import { addInternshipToFavorite, removeInternshipFromFavorite } from "../../api/internshipApi";
import styles from "./InternshipList.module.css";

export default function InternshipsList({ data, isAuthenticated, onPageChange }) {
    const navigate = useNavigate();
    const [liked, setLiked] = useState({});
    const [loadingLiked, setLoadingLiked] = useState({});
    const [toasts, setToasts] = useState([]);
    
    useEffect(() => {
        if (data?.items) {
            const newLiked = data.items.reduce((acc, item) => {
                acc[item.id] = item.isLiked || false;
                return acc;
            }, {});
            setLiked(newLiked);
        }
    }, [data?.items]);
    
    const addToast = useCallback((type, message) => {
        const id = Date.now() + Math.random();
        setToasts((prev) => [...prev, { id, type, message }]);
    }, []);

    const removeToast = useCallback((id) => {
        setToasts((prev) => prev.filter((toast) => toast.id !== id));
    }, []);

    const renderTooltip = (message) => (
        <Tooltip id="tooltip">{message}</Tooltip>
    );

    const handleLikeClick = async (internshipId, e) => {
        e.stopPropagation();
        if (!isAuthenticated) return;

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
            addToast('error', 'Something went wrong while liking this internship');
        } finally {
            setLoadingLiked(prev => ({
                ...prev,
                [internshipId]: false
            }));
        }
    };

    const handlePageChange = (newPage) => {
        if (onPageChange) {
            onPageChange(newPage);
            window.scrollTo({ top: 0, behavior: 'smooth' });
        }
    };

    const renderPagination = () => {
        if (!data) return null;

        const currentPage = data.page;
        const totalPages = Math.ceil(data.totalCount / data.pageSize);

        if (totalPages <= 1) return null;
        
        const pages = [];
        const maxVisiblePages = 5;
        let startPage = Math.max(1, currentPage - Math.floor(maxVisiblePages / 2));
        let endPage = Math.min(totalPages, startPage + maxVisiblePages - 1);

        if (endPage - startPage < maxVisiblePages - 1) {
            startPage = Math.max(1, endPage - maxVisiblePages + 1);
        }

        pages.push(
            <button
                key="prev"
                className={`${styles.paginationBtn} ${!data.hasPreviousPage ? styles.disabled : ''}`}
                onClick={() => handlePageChange(currentPage - 1)}
                disabled={!data.hasPreviousPage}
            >
                <i className="bi bi-chevron-left"></i>
            </button>
        );

        if (startPage > 1) {
            pages.push(
                <button
                    key={1}
                    className={styles.paginationBtn}
                    onClick={() => handlePageChange(1)}
                >
                    1
                </button>
            );
            if (startPage > 2) {
                pages.push(<span key="ellipsis1" className={styles.ellipsis}>...</span>);
            }
        }

        for (let i = startPage; i <= endPage; i++) {
            pages.push(
                <button
                    key={i}
                    className={`${styles.paginationBtn} ${currentPage === i ? styles.active : ''}`}
                    onClick={() => handlePageChange(i)}
                >
                    {i}
                </button>
            );
        }

        if (endPage < totalPages) {
            if (endPage < totalPages - 1) {
                pages.push(<span key="ellipsis2" className={styles.ellipsis}>...</span>);
            }
            pages.push(
                <button
                    key={totalPages}
                    className={styles.paginationBtn}
                    onClick={() => handlePageChange(totalPages)}
                >
                    {totalPages}
                </button>
            );
        }

        pages.push(
            <button
                key="next"
                className={`${styles.paginationBtn} ${!data.hasNextPage ? styles.disabled : ''}`}
                onClick={() => handlePageChange(currentPage + 1)}
                disabled={!data.hasNextPage}
            >
                <i className="bi bi-chevron-right"></i>
            </button>
        );

        return <div className={styles.pagination}>{pages}</div>;
    };

    if (!data || !data.items?.length) {
        return (
            <div className={styles.noResults}>
                <i className="bi bi-search fs-1 text-muted mb-3"></i>
                <p className="fs-5 text-muted">No internships found.</p>
            </div>
        );
    }

    const totalPages = Math.ceil(data.totalCount / data.pageSize);

    return (
        <>
            <ToastContainer toasts={toasts} removeToast={removeToast} />
            <div className={styles.container}>
                <div className={styles.header}>
                    <h2 className={styles.title}>
                        {data.totalCount} {data.totalCount === 1 ? 'internship' : 'internships'}
                    </h2>
                    <span className={styles.pageInfo}>
                        Page {data.page} of {totalPages}
                    </span>
                </div>

                <div className={styles.listContainer}>
                    {data.items.map((i) => (
                        <div
                            key={i.id}
                            className={styles.card}
                            onClick={() => navigate(`/internships/${i.id}`)}
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
                                        {i.cityName}, {i.countryName}
                                        {i.isRemote && (
                                            <span className={styles.remoteBadge}>Remote</span>
                                        )}
                                    </div>
                                </div>
                            </div>

                            <div className={styles.actions}>
                                {!isAuthenticated ? (
                                    <OverlayTrigger
                                        placement="top"
                                        overlay={renderTooltip("Log in or sign up to add this internship to your favorites")}
                                    >
                                        <div className={`${styles.actionBtn} ${styles.disabled}`}>
                                            <i className="bi bi-star"></i>
                                        </div>
                                    </OverlayTrigger>
                                ) : (
                                    <div
                                        className={`${styles.actionBtn} ${liked[i.id] ? styles.liked : ''}`}
                                        onClick={(e) => handleLikeClick(i.id, e)}
                                    >
                                        {loadingLiked[i.id] ? (
                                            <span
                                                className="spinner-border spinner-border-sm"
                                                role="status"
                                            />
                                        ) : (
                                            <i className={`bi ${liked[i.id] ? 'bi-star-fill' : 'bi-star'}`}></i>
                                        )}
                                    </div>
                                )}

                                {!isAuthenticated ? (
                                    <OverlayTrigger
                                        placement="top"
                                        overlay={renderTooltip("Log in or sign up to mark this internship as not interesting")}
                                    >
                                        <div className={`${styles.actionBtn} ${styles.disabled}`}>
                                            <i className="bi bi-hand-thumbs-down"></i>
                                        </div>
                                    </OverlayTrigger>
                                ) : (
                                    <div className={styles.actionBtn}>
                                        <i className="bi bi-hand-thumbs-down"></i>
                                    </div>
                                )}
                            </div>
                        </div>
                    ))}
                </div>

                {renderPagination()}
            </div>
        </>
    );
}

// import { useState, useCallback } from "react";
// import { OverlayTrigger, Tooltip } from "react-bootstrap";
// import { useNavigate } from "react-router-dom";
// import { ToastContainer } from "../../components/Toast";
// import { addInternshipToFavorite, removeInternshipFromFavorite } from "../../api/internshipApi";
// import styles from "./InternshipList.module.css"

// export default function InternshipsList({ data, isAuthenticated }) {
//     console.log(data)
//     const navigate = useNavigate();
//     const [liked, setLiked] = useState(
//         data?.items?.reduce((acc, item) => {
//             acc[item.id] = item.isLiked || false;
//             return acc;
//         }, {}) || {}
//     );
//     const [loadingLiked, setLoadingLiked] = useState({});

//     const [toasts, setToasts] = useState([]);
    
//     const addToast = useCallback((type, message) => {
//         const id = Date.now() + Math.random();
//         setToasts((prev) => [...prev, { id, type, message }]);
//     }, []);

//     const removeToast = useCallback((id) => {
//         setToasts((prev) => prev.filter((toast) => toast.id !== id));
//     }, []);

//     if (!data || !data.items?.length) return <p>No internships found.</p>;
//     const renderTooltip = (message) => (
//         <Tooltip id="tooltip">{message}</Tooltip>
//     );

//     const handleLikeClick = async (internshipId, e) => {
//         e.stopPropagation();
//         if (!isAuthenticated) return;

//         const isLiked = liked[internshipId];

//         //Optimistic UI
//         setLiked(prev => (
//             {
//                 ...prev,
//                 [internshipId]: !isLiked
//             }
//         ))

//         setLoadingLiked(prev => ({
//             ...prev,
//             [internshipId]: true
//         }))

//         try {
//             if(isLiked) {
//                 await removeInternshipFromFavorite(internshipId);
//             } else {
//                 await addInternshipToFavorite(internshipId);
//             }
//         } catch (ex) {
//             setLiked(prev => ({
//                 ...prev,
//                 [internshipId]: isLiked
//             }));
//             addToast('error', 'Something went wrong while liking this internship');
//         } finally {
//             setLoadingLiked(prev => ({
//                 ...prev,
//                 [internshipId]: false
//             }));
//         }
//     }
//     return (
//         <>
//         <ToastContainer toasts={toasts} removeToast={removeToast}/>
//         <div className="container w-75">
//             <div className="fs-3 fw-bold mb-3">{data.pageSize} internships</div>
//             {data.items.map((i) => (
//                 <div key={i.id} className="border shadow-sm bg-body-tertiary rounded p-4 mb-3" style={{"cursor": "pointer"}} onClick={() => {navigate(`/internships/${i.id}`)}}>
//                     <span value={i.id} hidden/>
//                     <div className="fs-3 fw-bold mb-2">{i.title}</div>
//                     <div className="d-flex mb-2">
//                         <div className="me-4">{i.employerName}</div>
//                         <div>{i.cityName}, {i.countryName} {i.isRemote ? "(Remote)" : null}</div> 
//                     </div>
//                     <div className="d-flex align-items-center">
//                         {/* Star - Favorite Button */}
//                         {!isAuthenticated ? (
//                             <OverlayTrigger
//                                 placement="top"
//                                 overlay={renderTooltip("Log in or sign up to add this job to your favorites")}
//                             >
//                                 <div className="me-4 fs-4 text-primary opacity-50">
//                                     <i className="bi bi-star"></i>
//                                 </div>
//                             </OverlayTrigger>
//                         ) : (
//                             <div 
//                                 className={"me-4 fs-4 text-primary position-relative " + styles.icon}
//                                 onClick={(e) => handleLikeClick(i.id, e)}
//                             >
//                                 {loadingLiked[i.id] ? (
//                                     <span 
//                                         className="spinner-border spinner-border-sm text-primary" 
//                                         role="status"
//                                         style={{ width: "1.5rem", height: "1.5rem" }}
//                                     />
//                                 ) : (
//                                     <i 
//                                         className={`bi ${liked[i.id] ? 'bi-star-fill' : 'bi-star'}`}
//                                     ></i>
//                                 )}
//                             </div>
//                         )}
                        
//                         {/* Hand Thumbs Down */}
//                         {!isAuthenticated ? (
//                             <OverlayTrigger
//                                 placement="top"
//                                 overlay={renderTooltip("Log in or sign up to mark this job as not interesting")}
//                             >
//                                 <div className="me-2 fs-4 text-primary opacity-50">
//                                     <i className="bi bi-hand-thumbs-down"></i>
//                                 </div>
//                             </OverlayTrigger>
//                         ) : (
//                             <div className={"me-2 fs-4 text-primary " + styles.icon}>
//                                 <i className="bi bi-hand-thumbs-down"></i>
//                             </div>
//                         )} 
//                     </div>
//                 </div>
//             ))}
//         </div>
//         </>
//     );
// }
