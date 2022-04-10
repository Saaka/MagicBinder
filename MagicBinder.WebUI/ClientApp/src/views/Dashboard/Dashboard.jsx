import React from "react"
import {useDocumentTitle} from "Hooks";
import "./Dashboard.scss";

function Dashboard(props) {
    useDocumentTitle("Dashboard");

    return (
        <section className="section columns is-centered is-mobile">
            <div className="column is-three-quarters-desktop is-half-widescreen">
                <div className="tile is-parent">
                    <div className="tile box" onClick={() => console.log("Clicked on component") /*redirectTo(RouteNames.Tst)*/}>
                        <div className="content">
                            <div className="columns is-mobile">
                                <div className="column is-narrow">
                                    <figure className="image is-64x64">
                                        <img className="is-rounded" src={props.user.avatar} alt="Logo" style={{maxWidth: "256px"}} />
                                    </figure>
                                </div>
                                <div className="column title-column">
                                    <p className="title is-size-5-mobile">Welcome {props.user.name}!</p>
                                </div>
                            </div>
                            <p className="subtitle is-size-6-mobile">Here you will have access to your dashboard. Stay tuned!</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
}

export {Dashboard};