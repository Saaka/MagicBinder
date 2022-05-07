import React, {useEffect} from "react";
import {useDocumentTitle} from "Hooks";
import {AuthService} from "services/AuthService";
import {Loader} from "components/common";

function Logout(props) {
    useDocumentTitle("Logout page");
    useEffect(() => {
        if (props.onLogout)
            props.onLogout();

        new AuthService().logout();
        props.history.replace('/');
    });

    return (
        <section className="hero has-background-gradient is-fullheight">
            <div className="hero-body login-body">
                <div className="container center login-container">
                    <Loader primary/>
                </div>
            </div>
        </section>
    );
}

export {Logout};