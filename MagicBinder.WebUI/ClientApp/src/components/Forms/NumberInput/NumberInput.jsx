import React from "react";
import "./NumberInput.scss";

export function NumberInput(props) {

    return (
        <div className="field">
            <label className="label">{props.label}</label>
            <div className="control">
                <input type="number"
                       id={props.id}
                       name={props.name}
                       value={props.value}
                       onChange={props.onChange}
                       className="input"
                       step={props.step}
                       min={props.min}
                       max={props.max}
                       required={props.required}
                       disabled={props.disabled}/>
                <div className="control-error">{props.error}</div>
            </div>
        </div>
    );
}