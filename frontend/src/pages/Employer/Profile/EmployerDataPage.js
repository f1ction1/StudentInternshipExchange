import { useLoaderData } from "react-router-dom";
import EmployerDataForm from "../../../components/Employer/EmployerDataForm";
import { tokenLoader } from "../../../util/auth";
import Alert from "../../../components/Alert";

export default function EmployerDataPage() {
    const employer = useLoaderData();

    return (
        <>
            <Alert/>
            <EmployerDataForm employer={employer}/>
        </>
    );
}

async function loadEmployer(token) {
    try {
        const response = await fetch('https://localhost:7244/api/User/employer', {
            headers: {
                'Authorization': 'Bearer ' + token 
            }
        });
        return await response.json();
    } catch (ex) {
        throw { message: "Can't load employer data"}
    }
}

export async function loader() {
    const token = tokenLoader();

    return await loadEmployer(token);
}

export async function action({ request }) {
    const token = tokenLoader();
    const formData = await request.formData();

    const employerData = {
        companyName: formData.get('companyName'),
        companySizeId: formData.get('companySizeId'),
        companyDescription: formData.get('companyDescription'),
        companyWebsite: formData.get('companyWebsite')
    }
    
    try {
        const response = await fetch('https://localhost:7244/api/User/employer/edit', {
            method: 'PUT',
            headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
            },
            body: JSON.stringify(employerData),
        });

        if(!response.ok) {
            throw {message: "Saving personal data failed", statusCode: 500}
        }

        return { success: true, message: "Your company data saved successfully 🎉"};
    } catch( ex ) {
        throw { message: ex.message}
    } 
}