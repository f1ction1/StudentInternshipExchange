import { Row, Col } from "react-bootstrap";
import { Form, NavLink, Outlet, useNavigation, useSubmit } from "react-router-dom";

export default function StudentProfilePage() {
    const navigation = useNavigation();
    const submit = useSubmit();

    return (
        <>
            <Row className="m-0 mt-4">
                <Col className="col-4 ">
                    <div className="list-group">
                        <NavLink
                            to="/student/profile/data"
                            className={({ isActive }) => {
                            return isActive
                                ? "list-group-item list-group-item-action active"
                                : "list-group-item list-group-item-action";
                            }}
                        >
                            Personal Data
                        </NavLink>
                        <NavLink
                            to="/student/profile/liked"
                            className={({ isActive }) => {
                            return isActive
                                ? "list-group-item list-group-item-action active"
                                : "list-group-item list-group-item-action";
                            }}
                        >
                            Liked
                        </NavLink>
                        <NavLink
                            to="/student/profile/applications"
                            className={({ isActive }) => {
                            return isActive
                                ? "list-group-item list-group-item-action active"
                                : "list-group-item list-group-item-action";
                            }}
                        >
                            Applications
                        </NavLink>
                        <li className="list-group-item list-group-item-action" onClick={() => submit(null, { method: "post", action: "/logout" })}>
                            Log out
                        </li>
                    </div>
                    
                </Col>
                <Col className="col-8 border border-2">
                    {navigation.state == 'loading' ? <p>Loading...</p> : <Outlet/>}
                </Col>
            </Row>
        </>
    );
}


export function loader() {

}