import { Form, redirect, useRouteLoaderData } from 'react-router-dom'
import { getTokenClaims, tokenLoader } from '../../util/auth';

const COMPANY_SIZES = [
  { id: 1, name: "Self-employed" },
  { id: 2, name: "Micro (2–9 employees)" },
  { id: 3, name: "Small (10–49 employees)" },
  { id: 4, name: "Medium (50–249 employees)" },
  { id: 5, name: "Large (250–999 employees)" },
  { id: 6, name: "Enterprise (1000+ employees)" },
];

export default function CompleteEmployerProfiePage() {
    return (
      <div className="container mt-5">
        <h2 className="mb-4">Complete Profile</h2>
        <Form method='post' className="border p-4 rounded shadow-sm">
            <h5>Personal Information</h5>
            <div className="row mb-3">
            <div className="col-md-6">
                <label className="form-label">First Name<span className="text-danger">*</span></label>
                <input
                type="text"
                className="form-control"
                name="firstName"
                required
                />
            </div>
            <div className="col-md-6">
                <label className="form-label">Last Name<span className="text-danger">*</span></label>
                <input
                type="text"
                className="form-control"
                name="lastName"
                required
                />
            </div>
            </div>

            <div className="mb-3">
            <label className="form-label">Phone Number (Optional)</label>
            <input
                type="tel"
                className="form-control"
                name="phoneNumber"
            />
            </div>

            <h5 className="mt-4">Company Information</h5>
            <div className="mb-3">
            <label className="form-label">Company Name<span className="text-danger">*</span></label>
            <input
                type="text"
                className="form-control"
                name="companyName"
            />
            </div>

            <div className="mb-3">
            <label className="form-label">Company Size<span className="text-danger">*</span></label>
            <select
                className="form-select"
                name="companySizeId"
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
            />
            </div>

            <div className="mb-3">
            <label className="form-label">Company Website (Optional)</label>
            <input
                type="url"
                className="form-control"
                name="companyWebsite"
            />
            </div>

            <button type="submit" className="btn btn-primary">
                Submit
            </button>
        </Form>
        </div>  
    );
}

export async function loader() {
    const token = tokenLoader();
    console.log('Employer Root check token')
    if(!token) {
        return redirect('/auth/login');
    }

    const tokenClaims = await getTokenClaims();
    if (tokenClaims.role !== 'employer') {
        return redirect('/');
    }

    return tokenClaims;
}

export async function action({ request }) {
    const data = await request.formData();
    const employeeData = {
        firstName: data.get('firstName'),
        lastName: data.get('lastName'),
        phoneNumber: data.get('phoneNumber'),
        companyName: data.get('companyName'),
        companySizeId: data.get('companySizeId'),
        companyDescription: data.get('companyDescription'),
        companyWebsite: data.get('companyWebsite'),
    }
    
    const token = tokenLoader();
    if(!token) {
        return redirect('/auth/login');
    }
    try {
        const response = await fetch('https://localhost:7244/api/User/upsert-employer-profile', {
            method: 'POST',
            headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
            },
            body: JSON.stringify(employeeData),
        });

        if (!response.ok) {
            throw { 
                message: 'Could not complete profile.',
                status: 500
            };
        }
    } catch(err) {
        throw { 
            message: 'Something goes wrong :(',
            status: 500
        };
    }
    console.log('doszlo')
    return redirect('/employer');
}