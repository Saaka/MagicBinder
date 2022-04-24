import React from 'react';
import "./Select.scss";

const Select = props => {
    return (
        <div className="field field-select">
            {!!props.label ? <label className="label">{props.label}</label> : ""}
            <div className="control is-expanded">
                <div className="select is-fullwidth">
                    <select id={props.id}
                            name={props.name}
                            value={props.value}
                            onChange={props.onChange}
                            required={props.required}
                            disabled={props.disabled}>
                        {props.values.map(v =>
                            <option key={v.id}
                                    value={v.id}>
                                {v.name}
                            </option>
                        )}
                    </select>
                </div>
                <div className="control-error">{props.error}</div>
            </div>
        </div>
    );
}

export {Select};