import { NavLink, Outlet, Form } from "react-router-dom";

export default function EmployerProfilePage() {
    return (
        <div className="container mt-3">
            <div className="text-end">
                <Form action="/logout" method="post">
                    <button className="btn btn-warning">Logout</button>
                </Form>     
            </div>
            
            <ul className="nav nav-underline nav-fill">
                <li className="nav-item">
                    <NavLink
                        to="my"
                        className={({ isActive }) => `nav-link ${isActive && 'active'}`}
                    >
                        Edit personal data
                    </NavLink>
                </li>
                <li className="nav-item">
                    <NavLink
                        to="company"
                        className={({ isActive }) => `nav-link ${isActive && 'active'}`}
                    >
                        Edit company data
                    </NavLink>
                </li>
            </ul>
            <div className="container mt-4">
                <Outlet/>
            </div>
        </div>
    );
}