import React from "react"
import {useDocumentTitle} from "Hooks";
import "./Dashboard.scss";

function Dashboard(props) {
    useDocumentTitle("Dashboard");

    return (
        <section className="section columns is-centered">
            <div className="column is-half-desktop">
                <div className="tile is-parent">
                    <div className="tile box" onClick={() => console.log("tst") /*redirectTo(RouteNames.Tst)*/}>
                        <div className="content">
                            <p className="title">Tst</p>
                            <p className="subtitle">Manage Tst</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
}

export {Dashboard};