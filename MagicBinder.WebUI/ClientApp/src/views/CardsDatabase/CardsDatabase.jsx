import React, {useState, useEffect} from "react";
import {useDocumentTitle, useQuery} from "Hooks";
import {CardsDatabaseList} from "./CardsList/CardsDatabaseList";
import {useNavigate, useSearchParams} from "react-router-dom";
import {CardsService} from "Services";
import {RouteNames} from "routes/names";
import "./CardsDatabase.scss";

function CardsDatabase(props) {
    const history = useNavigate();
    const query = useQuery();
    const [searchParams, setSearchParams] = useSearchParams();
    useDocumentTitle(query.get("name"));
    const cardsService = new CardsService();
    const [cardsList, setCards] = useState({
        items: []
    });
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        setIsLoading(true);
        loadCards(10, 1)
            .then(() => setIsLoading(false));
    }, []);

    function loadCards(pageSize, pageNumber) {
        return cardsService
            .getCards({
                filter: "",
                pageSize: pageSize,
                pageNumber: pageNumber
            })
            .then((data) => {
                setCards(data);
            });
    }

    return (
        <section className="section columns is-centered is-mobile">
            <div className="column is-responsive">
                <p className="title has-text-light">Cards database</p>
                <div className="box">
                    <CardsDatabaseList
                        cards={cardsList.items}
                        setIsLoading={setIsLoading}
                        isLoading={isLoading}
                        pageOptions={cardsList.options}
                        fetch={loadCards}/>
                </div>
            </div>
        </section>
    );
}

export {CardsDatabase};