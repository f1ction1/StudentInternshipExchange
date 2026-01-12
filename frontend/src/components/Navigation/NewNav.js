import { useNavigate, Link, NavLink } from "react-router-dom";
import './NewNav.css';

export default function NewNav({tokenClaims}) {
    const navigate = useNavigate()
    const internshipsCount = 10352;
    const companiesCount = 3455;
    const resumesCount = 2535;

    return (
        <div className="nav-wrapper">
            <div className="container-fluid px-4" style={{ maxWidth: '1280px' }}>
                <div className="d-flex justify-content-between align-items-center">
                    {/* Logo */}
                    <div className="text-white text-start" onClick={() => navigate('/')}>
                        <h1 className="logo-title">internships<span className="text-blue-light">.com</span></h1>
                        <p className="logo-subtitle mb-0">Find your dream internship</p>
                    </div>
                    {!tokenClaims && (
                        <div className="header-switcher">
                            <NavLink to="/" className={({ isActive }) =>
                                isActive ? 'switcher-btn active' : 'switcher-btn'
                            }>
                                Student
                            </NavLink>
                            <NavLink to="/employerInfo" className={({ isActive }) =>
                                isActive ? 'switcher-btn active' : 'switcher-btn'
                            }>
                                Employer
                            </NavLink>
                        </div>
                    )}
                    
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
                                <span className="user-email">{tokenClaims.email}</span>
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
        </div>
    );
}   
//Just copied from SearchBar in Search page should be changed
// {/* <div className="header-wrapper">
//                     <div className="container-fluid px-4" style={{ maxWidth: '1280px' }}>
//                         <div className="d-flex justify-content-between align-items-center mb-4">
//                             {/* Logo */}
//                             <div className="text-white text-start">
//                                 <h1 className="logo-title">internships<span className="text-blue-light">.com</span></h1>
//                                 <p className="logo-subtitle mb-0">Find your dream internship</p>
//                             </div>
//                             {/* Login/User Info */}
//                             <div>
//                             {isLoggedIn ? (
//                                 <div className="d-flex align-items-center gap-2">
//                                     <button className="favorite-btn">
//                                         <i className="bi bi-star-fill fs-5"></i>
//                                     </button>
//                                     <div className="user-badge">
//                                         <div className="user-avatar">
//                                             <i className="bi bi-person-fill"></i>
//                                         </div>
//                                         <span className="user-email">{userEmail}</span>
//                                     </div>
//                                 </div>
//                                 ) : (
//                                 <button className="login-btn">
//                                     <i className="bi bi-box-arrow-in-right"></i>
//                                     Log In
//                                 </button>
//                                 )}
//                             </div>
//                         </div>

//                     {/* Search */}
//                     <div className="search-container">
//                         <form onSubmit={(e) => {e.preventDefault(); updateFilter('searchTerm', e.target.searchTerm.value);}}>
//                             <div className="d-flex align-items-center gap-2">
//                                 <i className="bi bi-search search-icon"></i>
//                                 <input 
//                                     name="searchTerm"
//                                     type="text" 
//                                     className="form-control search-input" 
//                                     placeholder="Search internships by title, description, or company..."
//                                 />
//                                 <button className="search-btn" >Search</button>
//                             </div>
//                         </form>
//                     </div>
//                 </div>
//             </div> */}