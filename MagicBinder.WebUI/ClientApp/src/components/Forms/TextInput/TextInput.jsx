import React from "react";
import "./TextInput.scss";

export const TextInput = (props) => {

    return (
        <div className="field">
            <label className="label">{props.label}</label>
            <div className="control">
                <input type="text"
                       id={props.id}
                       name={props.name}
                       required={props.required}
                       value={props.value}
                       onChange={props.onChange}
                       disabled={props.disabled}
                       minLength={props.minLength}
                       maxLength={props.maxLength}
                       autoComplete="off"
                       ref={props.inputRef}
                       onKeyPress={event => {
                           event.key === "Enter" && !!props.onEnterPressed && props.onEnterPressed()
                       }}
                       className={"input"}/>
                <div className="control-error">{props.error}</div>
            </div>
        </div>
    )
}