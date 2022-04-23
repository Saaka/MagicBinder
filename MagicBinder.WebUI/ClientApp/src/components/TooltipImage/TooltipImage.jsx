import React from "react";
import ReactTooltip from "react-tooltip";
import "./TooltipImage.scss";

const TooltipImage = (props) => {

    const imageContent = (dataTip) =>
        (<figure className="image">
            <img className="image-element" src={dataTip} alt="Image tooltip for current element"/>
        </figure>);

    return (
        <ReactTooltip id={props.id} 
                      place="right" 
                      effect="solid" 
                      className="tooltip-container" 
                      type="light" 
                      border={true}
                      getContent={(dt) => imageContent(dt)}>
        </ReactTooltip>
    );
}

export {TooltipImage};