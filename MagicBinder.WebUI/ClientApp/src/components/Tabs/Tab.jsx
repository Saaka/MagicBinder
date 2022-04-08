import React from "react";

const Tab = props => {
    return (
        <li>
            <a>
                <span>{props.label}</span>
            </a>
        </li>
    )
}

export {Tab};