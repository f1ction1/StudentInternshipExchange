import { Container } from "react-bootstrap";
import { Form } from "react-router-dom";

export function ProfileForm({profile = {}}) {
    return (
        <Container className="border border-3 rounded p-3 bg-lightshade">
            <p className="fw-bold">Personal Data</p>
            <Form method="post">
                <input type="hidden" name="actionType" value="profileData"/>
                <div className="form-floating mb-3">
                    <input name="firstName" type="text" className="form-control" id="firstName" placeholder="First Name" defaultValue={profile.firstName || ''}/>
                    <label htmlFor="firstName">First Name<span className="text-danger">*</span></label>
                </div>
                <div className="form-floating mb-3">
                    <input name="lastName" type="text" className="form-control" id="lastName" placeholder="Last Name" defaultValue={profile.lastName || ''}/>
                    <label htmlFor="lastName">Last Name<span className="text-danger">*</span></label>
                </div>
                <div className="form-floating">
                    <input name="phoneNumber" type="text" className="form-control" id="phoneNumber" placeholder="Phone number" defaultValue={profile.phoneNumber || ''}/>
                    <label htmlFor="phoneNumber">Phone number</label>
                </div>
                <Container className="m-0  p-0 text-start">
                    <button className="btn btn-primary mt-3" type="submit">Update data</button>
                </Container>
            </Form>
        </Container>
    );
}