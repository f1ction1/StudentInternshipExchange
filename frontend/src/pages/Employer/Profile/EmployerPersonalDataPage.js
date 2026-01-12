import { useLoaderData } from "react-router-dom";
import { ProfileForm } from "../../../components/ProfileForm";
import { tokenLoader } from "../../../util/auth";
import { loadProfile } from "../../../util/sharedLoaders";
import Alert from "../../../components/Alert";

export default function EmployerPersonalDataPage() {
    const profile = useLoaderData();

    return (
        <>
            <Alert/>
            <ProfileForm profile={profile}/>
        </>
        
    );
}

export async function loader() {
    const token = tokenLoader();

    return await loadProfile(token);
}

export async function action({ request }) {
    const formData = await request.formData();
    const token = tokenLoader();

    const profileData = {
        firstName: formData.get('firstName'),
        lastName: formData.get('lastName'),
        phoneNumber: formData.get('phoneNumber')
    }
    try {
        const response = await fetch('https://localhost:7244/api/User/upsert-profile', {
        method: 'POST',
        headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
        },
        body: JSON.stringify(profileData),
    });

    if(!response.ok) {
        throw {message: "Saving personal data failed", statusCode: 500}
    }
    } catch (ex) {
        throw {message: ex.message}
    }
    

    return { success: true, message: "Your personal data saved successfully 🎉"};
}