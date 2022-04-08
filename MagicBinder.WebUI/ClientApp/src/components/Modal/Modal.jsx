import React, {useState, useEffect} from "react";
import ReactDOM from "react-dom";
import {usePortal} from "Hooks";
import "./Modal.scss";

export function Modal(props) {
    const portalEl = usePortal("modal-root");

    function handleClose(ev) {
        if (!!props.onClose) {
            props.onClose(ev);
        } else {
            props.toggle();
        }
    }

    return ReactDOM.createPortal(
        <div className="modal is-clipped is-active">
            <div className="modal-background"/>
            <div className="modal-content">
                {props.children}
            </div>
            <button className="modal-close is-large" aria-label="close" onClick={(ev) => handleClose(ev)}/>
        </div>, portalEl);
}