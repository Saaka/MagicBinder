import React, {useState, useEffect} from "react";
import {useDocumentTitle} from "Hooks";
import {CardsService} from "Services";
import {Loader} from "../../../components/Loader/Loader";


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
                        <div className="column">
                            <div className="block">
                                <p className="is-6"><b>Mana cost</b></p>
                                <p>{card.manaCost}</p>
                            </div>
                            <div className="block">
                                <p className="is-6"><b>Type line</b></p>
                                <p>{card.typeLine}</p>
                            </div>
                            <div className="block">
                                <p className="is-6"><b>Oracle text</b></p>
                                <p>{card.oracleText}</p>
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