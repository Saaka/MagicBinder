import React from "react";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome"

const Icon = (props) => {

    const brands = ["google", "facebook-f"];
    const solidOnly = ["ban", "home"];

    function getIcon() {
        if (brands.indexOf(props.name) >= 0)
            return ["fab", props.name];

        if (props.solid || solidOnly.indexOf(props.name) >= 0)
            return ["fas", props.name];

        return ["far", props.name];
    }

    return (
        <FontAwesomeIcon icon={getIcon()} spin={props.spin} size={props.size}/>
    );
};

export {Icon};