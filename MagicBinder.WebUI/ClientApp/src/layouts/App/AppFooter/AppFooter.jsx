import React from "react";
import "./AppFooter.scss";

const AppFooter = (props) => {

    return (
        <footer className="footer">
            <div className="columns is-centered is-mobile">
                <div className="column is-responsive-footer">
                    <div className="content">
                        <p className="footer-text">
                            <b>MagicBinder</b> is unofficial Fan Content permitted under the Fan Content Policy. Not approved/endorsed by Wizards. Portions of the materials used are property of
                            Wizards of the Coast. ©Wizards of the Coast LLC.
                        </p>
                        <p className="footer-text">
                            All written and graphical information presented on this site about Magic: The Gathering, including card images, the mana
                            symbols, and Oracle text, is copyright Wizards of the Coast, LLC, a subsidiary of Hasbro, Inc. <b>MagicBinder</b> is not produced by, endorsed by, supported by, or
                            affiliated with Wizards of the Coast.
                        </p>
                        <p className="footer-text">
                            Data provided on site is downloaded from <b><a href="https://scryfall.com/">Scryfall</a></b>.
                        </p>
                        <p className="footer-text">
                            Please visit my <b><a href="https://github.com/Saaka/MagicBinder" className="is-italic">GitHub page</a></b> to see the source code for this project.
                        </p>
                    </div>
                </div>
            </div>
        </footer>
    );
};

export {AppFooter};