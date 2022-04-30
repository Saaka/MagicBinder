import React, {useState, useEffect} from 'react';
import "./Select.scss";

const Select = props => {
    const [idField, setIdField] = useState("id");
    const [nameField, setNameField] = useState("name");
    
    useEffect(() => {
        if(!!props.nameField)
            setNameField(props.nameField);
        
    }, [props.nameField]);

    useEffect(() => {
        if(!!props.idField)
            setIdField(props.idField);

    }, [props.idField]);
    
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
                        {props.values.map((v, i) =>
                            <option key={i}
                                    value={v[idField]}>
                                {v[nameField]}
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