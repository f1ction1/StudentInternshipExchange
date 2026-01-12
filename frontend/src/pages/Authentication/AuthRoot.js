import { Container, Row, Col } from "react-bootstrap";
import { Link, Outlet, useNavigate } from "react-router-dom";
import classes from './AuthRoot.module.css';
import logo from '../../assets/rightCornerLogo.png';

export function AuthLayout() {
    const navigate = useNavigate()

    return (
        <>
            <Container>
                <Row className="py-4 fs-1">
                    <Col className="d-none d-md-none d-lg-block">
                    </Col>
                    <Col className="d-none d-md-none d-lg-block">
                    <Link className={classes.link} to='/'>
                        <div className={`${classes.logoBox} `}>
                            <img src={logo} alt="logo" className={classes.logo}/>   
                            <span>Internships.com</span>
                            <img src={logo} alt="logo" className={classes.logo}/>      
                        </div>
                    </Link>
                    </Col>
                    <Col  className="text-end">
                        <div className={classes.link} onClick={() => navigate(-1)}>
                            Back
                        </div>
                    </Col>
                </Row>
                
            </Container>
            <Outlet/>
        </>  
    );
}