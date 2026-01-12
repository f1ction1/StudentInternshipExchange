import { useLoaderData } from "react-router-dom";
import { getEmployerInternships } from "../../api/internshipApi";
import { Link } from "react-router-dom";

export default function EmployerInternshipsPage() {
    const internships = useLoaderData();

    return(
        <div className="container my-5">
        <h2 className="mb-4 text-center">Your Internships</h2>

        {internships.length === 0 ? (
            <div className="alert alert-info text-center">
            You don’t have any internships yet.
            </div>
        ) : (
            <div className="row g-4">
            {internships.map((internship) => (
                <div className="col-md-6 col-lg-4" key={internship.id}>
                <div className="card h-100 shadow-sm border-0">
                    <div className="card-body d-flex flex-column">
                    <h5 className="card-title">{internship.title}</h5>
                    <h6 className="card-subtitle mb-1 text-muted">
                        {internship.city?.value}, {internship.country?.value}
                    </h6>

                    <div className="mb-1">
                        <strong>Industry:</strong> {internship.industry?.value}
                    </div>

                    <div className="d-flex mb-2">
                        <strong className="d-block me-1">Skills:</strong>
                        <div className="">
                            {internship.skills?.map((s) => (
                                <span key={s.id} className="badge bg-secondary me-1">
                                {s.value}
                                </span>
                            ))}
                        </div>
                    </div>

                    <div className="d-flex justify-content-between align-items-center mt-auto">
                        <div>
                        <span
                            className={`badge ${
                            internship.isRemote ? "bg-success" : "bg-info text-dark"
                            } me-1`}
                        >
                            {internship.isRemote ? "Remote" : "On-site"}
                        </span>
                        <span
                            className={`badge ${
                            internship.internshipStatus === "Active"
                                ? "bg-primary"
                                : "bg-secondary"
                            }`}
                        >
                            {internship.internshipStatus}
                        </span>
                        </div>
                    </div>

                    <hr className="my-3" />

                    <div className="text-muted small mb-2">
                        <i className="bi bi-calendar-event me-1"></i>
                        <strong>Expires:</strong>{" "}
                        {new Date(internship.expiresAt).toLocaleDateString()}
                    </div>
                    
                    <div className="text-muted small mb-3">
                        <i className="bi bi-clock-history me-1"></i>
                        <strong>Created:</strong>{" "}
                        {new Date(internship.createdAt).toLocaleDateString()}
                    </div>
                    <div className="d-flex justify-content-between">
                        <Link
                            to={`/employer/internships/${internship.id}`}
                            className="btn btn-outline-primary btn-sm mt-auto align-self-start"
                        >
                            View Details
                        </Link>
                        <Link
                            to={`/employer/internships/${internship.id}/applicants`}
                            className="btn btn-outline-primary btn-sm mt-auto align-self-start"
                        >
                            View applicants
                        </Link>
                    </div>
                    </div>
                </div>
                </div>
            ))}
            </div>
        )}
    </div>
    )
}

export async function loader() {
    try {
        const response = await getEmployerInternships();
        const employerInternships = response.data;
        console.log(employerInternships);
        return employerInternships ?? [];
    }
    catch {
        throw "EmployerInternshipsPageLoader error"
    }   
}