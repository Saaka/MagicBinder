import React, {useState, useEffect} from "react";
import {useDocumentTitle} from "Hooks";
import {CardsService} from "Services";
import {Loader} from "../../../components/Loader/Loader";
import "./CardPage.scss";


const CardPage = (props) => {
    const cardsService = new CardsService();
    const [card, setCard] = useState({images: {}});
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
            .finally(setLoading(false))

    }, []);

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

    const renderCardInventory = () => (
        <div className="card-info">
            <div className="block subtitle-block">
                <p className="subtitle">User Inventory</p>
            </div>
            <div className="block">
                <p className="is-6 info-header"><b>Placeholder</b></p>
                <p>{card.setName}</p>
            </div>
            <hr/>
        </div>
    );

    const renderCard = () => (
        <div className="box">
            <article>
                <div className="is-left">
                    <p className="title is-5">{card.name}</p>
                    <hr/>
                    <div className="columns">
                        <div className="column is-narrow center">
                            <figure className="image">
                                <img src={card.images.large}/>
                            </figure>
                        </div>
                        <div className="column card-info-columns">
                            <div className="columns">
                                <div className="column">
                                    {renderCardInventory()}
                                </div>
                                <div className="column">
                                    <hr className="column-separator"/>
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