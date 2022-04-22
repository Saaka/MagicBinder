import React, {useState, useEffect} from "react";
import {useDocumentTitle, useQueryString} from "Hooks";
import {CardsDatabaseList} from "./CardsList/CardsDatabaseList";
import {useHistory} from "react-router-dom";
import {CardsService} from "Services";
import {RouteNames} from "routes/names";
import "./CardsDatabase.scss";

function CardsDatabase(props) {
    useDocumentTitle("Cards database");

    const history = useHistory();
    const [qs, updateQs] = useQueryString();
    const cardsService = new CardsService();
    const [cardsList, setCards] = useState({items: []});
    const [isLoading, setIsLoading] = useState(true);
    const [filters, setFilters] = useState(null);

    useEffect(() => {
        setIsLoading(true);
        const query = qs ?? {};
        setFilters({
            search: query.search ?? "",
            pageSize: query.pageSize ?? 10,
            pageNumber: query.pageNumber ?? 1
        });

    }, []);

    useEffect(() => {
        if (filters == null) return;
        loadCards();
    }, [filters])

    function updatePageSizeFilters(pageSize, pageNumber) {
        updateQs({pageSize: pageSize, pageNumber: pageNumber});
        setFilters({...filters, pageSize: pageSize, pageNumber: pageNumber})
    }

    const loadCards = () => {
        setIsLoading(true);
        cardsService
            .getCards({
                filter: filters.search,
                pageSize: filters.pageSize,
                pageNumber: filters.pageNumber
            })
            .then((data) => {
                setCards(data);
              //  setIsLoading(false);
            })
    };

    return (
        <section className="section columns is-centered is-mobile">
            <div className="column is-responsive">
                <p className="title has-text-light">Cards database</p>
                <div className="box">
                    <CardsDatabaseList
                        cards={cardsList.items}
                        isLoading={isLoading}
                        pageOptions={cardsList.options}
                        onPaginationChanged={updatePageSizeFilters}/>
                </div>
            </div>
        </section>
    );
}

export {CardsDatabase};