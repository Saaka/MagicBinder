import React from "react";
import {Tab} from "./Tab";

const Tabs = props => {

    const getTabsStyling = () => "tabs " + (props.fullwidth ? "is-fullwidth" : "");

    return (
        <div className={getTabsStyling()}>
            <ul>
                {props.tabs.map(tab =>
                    <Tab label={tab.label}
                    />
                )}
            </ul>
        </div>
    )
}

export {Tabs};