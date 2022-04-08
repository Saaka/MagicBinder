import React from "react";
import {Modal} from "components/modal";

function QuestionModalComponent(props) {

    function handleConfirm(ev) {
        if (!!props.onConfirm)
            props.onConfirm(ev);
        props.toggle();
    }
    
    function handleClose(ev) {
        if(!!props.onClose)
            props.onClose(ev);
        props.toggle();
    }
    
    return (
        <Modal {...props} onClose={handleClose} >
            <div className="modal-card">
                <section className="modal-card-body">
                    <p className="subtitle">{props.text}</p>
                </section>
                <footer className="modal-card-foot">
                    <button className="button is-primary" onClick={(ev) => handleConfirm(ev)}>Yes</button>
                    <button className="button" onClick={(ev) => handleClose(ev)}>No</button>
                </footer>
            </div>
        </Modal>
    );
}

export {QuestionModalComponent};