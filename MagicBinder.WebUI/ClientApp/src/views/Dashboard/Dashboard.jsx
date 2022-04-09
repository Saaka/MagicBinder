import React from "react"
import {useDocumentTitle} from "Hooks";
import "./Dashboard.scss";

function Dashboard(props) {
    useDocumentTitle("Dashboard");

    return (
        <section className="section columns is-centered">
            <div className="column is-half-desktop">
                <div className="tile is-parent">
                    <div className="tile box" onClick={() => console.log("Clicked on component") /*redirectTo(RouteNames.Tst)*/}>
                        <div className="content">
                            <p className="title">Welcome {props.user.name}!</p>
                            <p className="subtitle">Here you will have access to your dashboard. Stay tuned!</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
}

export {Dashboard};