import React from "react";

export const Message = (props) => {
    
    return (
        <article className={"message " + props.className}>
            <div className="message-body">
                {props.children}
            </div>
        </article> 
    );
}