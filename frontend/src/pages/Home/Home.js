import { useLoaderData, useNavigate } from "react-router-dom";
import './Home.css';
import RecommendedInternships from "../../components/Internship/RecommendedInternships";
import { getColaborativeRecommendations } from "../../api/recommendationApi";

const INDUSTRIES = [
    { id: 1, value: "Information Technology", icon: "bi-laptop" },
    { id: 2, value: "Finance", icon: "bi-currency-dollar" },
    { id: 3, value: "Healthcare", icon: "bi-heart-pulse" },
    { id: 4, value: "Marketing", icon: "bi-megaphone" },
    { id: 11, value: "Business & Management", icon: "bi-briefcase" },
    { id: 20, value: "HR & Recruiting", icon: "bi-people" },
];

const COUNTRIES = [
    { id: 1, value: 'Ukraine', flag: '🇺🇦' },
    { id: 2, value: 'Poland', flag: '🇵🇱' },
    { id: 3, value: 'Germany', flag: '🇩🇪' },
    { id: 4, value: 'United States', flag: '🇺🇸' },
    { id: 5, value: 'Canada', flag: '🇨🇦' },
    { id: 6, value: 'United Kingdom', flag: '🇬🇧' },
];

export default function HomePage() {
    const internshipsCount = 108982, companiesCount = 35726, resumesCount = 43678;
    const navigate = useNavigate();
    const coloborativeRecs = useLoaderData();

    const handleSearch = (e) => {
        e.preventDefault();
        const searchTerm = e.target.searchTerm.value;
        if (searchTerm) {
            navigate(`/internships?searchTerm=${searchTerm}`);
            return;
        }
        navigate('/internships');
    };

    const handleIndustryClick = (industryId) => {
        navigate(`/internships?industryIds=${industryId}`);
    };

    const handleCountryClick = (countryId) => {
        navigate(`/internships?countryId=${countryId}`);
    };

    const handleViewAllIndustries = () => {
        navigate('/industries'); // або '/internships' якщо немає окремої сторінки
    };

    const handleViewAllCountries = () => {
        navigate('/countries'); // або '/internships' якщо немає окремої сторінки
    };

    return (
        <>
            <div className="header-wrapper">
                <div className="text-light text-center mb-5">
                    <p className="fs-2 fw-bold">Internships.com - works for you!</p>
                    <div className="d-flex justify-content-center">
                        <div><strong>{internshipsCount}</strong> internships from <strong>{companiesCount}</strong> companies</div>
                        <div className="mx-4">&#9830;</div>
                        <div><strong>{resumesCount}</strong> students who have entrusted us with their resumes</div>
                    </div>
                </div>
                {/* Search */}
                <div className="search-container">
                    <form onSubmit={handleSearch}>
                        <div className="d-flex align-items-center gap-2">
                            <i className="bi bi-search search-icon"></i>
                            <input
                                name="searchTerm"
                                type="text"
                                className="form-control search-input"
                                placeholder="Search internships by title, description, or company..."
                            />
                            <button className="search-btn">Search</button>
                        </div>
                    </form>
                </div>
            </div>

            {/* Recommended Internships */}
            {coloborativeRecs && <RecommendedInternships data={coloborativeRecs} />}

            {/* Industries Section */}
            <div className="container my-5">
                <div className="section-header-row">
                    <div>
                        <h2 className="section-title">Browse by Industry</h2>
                        <p className="section-subtitle">Find internships in your field of interest</p>
                    </div>
                    <button className="view-all-btn" onClick={handleViewAllIndustries}>
                        <i className="bi bi-grid-3x3-gap"></i>
                        View All Industries
                    </button>
                </div>

                <div className="industries-grid">
                    {INDUSTRIES.map((industry) => (
                        <div
                            key={industry.id}
                            className="industry-card"
                            onClick={() => handleIndustryClick(industry.id)}
                        >
                            <div className="industry-icon">
                                <i className={`bi ${industry.icon}`}></i>
                            </div>
                            <h3 className="industry-name">{industry.value}</h3>
                            <div className="industry-arrow">
                                <i className="bi bi-arrow-right"></i>
                            </div>
                        </div>
                    ))}
                </div>
            </div>

            {/* Countries Section */}
            <div className="container my-5">
                <div className="section-header-row">
                    <div>
                        <h2 className="section-title">Browse by Location</h2>
                        <p className="section-subtitle">Discover opportunities around the world</p>
                    </div>
                    <button className="view-all-btn" onClick={handleViewAllCountries}>
                        <i className="bi bi-globe"></i>
                        View All Countries
                    </button>
                </div>

                <div className="countries-grid">
                    {COUNTRIES.map((country) => (
                        <div
                            key={country.id}
                            className="country-card"
                            onClick={() => handleCountryClick(country.id)}
                        >
                            <div className="country-flag">{country.flag}</div>
                            <h3 className="country-name">{country.value}</h3>
                            <div className="country-arrow">
                                <i className="bi bi-arrow-right"></i>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </>
    );
}

export async function loader() {
    try {
        const response = await getColaborativeRecommendations(3);
        const data = response.data;
        return data;
    } catch {
        return null;
    }
}