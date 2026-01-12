import AuthForm from "../../components/AuthForm";
import { redirect } from "react-router-dom";

export default function RegistrationPage() {
    return (
        <>
            <AuthForm />
        </>
    )
}

export async function action({ request }) {
    const searchParams = new URL(request.url).searchParams;
    const isRedirect = searchParams.get('redirect');

    const data = await request.formData();
    const registrationData = {
        role: data.get('role'),
        email: data.get('email'),
        password: data.get('password'),
    }
    console.log('Registration action triggered: ', registrationData);
    // refactor
    const response = await fetch('https://localhost:7244/api/auth/register', {
        method: 'POST',
        headers: {
        'Content-Type': 'application/json',
        },
        body: JSON.stringify(registrationData),
    });
    
    if (response.status === 409 || response.status === 401) {
        return response;
    }

    if (!response.ok) {
        throw { 
            message: 'Could not register user.',
            status: 500
        };
    }

    const resData = await response.json();
    const token = resData.token;
    
    localStorage.setItem('token', token);

    if(isRedirect) {
        return redirect(isRedirect);
    }
    return redirect('/'+ registrationData.role);
}