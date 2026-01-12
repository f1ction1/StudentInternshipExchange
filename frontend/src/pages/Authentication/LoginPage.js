import { Container } from "react-bootstrap";
import { Link, redirect } from 'react-router-dom'
import AuthForm from "../../components/AuthForm";
import { jwtDecode } from "jwt-decode";

export default function LoginPage() {
    return (
        <Container className="m-0 p-0">
            <AuthForm/>
            <Container className="text-center p-2">
                Don't have an account? <Link to='/auth/registration' className="fst-italic fw-medium text-primary">Register right now!</Link>
            </Container>
        </Container>
    );
}

export async function action({ request }) {
    const searchParams = new URL(request.url).searchParams;
    const isRedirect = searchParams.get('redirect');

    const data = await request.formData();
    const loginData = {
        email: data.get('email'),
        password: data.get('password'),
    }
    //console.log('Login action triggered: ', loginData);
    // refactor
    const response = await fetch('https://localhost:7244/api/auth/login', {
        method: 'POST',
        headers: {
        'Content-Type': 'application/json',
        },
        body: JSON.stringify(loginData),
    });

    if (response.status === 409 || response.status === 401) {
        return response;
    }
    if (!response.ok) {
        throw { 
            message: 'Could not authenticate user.',
            status: 500
        };
    }

    const resData = await response.json();
    const token = resData.token;
    localStorage.setItem('token', token);

    const role = jwtDecode(token).role;

    if(isRedirect) {
        return redirect(isRedirect);
    }
    return redirect('/' + role);
}