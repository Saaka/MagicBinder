import React, {useState} from "react";
import {Icon, TooltipImage} from "components/common";
import _ from "lodash";
import "./CardPageInventory.scss";

const CardPageInventory = ({inventory, setInventory, card, isLoading}) => {
    const [editing, setEditing] = useState(false);
    const [copy, setCopy] = useState();

    const renderInventoryRows2 = () => inventory.printings.map((printing, i) =>
        (
            <div key={i} data-tip={printing.image} data-for={`image-tooltip-${i}`}>
                <p><span className="tag is-info tag-count">{printing.count}</span> {printing.setName} {printing.isFoil ? " (Foil)" : ""}</p>
                <TooltipImage id={`image-tooltip-${i}`} place="right"/>
            </div>
        ));

    const renderInventoryRows = () => inventory.printings.map((printing, i) =>
        (
            <div key={i} className="field has-addons">
                <div className="control" data-tip={printing.image} data-for={`image-tooltip-${i}`}>
                    <button className="button"><Icon name="image" onClick={(ev) => ev.stopPropagation()}/></button>
                    <TooltipImage id={`image-tooltip-${i}`} place="right"/>
                </div>
                <div className="control card-count-input">
                    <input readOnly={!editing} className={"input " + (!editing ? "edit-disabled" : "")} name="count" type="number" step="1" min="1" max="10000"
                           value={printing.count} onChange={ev => handlePrintingInfoChanged(ev, printing)}
                           onKeyPress={(event) => {
                               if (!/[0-9]/.test(event.key)) event.preventDefault();
                           }}/>
                </div>
                <div className="control is-expanded">
                    <div className="select is-fullwidth">
                        <select className={(!editing ? "edit-disabled" : "")}
                                name="card-printing"
                                value={printing.cardId}
                                onChange={ev => handlePrintingChanged(ev, printing)}
                                onClick={ev => ev.stopPropagation()}
                                disabled={isLoading}>
                            {card.printings.map((v, i) =>
                                <option key={i}
                                        value={v.cardId}
                                        data-tip={v.image} data-for={`image-option-tooltip-${i}`}>
                                    {v.setName} - #{v.collectorNumber}
                                </option>
                            )}
                        </select>
                        <TooltipImage id={`image-option-tooltip-${i}`} place="right"/>
                    </div>
                </div>
                <div className="control">
                    <button className={"button foil-control " + (!editing ? "edit-disabled" : "")} onClick={ev => toggleFoil(printing)}>
                        {printing.isFoil ? <span>Foil <Icon solid className="icon-is-foil" name="check"/></span> : "Non foil"}
                    </button>
                </div>
            </div>
        ));

    const renderButtons = () => !editing
        ?
        <button className="button is-primary is-small" onClick={() => enableChanges()}>Edit</button>
        :
        <React.Fragment>
            <button className="button is-link is-small" onClick={() => saveChanges()}>Save</button>
            <button className="button is-small button-cancel-changes" onClick={() => discardChanges()}>Cancel</button>
        </React.Fragment>
    ;

    const enableChanges = () => {
        let deepCopy = _.cloneDeep(inventory);
        setCopy(deepCopy);
        setEditing(true);
    }

    const discardChanges = () => {
        setInventory(copy);
        setEditing(false);
    }

    const saveChanges = () => {
        setEditing(false);
    }

    const handlePrintingInfoChanged = (ev, printing) => {
        const {value, name} = ev.target;
        let inventoryPrintings = inventory.printings.slice();
        let selected = inventoryPrintings.find(el => el == printing);
        selected[name] = value;
        setInventory(prev => ({...prev, [name]: value}));

    }

    const handlePrintingChanged = (ev, printing) => {
        if (!editing) return;

        const {value} = ev.target;
        let inventoryPrintings = inventory.printings.slice();
        let selected = inventoryPrintings.find(el => el == printing);
        selected.cardId = value;
        selected.image = card.printings.find(el => el.cardId == selected.cardId).image;
        setInventory(prev => ({...prev, printings: inventoryPrintings}));
    }

    const toggleFoil = (printing) => {
        if (!editing) return;

        let inventoryPrintings = inventory.printings.slice();
        let selected = inventoryPrintings.find(el => el == printing);
        selected.isFoil = !selected.isFoil;
        setInventory(prev => ({...prev, printings: inventoryPrintings}));
    }

    return (
        <div className="card-info">
            <div className="block subtitle-block">
                <p className="subtitle">Inventory {renderButtons()}</p>
            </div>
            <hr/>
            <div className="block">
                {inventory.printings.length === 0
                    ? <p>Card not owned</p>
                    : renderInventoryRows()}
            </div>
        </div>
    );
};

export {CardPageInventory};