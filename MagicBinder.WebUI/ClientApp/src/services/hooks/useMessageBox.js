import React, {useState} from "react";
import {Message} from "components/common";

const useMessageBox = (type, size) => {
    const [message, setMessage] = useState("");
    const getSize = (size) => `is-${size ?? "normal"}`;

    let classes = type === "error" ? "is-danger " : "is-primary ";
    classes += getSize(size);

    const renderMessage = () => !!message
        ? <Message className={classes}>{message}</Message>
        : "";

    return [setMessage, renderMessage];
}

export {useMessageBox};