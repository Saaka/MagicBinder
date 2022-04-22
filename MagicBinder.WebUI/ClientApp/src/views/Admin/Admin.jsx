import React, {useEffect} from "react"
import {useNavigate} from "react-router-dom";
import {useDocumentTitle} from "Hooks";
import {RouteNames} from "routes/names";
import "./Admin.scss";

function Admin(props) {
    useDocumentTitle("Admin page");
    const history = useNavigate();

    function redirectTo(route) {
        history.push(route);
    }

    return (
        <section className="columns is-centered">
            <div className="column is-half-desktop">
                <p className="title has-text-light">Admin Page</p>
                <div className="tile is-ancestor">
                    <div className="tile is-parent">
                        <div className="tile box" onClick={() => redirectTo(RouteNames.Home)}>
                            <div className="content">
                                <p className="title">Home</p>
                                <p className="subtitle">Just go back to home page!</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
}

export {Admin};