import React from "react";
import {Icon} from "components/common";
import "./Checkbox.scss";

export function Checkbox(props) {

    const icon = () => props.value ? "check-square" : "square";

    return (
        <div className="field">
            <label className="label">{props.label}</label>
            <div className="control">
                <label className="switch">
                    <input type="checkbox"
                           id={props.is}
                           name={props.name}
                           checked={props.value}
                           onChange={props.onChange}
                           disabled={props.disabled}/>
                    <span className="slider round"></span>
                </label>
            </div>
        </div>
    );
}