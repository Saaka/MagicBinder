import React, {useState} from "react";
import {Link} from "react-router-dom";
import {RouteNames} from "routes/names";
import "./AppNavbar.scss";

function AppNavbar(props) {
    const [isMenuActive, setActiveMenu] = useState(false);

    const logout = () => props.history.replace(RouteNames.Logout);
    const login = () => props.history.replace(RouteNames.Login);

    const toggleNavbarMenu = () => setActiveMenu(prevState => !prevState);

    const menuClasses = () => isMenuActive ? "navbar-menu is-active" : "navbar-menu";
    const burgerClasses = () => isMenuActive ? "navbar-burger burger is-active" : "navbar-burger burger";

    const navbarItems = [
        {
            label: "Dashboard",
            route: RouteNames.Dashboard,
            requireAuth: true,
        },
        {
            label: "Cards Database",
            route: RouteNames.CardsDatabase,
            requireAuth: false,
        },
        {
            label: "Admin",
            route: RouteNames.Admin,
            requireAdmin: true
        },
        {
            label: "About",
            route: RouteNames.About,
            requireAuth: false,
        }
    ];

    const renderLink = (item) =>
        (<Link className="navbar-item" to={item.route} onClick={() => toggleNavbarMenu()} key={item.label} >{item.label}</Link>);

    const shouldRenderLink = (item) => {
        return (!item.requireAuth || props.user.isLoggedIn) &&
            (!item.requireAdmin || props.user.isAdmin());
    }

    return (
        <nav className="navbar is-primary" role="navigation" aria-label="main site navigation">
            <div className="container">
                <div className="navbar-brand">
                    <a className="navbar-item has-text-weight-bold">MagicBinder</a>

                    <a className={burgerClasses()}
                       onClick={toggleNavbarMenu}
                       role="button" aria-label="dropdown menu" aria-expanded="false">
                        <span aria-hidden="true"/>
                        <span aria-hidden="true"/>
                        <span aria-hidden="true"/>
                    </a>
                </div>
                <div className={menuClasses()} id="app-navbar-menu">
                    <div className="navbar-start">
                        {
                            navbarItems.map(item => (
                                shouldRenderLink(item)
                                    ? renderLink(item)
                                    : ""
                            ))
                        }
                    </div>
                    <div className="navbar-end">
                        <div className="navbar-item">
                            {
                                props.user.isLoggedIn
                                    ? (<a className="button is-primary is-inverted is-outlined" onClick={logout}>Log
                                        out</a>)
                                    : (
                                        <a className="button is-primary is-inverted is-outlined" onClick={login}>Log in</a>)
                            }
                        </div>
                    </div>
                </div>
            </div>
        </nav>
    );
}

export {AppNavbar};