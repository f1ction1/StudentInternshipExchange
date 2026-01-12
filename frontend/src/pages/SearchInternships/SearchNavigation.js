import { Link, useNavigate } from "react-router-dom";

export default function SearchNavigation({tokenClaims}) {
    const navigate = useNavigate();

    return (
        <div className="nav-wrapper px-4 py-4 mt-3">
            <div className="d-flex justify-content-between align-items-center">
                {/* Logo */}
                <div className="text-white text-start" onClick={() => navigate('/')}>
                    <h1 className="logo-title">internships<span className="text-blue-light">.com</span></h1>
                    <p className="logo-subtitle mb-0">Find your dream internship</p>
                </div>
                {/* Login */}
                <div>
                    {tokenClaims && tokenClaims.role === "student" ? (
                    <div className="d-flex align-items-center gap-2">
                        <Link to="/student/profile/liked" className="favorite-btn">
                            <i className="bi bi-star-fill fs-5"></i>
                        </Link>
                        <Link to="/student/profile" className="user-badge">
                            <div className="user-avatar">
                                <i className="bi bi-person-fill"></i>
                            </div>
                            <span>{tokenClaims.email}</span>
                        </Link>
                    </div>
                    ) : (
                    <Link to="/auth/login">
                        <button className="login-btn">
                            <i className="bi bi-box-arrow-in-right"></i>
                            Log In
                        </button>
                    </Link>
                    )}
                </div>
            </div>
        </div>
    );
}