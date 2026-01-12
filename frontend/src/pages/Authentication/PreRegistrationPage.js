import { Container, Row, Col } from "react-bootstrap";
import { Link } from 'react-router-dom'

export default function PreRegistrationPage() {
    return (
        <>
            <Container className="text-start mt-5">
                <Row>
                    <Col>
                        <h1 className="w-75 text-start mb-4"><span className="text-primary">Internship.com </span>works for you! Why are you hiring us today?</h1>
                    </Col>
                    <Col>
                        <Row className="mb-4">
                            <Link to='student'>
                                <div className="border border-2 rounded p-4">
                                    <p className="fs-2 text-primary fw-medium">Student</p>
                                    <p className="fs-5"><strong>43 536</strong> current internships from <strong>467</strong> companies are waiting for you</p>
                                </div>
                            </Link>
                        </Row>
                        <Row>
                            <Link to='employer'>
                                <div className="border border-2 rounded p-4">
                                    <p className="fs-2 text-primary fw-medium">Employer</p>
                                    <p className="fs-5"><strong>135 467</strong> students who have entrusted us with their resumes are waiting for you</p>
                                </div>
                            </Link>
                        </Row>
                    </Col>
                </Row>
            </Container>
        </>
    );
}