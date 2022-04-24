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
        <div className="tile is-parent">
            <article className="tile is-child notification is-primary">
                <figure className="image">
                    <img src={card.images.large}/>
                </figure>
            </article>
        </div>
    );

    return (
        <div className="columns">
            <div className="column is-responsive-medium">
                {isLoading ? renderLoader() : renderCard()}
            </div>
        </div>
    );
}

export {CardPage};