import React, {useState, useEffect} from 'react';
import {Route, Navigate} from "react-router-dom";
import {Loader} from "components/common";
import {RouteNames} from "routes/names";
import {Login, Logout} from "views/exports";
import {App} from "layouts/exports";
import {AuthService} from "Services";
import {useDocumentTitle} from "Hooks";
import {User} from "Models";
import "./Index.scss";

function Index(props) {
    const authService = new AuthService();
    const [isLoading, setIsLoading] = useState(true);
    const [user, setUser] = useState(new User(false));
    
    useDocumentTitle("")
    
    useEffect(() => {
        if (authService.isLoggedIn())
            loadUserData();
        else {
            removeUser();
            hideLoader();
        }
    }, []);

    function loadUserData() {
        let userModel = authService
            .getUser();
        setUser(userModel);

        hideLoader();
    }

    function updateUser(userData) {
        setUser(new User(true, userData));
    }
    
    const clearUser = () => setUser(new User(false));

    function removeUser() {
        clearUser();
        return authService.logout()
    }

    const onLogin = (userData) => {
        hideLoader();
        updateUser(userData);
    };

    const onLogout = () => clearUser();

    const hideLoader = () => setIsLoading(false);

    function renderApp() {
        return (
            <React.Fragment>
                <Route exact
                       path={RouteNames.Root}
                       render={(renderProps) => <Navigate to={RouteNames.App}
                                                          from={renderProps.path}
                                                          {...renderProps}
                                                          user={user}
                                                          updateUser={updateUser}/>}/>
                {/*<Route path={RouteNames.Login}*/}
                {/*       render={(renderProps) => <Login {...renderProps}*/}
                {/*                                       onLogin={onLogin}*/}
                {/*                                       user={user}/>}/>*/}
                <Route path={RouteNames.Logout}
                       render={(renderProps) => <Logout {...renderProps}
                                                        onLogout={onLogout}/>}/>
                <Route path={RouteNames.App}
                       render={(props) => <App {...props} 
                                               user={user}
                                               updateUser={updateUser}/>}/>
            </React.Fragment>
        );
    }

    function renderLoader() {
        return (
            <div className="hero has-background-gradient is-fullheight">
                <div className="hero-body center">
                    <Loader/>
                </div>
            </div>
        );
    }

    return isLoading ? renderLoader() : renderApp();
}

export {Index};