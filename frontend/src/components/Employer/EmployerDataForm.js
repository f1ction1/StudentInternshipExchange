import { Form } from 'react-router-dom'

const COMPANY_SIZES = [
  { id: 1, name: "Self-employed" },
  { id: 2, name: "Micro (2–9 employees)" },
  { id: 3, name: "Small (10–49 employees)" },
  { id: 4, name: "Medium (50–249 employees)" },
  { id: 5, name: "Large (250–999 employees)" },
  { id: 6, name: "Enterprise (1000+ employees)" },
];

export default function EmployerDataForm({employer = {}}) {
    return (
        <Form method='post' className='border border-3 rounded p-3'>
            <p className="fw-bold">Employer Data</p>
            <div className="mb-3">
            <label className="form-label">Company Name<span className="text-danger">*</span></label>
            <input
                type="text"
                className="form-control"
                name="companyName"
                defaultValue={employer.companyName}
            />
            </div>

            <div className="mb-3">
            <label className="form-label">Company Size<span className="text-danger">*</span></label>
            <select
                className="form-select"
                name="companySizeId"
                defaultValue={employer.companySizeId}
            >
                {COMPANY_SIZES.map((size) => (
                <option key={size.id} value={size.id}>
                    {size.name}
                </option>
                ))}
            </select>
            </div>

            <div className="mb-3">
            <label className="form-label">Company Description (Optional)</label>
            <textarea
                className="form-control"
                name="companyDescription"
                defaultValue={employer.companyDescription}
            />
            </div>

            <div className="mb-3">
            <label className="form-label">Company Website (Optional)</label>
            <input
                type="url"
                className="form-control"
                name="companyWebsite"
                defaultValue={employer.companyWebsite}
            />
            </div>

            <button type="submit" className="btn btn-primary">
                Edit
            </button>
        </Form>
    );
}