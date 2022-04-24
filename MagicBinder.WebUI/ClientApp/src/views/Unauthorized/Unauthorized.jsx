import React from "react";
import {useDocumentTitle} from "Hooks";

const Unauthorized = (props) => {
    useDocumentTitle("Unauthorized");
    return (
        <div className="columns">
            <div className="column is-responsive-medium">
                <div className="tile is-parent">
                    <article className="tile is-child notification is-primary">
                        <p className="title">Unauthorized</p>
                        <p className="subtitle">You don't have access to requested page.</p>
                        <div className="content">
                            Please try different address or contact the administrator for more information.
                        </div>
                    </article>
                </div>
            </div>
        </div>
    );
};

export {Unauthorized};