import React, {useState, useEffect, useRef} from "react";
import {useDocumentTitle, useQueryString, useMessageBox} from "Hooks";
import {CardsListTable} from "./CardsListTable";
import {CardsService} from "Services";
import "./CardsList.scss";
import {TextInput} from "components/forms";

function CardsList(props) {
    useDocumentTitle("Cards database");

    const isClosed = useRef(false);
    const [setError, renderError] = useMessageBox("error", "small");
    const [qs, updateQs] = useQueryString();
    const cardsService = new CardsService();
    const [cardsList, setCards] = useState({items: []});
    const [isLoading, setIsLoading] = useState(true);
    const [filters, setFilters] = useState(null);
    const emptyInputs = {name: "", typeLine: "", oracleText: ""};
    const [inputs, setInputs] = useState(emptyInputs);

    useEffect(() => {
        setIsLoading(true);
        const query = qs ?? {};
        const queryInputs = {
            name: query.name ?? "",
            typeLine: query.typeLine ?? "",
            oracleText: query.oracleText ?? ""
        };
        setFilters({
            ...queryInputs,
            pageSize: query.pageSize ?? 10,
            pageNumber: query.pageNumber ?? 1
        });
        setInputs(queryInputs);

    }, []);

    useEffect(() => {
        isClosed.current = false;
        if (!filters) return;
        loadCards();

        return () => {
            isClosed.current = true;
        }
    }, [filters])

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
        updateQs(newValues);
        setFilters(prev => ({...prev, ...newValues}));
    }

    const loadCards = () => {
        setIsLoading(true);
        setError("");
        cardsService
            .getCards(filters)
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
        <section className="columns is-centered is-mobile">
            <div className="column is-responsive">
                <p className="title has-text-light">Cards database</p>
                <div className="box">
                    <div className="filters">
                        <div className="columns is-responsive">
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
                        <div>
                            <button className="button is-primary"
                                    onClick={() => applyFilters()}
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

export {CardsList};