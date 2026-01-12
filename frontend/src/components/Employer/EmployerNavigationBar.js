import classes from '../Student/StudentNavigationBar.module.css';
import { NavLink } from "react-router";
import logo from '../../assets/rightCornerLogo.png';
import vacancy_icon from '../../assets/vacancy_icon.png';
import user_icon from '../../assets/user_icon.png';
import plus_icon from '../../assets/plus-icon.png';

export default function EmployerNavigationBar({ username }) {
    return (
        <>
        <header className={`${classes.header} border-3 border mt-3 rounded`}>
            <nav>
                <ul className={classes.list}>
                    <li>
                        <NavLink 
                            to='/employer'>
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
                            to='/employer/internships'
                            end
                            className={({ isActive }) =>
                                isActive ? classes.active : undefined
                            }>
                            <div className={classes.logoBox}>
                                <img src={vacancy_icon} alt="vacancy_icon" className={classes.logo}/>   
                                <span className='ms-1'>Company internships</span>     
                            </div>
                        </NavLink>
                    </li>
                    <li>
                        <NavLink 
                            to='/employer/internships/add'
                            className={({ isActive }) =>
                                isActive ? classes.active : undefined
                            }>
                            <div className={`${classes.logoBox} border border-2 rounded p-2`}>
                                <img src={plus_icon} alt="plus_icon" className={classes.logo}/>   
                                <span className='ms-1'>Add internship</span>     
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