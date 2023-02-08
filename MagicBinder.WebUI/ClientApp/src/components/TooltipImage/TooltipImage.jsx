import React, {useState} from "react";
import ReactTooltip from "react-tooltip";
import {Loader} from "components/common";
import "./TooltipImage.scss";

const TooltipImage = (props) => {
    const [isLoaded, setIsLoaded] = useState(false);

    const imageContent = (dataTip, description) =>
        (<figure className="image tooltip-image center">
            {isLoaded ? null : <div><Loader primary/></div>}
            <img className="image-element"
                 style={isLoaded ? {} : {display: "none"}}
                 src={dataTip}
                 alt={description ?? "Image tooltip for current element"}
                 onLoad={() => setIsLoaded(true)}/>
        </figure>);

    const clearSelect = (ev) => {
        if (ev.target.activeElement)
            ev.target.activeElement.blur();
    }

    return (
        <ReactTooltip id={props.id}
                      place={props.place}
                      effect="float"
                      className="tooltip-container"
                      type="light"
                      border={true}
                      clickable={true}
                      getContent={(dt) => imageContent(dt, props.description)}
                      afterHide={clearSelect}>
        </ReactTooltip>
    );
}

TooltipImage.rebuild = () => {
    ReactTooltip.rebuild();
}

export {TooltipImage};