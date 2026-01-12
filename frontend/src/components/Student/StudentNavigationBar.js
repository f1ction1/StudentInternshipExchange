import classes from './StudentNavigationBar.module.css';
import { NavLink } from "react-router";
import logo from '../../assets/rightCornerLogo.png';
import liked_internship_icon from '../../assets/liked_job_icon.png';
import user_icon from '../../assets/user_icon.png';

export default function StudentNavigationBar({ username }) {
    return (
        <>
        <header className={`${classes.header} border-3 border mt-3 rounded`}>
            <nav>
                <ul className={classes.list}>
                    <li>
                        <NavLink 
                            to='/student'>
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
                            to='liked'
                            className={({ isActive }) =>
                                isActive ? classes.active : undefined
                            }>
                            <div className={classes.logoBox}>
                                <img src={liked_internship_icon} alt="liked_job_icon" className={classes.logo}/>   
                                <span className='ms-1'>Liked internships</span>     
                            </div>
                        </NavLink>
                    </li>
                    <li>
                        <NavLink 
                            to='profile'
                            className={({ isActive }) =>
                                isActive ? classes.active : undefined
                            }>
                            <div className={classes.logoBox}>
                                <img src={user_icon} alt="user_icon" className={classes.logo}/>   
                                <span className='ms-1'>{username}</span>     
                            </div>
                        </NavLink>
                    </li>
                </ul>
            </nav>
        </header>
        </>
    );
}