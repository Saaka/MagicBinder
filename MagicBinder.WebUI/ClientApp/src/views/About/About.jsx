import React from "react";
import {useDocumentTitle} from "Hooks";

const About = (props) => {
    
    useDocumentTitle("About page");

    return (
        <div className="columns">
            <div className="column is-half-desktop is-offset-3-desktop">
                <div className="tile is-parent">
                    <article className="tile is-child notification is-primary">
                        <p className="title">Magic Binder</p>
                        <p className="subtitle">Manage your card portfolio, create decks and more to come. Stay tuned!</p>
                        <div className="content">
                            Please visit my <a href="https://github.com/Saaka/MagicBinder" className="is-italic">GitHub page</a> to see the source code for this project.
                        </div>
                    </article>
                </div>
            </div>
        </div>
    );
};

export {About};