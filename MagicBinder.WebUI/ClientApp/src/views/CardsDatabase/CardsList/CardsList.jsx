import React, {useState, useEffect, useRef} from "react";
import {useDocumentTitle, useQueryString, useMessageBox} from "Hooks";
import {TextInput} from "components/forms";
import {CardsListTable} from "./CardsListTable";
import {CardsService} from "Services";
import "./CardsList.scss";

function CardsList(props) {
    useDocumentTitle("Cards database");

    const isClosed = useRef(false);
    const [setError, renderError] = useMessageBox("error", "small");
    const [query, updateQs] = useQueryString();
    const cardsService = new CardsService();
    const [cardsList, setCards] = useState({items: []});
    const [isLoading, setIsLoading] = useState(true);
    const [filters, setFilters] = useState(null);
    const emptyInputs = {name: "", typeLine: "", oracleText: ""};
    const [inputs, setInputs] = useState(emptyInputs);

    useEffect(() => {
        setIsLoading(true);
        const qs = query ?? {};
        const fv = filters ?? {};
        const [queryInputs, newFilters] = getFiltersAndInputs(qs, fv);
        setInputs(queryInputs);
        setFilters(newFilters);

        loadCards(newFilters);

        return () => {
            isClosed.current = true;
        }
    }, []);

    useEffect(() => {
        if (!isLoading) {
            setIsLoading(true);
            const [queryInputs, newFilters] = getFiltersAndInputs(query ?? {}, {});
            setInputs(queryInputs);
            setFilters(newFilters);

            loadCards(newFilters);
        }
    }, [query]);

    const getFiltersAndInputs = (qs, fv) => {
        const queryInputs = {
            name: qs.name ?? fv.name ?? "",
            typeLine: qs.typeLine ?? fv.typeLine ?? "",
            oracleText: qs.oracleText ?? fv.oracleText ?? ""
        };
        const newFilters = {
            ...queryInputs,
            pageSize: qs.pageSize ?? fv.pageSize ?? 10,
            pageNumber: qs.pageNumber ?? fv.pageNumber ?? 1
        }
        return [queryInputs, newFilters];
    }

    const applyFilters = () => {
        const newValues = {
            ...inputs,
            pageNumber: 1
        };
        updateCardFiltering(newValues);
    }

    const clearFilters = () => {
        updateCardFiltering({...filters, ...emptyInputs, pageNumber: 1});
        setInputs(emptyInputs)
    }

    const updatePageSizeFilters = (pageSize, pageNumber) => {
        const newValues = {
            pageSize: pageSize,
            pageNumber: pageNumber
        };
        updateCardFiltering(newValues);
    }

    const updateCardFiltering = (newValues) => {
        setIsLoading(true);
        updateQs(newValues);
        const newFilters = {
            ...filters,
            ...newValues
        };
        setFilters(newFilters);
        loadCards(newFilters);
    }

    const loadCards = (requestFilters) => {
        setError("");
        cardsService
            .getCards(requestFilters)
            .then((data) => {
                if (!isClosed.current) setCards(data);
            })
            .catch(ex => setError(ex.error ?? ex))
            .finally(() => {
                if (!isClosed.current) setIsLoading(false)
            });
    };

    const handleInputsChange = (ev) => {
        const {name, value} = ev.target;
        setInputs(prev => ({...prev, [name]: value}));
    }

    return (
        <React.Fragment>
            <p className="title has-text-light">Cards database</p>
            <div className="box">
                <div className="card card-filters">
                    <div className="card-header">
                        <p className="card-header-title">
                            Filters
                        </p>
                    </div>
                    <div className="card-content">
                        <div className="columns filter-columns">
                            <div className="column">
                                <TextInput id="name-input"
                                           label="Name"
                                           name="name"
                                           value={inputs.name}
                                           onChange={handleInputsChange}
                                           disabled={isLoading}
                                           onEnterPressed={applyFilters}/>
                            </div>
                            <div className="column">
                                <TextInput id="type-line-input"
                                           label="Type line"
                                           name="typeLine"
                                           value={inputs.typeLine}
                                           onChange={handleInputsChange}
                                           disabled={isLoading}
                                           onEnterPressed={applyFilters}/>
                            </div>
                            <div className="column">
                                <TextInput id="oracle-text-input"
                                           label="Oracle text"
                                           name="oracleText"
                                           value={inputs.oracleText}
                                           onChange={handleInputsChange}
                                           disabled={isLoading}
                                           onEnterPressed={applyFilters}/>
                            </div>
                        </div>
                        <div className="buttons block">
                            <button className="button is-primary"
                                    onClick={() => applyFilters()}
                                    disabled={isLoading}>
                                Apply filters
                            </button>
                            <button className="button"
                                    onClick={() => clearFilters()}
                                    disabled={isLoading}>
                                Clear filters
                            </button>
                        </div>
                    </div>
                </div>
                <CardsListTable
                    cards={cardsList.items}
                    isLoading={isLoading}
                    pageOptions={cardsList.options}
                    onPaginationChanged={updatePageSizeFilters}/>
                {renderError()}
            </div>
        </React.Fragment>
    );
}

export {CardsList};