import React from "react";
import {QuestionModalComponent, ModalBase} from "components/modal";

export class QuestionModal extends ModalBase {
    constructor(text, onConfirm, onClose) {
        super();
        
        this.buildModal = (toggle) => <QuestionModalComponent text={text} onConfirm={onConfirm} onClose={onClose} toggle={toggle}/>;
    }
}