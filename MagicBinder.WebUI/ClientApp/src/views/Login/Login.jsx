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

    function redirectToAppRoute() {
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
        <div className="columns login-column">
            <div className="column is-responsive-small">
                <div className="tile is-parent">
                    <article className="tile is-dark is-child notification">
                        <p className="title is-size-4 center">Login using options below</p>
                        <div className="content center">
                            <GoogleLogin onLoggedIn={onLoggedIn} onError={onError} disabled={loginInProgress}
                                         onLogin={() => setLoginInProgress(true)}/>
                        </div>
                    </article>
                </div>
            </div>
        </div>
    );
}

export {Login};
