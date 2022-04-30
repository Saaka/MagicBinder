import React from "react";
import ReactTooltip from "react-tooltip";
import "./TooltipImage.scss";

const TooltipImage = (props) => {

    const imageContent = (dataTip) =>
        (<figure className="image">
            <img className="image-element" src={dataTip} alt="Image tooltip for current element"/>
        </figure>);

    const clearSelect = (ev) => {
        if (ev.target.activeElement)
            ev.target.activeElement.blur();
    }

    return (
        <ReactTooltip id={props.id}
                      place={props.place}
                      effect="solid"
                      className="tooltip-container"
                      type="light"
                      border={true}
                      clickable={true}
                      getContent={(dt) => imageContent(dt)}
                      afterHide={clearSelect}>
        </ReactTooltip>
    );
}

export {TooltipImage};