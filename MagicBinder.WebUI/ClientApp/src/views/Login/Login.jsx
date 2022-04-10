import React, {useEffect, useState} from "react";
import {useDocumentTitle} from "Hooks";
import queryString from "query-string";
import {GoogleLogin} from "components/auth";
import {Loader} from "components/common";
import {AuthService} from "Services";
import {RouteNames} from "routes/names";
import "./Login.scss";

function Login(props) {
    useDocumentTitle("Login page");
    const authService = new AuthService();
    const [loginInProgress, setLoginInProgress] = useState(false);

    useEffect(() => {
        if (props.user.isLoggedIn)
            redirectToAppRoute();
    }, []);

    function redirectToAppRoute(){
        let parsedQuery = queryString.parse(props.location.search);
        if (!!parsedQuery && parsedQuery.redirect)
            redirectToPath(parsedQuery.redirect);
        else
            redirectToMainPage();
    }
    
    function redirectToMainPage() {
        props.history.replace(RouteNames.Dashboard);
    }

    function redirectToPath(path) {
        props.history.push(path);
    }

    function onLoggedIn(userData) {
        props.updateUser(userData);
        redirectToAppRoute();
    }

    function onError(err) {
        console.log(err);
        setLoginInProgress(false);
    }

    const renderLoader = () => (<Loader/>);

    return (
        <div className="section is-fullheight">
            <div className="container login-container center">
                <div className="login-block center">
                    <h1 className="is-size-4 login-title">Login using options below</h1>
                    <GoogleLogin onLoggedIn={onLoggedIn} onError={onError} disabled={loginInProgress}
                                 onLogin={() => setLoginInProgress(true)}/>
                </div>
            </div>
        </div>
    );
}

export {Login};
