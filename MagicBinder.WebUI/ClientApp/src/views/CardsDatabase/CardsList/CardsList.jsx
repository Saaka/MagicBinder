import React, {useState, useEffect} from "react";
import {useDocumentTitle, useQueryString, useMessageBox} from "Hooks";
import {CardsListTable} from "./CardsListTable";
import {CardsService} from "Services";
import "./CardsList.scss";
import {TextInput} from "components/forms";

function CardsList(props) {
    useDocumentTitle("Cards database");
    
    const [setError, renderError] = useMessageBox("error", "small");
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
        setInputs({search: query.search ?? ""});

    }, []);

    useEffect(() => {
        if (!filters) return;
        loadCards();
    }, [filters])

    const filterList = () => {
        const newValues = {
            search: inputs.search,
            pageNumber: 1
        };
        applyFiltering(newValues);
    }

    const clearFilters = () => {
        const newValues = {
            search: "",
            pageNumber: 1
        };
        applyFiltering(newValues);
        setInputs({search: ""})
    }

    const updatePageSizeFilters = (pageSize, pageNumber) => {
        const newValues = {
            pageSize: pageSize,
            pageNumber: pageNumber
        };
        applyFiltering(newValues);
    }

    const applyFiltering = (newValues) => {
        updateQs(newValues);
        setFilters(prev => ({...prev, ...newValues}));
    }

    const loadCards = () => {
        setIsLoading(true);
        setError("");
        cardsService
            .getCards({
                filter: filters.search,
                pageSize: filters.pageSize,
                pageNumber: filters.pageNumber
            })
            .then((data) => setCards(data))
            .catch(ex => setError(ex.error ?? ex))
            .finally(() => setIsLoading(false));
    };

    const handleInputsChange = (ev) => {
        const {name, value} = ev.target;
        setInputs(prev => ({...prev, [name]: value}));
    }

    return (
        <section className="columns is-centered is-mobile">
            <div className="column is-responsive">
                <p className="title has-text-light">Cards database</p>
                <div className="box">
                    <div className="filters">
                        <TextInput id="cardSearch"
                                   label="Filter"
                                   name="search"
                                   value={inputs.search}
                                   onChange={handleInputsChange}
                                   disabled={isLoading}
                                   onEnterPressed={filterList}/>

                        <div>
                            <button className="button is-primary"
                                    onClick={() => filterList()}
                                    disabled={isLoading}>
                                Apply filters
                            </button>
                            <button className="button button-clear is-primary is-light"
                                    onClick={() => clearFilters()}
                                    disabled={isLoading}>
                                Clear filters
                            </button>
                        </div>
                    </div>
                    <hr/>
                    <CardsListTable
                        cards={cardsList.items}
                        isLoading={isLoading}
                        pageOptions={cardsList.options}
                        onPaginationChanged={updatePageSizeFilters}/>
                    {renderError()}
                </div>
            </div>
        </section>
    );
}

export
{
    CardsList
}
    ;