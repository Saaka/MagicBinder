import React, {useState, useEffect} from "react";
import {useDocumentTitle, useQueryString} from "Hooks";
import {CardsDatabaseList} from "./CardsList/CardsDatabaseList";
import {useHistory} from "react-router-dom";
import {CardsService} from "Services";
import {RouteNames} from "routes/names";
import "./CardsDatabase.scss";
import {TextInput} from "components/forms";

function CardsDatabase(props) {
    useDocumentTitle("Cards database");

    const history = useHistory();
    const [qs, updateQs] = useQueryString();
    const cardsService = new CardsService();
    const [cardsList, setCards] = useState({items: []});
    const [isLoading, setIsLoading] = useState(true);
    const [filters, setFilters] = useState(null);
    const [inputs, setInputs] = useState({search: ""});

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
        if (!filters) return;
        loadCards();
    }, [filters])

    const filterList = () => {
        const newValues = {
            search: inputs.search,
            pageNumber: 1
        }
        updateQs(prev => ({...prev, ...newValues}));
        setFilters(prev => ({...prev, ...newValues}));
    }

    const updatePageSizeFilters = (pageSize, pageNumber) => {
        updateQs(prev => ({...prev, pageSize: pageSize, pageNumber: pageNumber}));
        setFilters(prev => ({...prev, pageSize: pageSize, pageNumber: pageNumber}));
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
                setIsLoading(false);
            })
    };

    const handleInputsChange = (ev) => {
        const {name, value} = ev.target;
        setInputs(prev => ({...prev, [name]: value}));
    }

    return (
        <section className="section columns is-centered is-mobile">
            <div className="column is-responsive">
                <p className="title has-text-light">Cards database</p>
                <div className="box">
                    <div className="filters">
                        <TextInput id="cardSearch"
                                   label="Filter"
                                   name="search"
                                   value={inputs.search}
                                   onChange={handleInputsChange}
                                   disabled={isLoading}/>
                        <button className="button is-primary"
                                onClick={() => filterList()}>Apply filters
                        </button>
                    </div>
                    <hr/>
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