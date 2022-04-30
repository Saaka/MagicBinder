import React, {useState, useEffect} from "react";
import {useDocumentTitle} from "Hooks";
import {CardsService, InventoriesService} from "Services";
import {Icon, Loader, TooltipImage} from "components/common";
import {CardPageInventory} from "./CardPageInventory";
import "./CardPage.scss";

const CardPage = (props) => {
    const cardsService = new CardsService();
    const inventoriesService = new InventoriesService();
    const [card, setCard] = useState({images: {}, printings: []});
    const [inventory, setInventory] = useState({printings: []});
    const [isLoading, setLoading] = useState(true);
    useDocumentTitle(card.name);

    useEffect(() => {
        setLoading(true);
        const oracleId = props.match.params.oracleId;

        cardsService
            .getCardDetails(oracleId)
            .then(resp => {
                setCard(resp);
            })
            .then(loadInventory(oracleId))
            .finally(setLoading(false))

    }, []);

    const loadInventory = (oracleId) => {
        if (!props.user.isLoggedIn) return;

        return inventoriesService
            .getCardInventory(oracleId)
            .then(resp => {
                setInventory(resp)
            });
    }

    const renderLoader = () => <div className="center"><Loader size="xs" dark/></div>;

    const renderCardDetails = () => (
        <div className="card-info">
            <div className="block subtitle-block">
                <p className="subtitle">Details</p>
            </div>
            <div className="block">
                <p className="is-6 info-header"><b>Mana cost</b></p>
                <p>{card.manaCost}</p>
            </div>
            <hr/>
            <div className="block">
                <p className="is-6"><b>Type line</b></p>
                <p>{card.typeLine}</p>
            </div>
            <hr/>
            <div className="block">
                <p className="is-6"><b>Oracle text</b></p>
                <p>{card.oracleText}</p>
            </div>
        </div>
    );

    const renderCardInventory = () => (!props.user.isLoggedIn ? "" :
        <div className="column">
            <CardPageInventory inventory={inventory} card={card} setInventory={setInventory}/>
            <hr className="column-separator"/>
        </div>);

    const renderCard = () => (
        <div className="box">
            <article>
                <div className="is-left">
                    <div className="block card-page-title">
                        <p className="title is-5">{card.name}</p>
                        <p className="subtitle is-6">{card.setName} - #{card.collectorNumber}</p>
                    </div>
                    <hr/>
                    <div className="columns">
                        <div className="column is-narrow center image-column">
                            <figure className="image card-image">
                                <img src={card.images.large}/>
                            </figure>
                        </div>
                        <div className="column card-info-columns">
                            <div className="columns">
                                {renderCardInventory()}
                                <div className="column">
                                    {renderCardDetails()}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </article>
        </div>
    );

    return (
        <div className="columns center">
            <div className="column is-responsive">
                {isLoading ? renderLoader() : renderCard()}
            </div>
        </div>
    );
}

export {CardPage};