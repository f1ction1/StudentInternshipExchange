import { NavLink } from "react-router";
import classes from './MainNavigation.module.css';
import logo from '../assets/rightCornerLogo.png';

function MainNavigation() {
    return <header className={`${classes.header} border-3 border mt-3 rounded`}>
        <nav>
            <ul className={classes.list}>
                <li>
                    <NavLink 
                        to='/'>
                        <div className={classes.logoBox}>
                            <img src={logo} alt="logo" className={classes.logo}/>   
                            <span>Internships.com</span>
                            <img src={logo} alt="logo" className={classes.logo}/>      
                        </div>
                        
                    </NavLink>
                </li>
            </ul>
        </nav>
        <nav>
            <ul className={classes.list}>
                <li>
                    <NavLink 
                        to='/'
                        className={({ isActive }) =>
                            isActive ? classes.active : undefined
                        }>
                        Student
                    </NavLink>
                </li>
                <li>
                    <NavLink 
                        to='/employerInfo'
                        className={({ isActive }) =>
                            isActive ? classes.active : undefined
                        }>
                        Employer
                    </NavLink>
                </li>
            </ul>
        </nav>
        <nav>
            <ul className={classes.list}>
                <li>
                    <NavLink 
                        to='/auth/login'>
                        Log in
                    </NavLink>
                </li>
            </ul>
        </nav>
    </header> ;
}

export default MainNavigation