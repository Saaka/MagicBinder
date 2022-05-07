import React from "react";
import "./Loader.scss";

const Loader = (props) => {

    const getLoaderStyle = () => {
        let style = "";
        if (props.size === "xs")
            style += "lds-dual-ring-xs";
        else
            style += "lds-dual-ring";

        if (props.dark)
            style += "-dark";
        else if (props.primary)
            style += "-primary";
        return style;
    }

    return (
        <div className={getLoaderStyle()}/>
    );
};

export {Loader};