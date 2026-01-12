import { Container } from "react-bootstrap";
import { Form, useActionData, useLocation } from 'react-router-dom'

export default function AuthForm() {
    const location = useLocation();
    const isLogin = location.pathname.includes('login');
    const isStudent = location.pathname.includes('student');

    const actionData = useActionData();

    return (
        <>
            <Container className="w-50 border border-3 rounded p-4 mt-5 bg-lightshade">
                <p className="fw-bold text-center">{isLogin ? 'Please log in on the page!' : isStudent ? 'Create a student account' : 'Create an employer account'}</p>     
                {actionData && actionData.error && <Container className="my-3 py-2 border-2 rounded text-center bg-danger-subtle">{actionData.error}</Container>}
                <Form method="post">
                    { isLogin ? null : <input name="role" value={isStudent ? 'student' : 'employer'} hidden readOnly/>}
                    <div className="form-floating mb-3">
                        <input name="email" type="email" className="form-control" id="floatingInput" placeholder="name@example.com"/>
                        <label htmlFor="floatingInput">Email address</label>
                    </div>
                    <div className="form-floating">
                        <input name="password" type="password" className="form-control" id="floatingPassword" placeholder="Password"/>
                        <label htmlFor="floatingPassword">Password</label>
                    </div>
                    <Container className="m-0  p-0 text-end">
                        <button className="btn btn-primary mt-3" type="submit">{isLogin ? 'Log in' : 'Create'}</button>
                    </Container>
                </Form>
            </Container>
        </>
    );
}