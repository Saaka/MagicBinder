import React, {useEffect, useState} from "react";

const Pagination = (props) => {
    const [pagingSettings, setPagingSettings] = useState({
        pageNumber: 1,
        pageSize: 10,
        totalItemsCount: 0,
        totalPages: 1,
        hasPreviousPage: false,
        hasNextPage: false,
        pages: [{
            number: 1,
            isLink: true
        }]
    });

    useEffect(() => {
        if (props.options == null) return;
        
        calculatePagingSettings(props.options);
    }, [props.options]);

    const handleFetch = (pageNumber) => {
        props.setIsLoading(true);
        return props
            .fetch(pagingSettings.pageSize, pageNumber)
            .finally(() => props.setIsLoading(false));
    }

    const calculatePagingSettings = (options) => {
        const buttonsCount = 5;
        const maxButtonsFromEdge = 3;

        let settings = {
            ...options,
            pages: [{
                number: 1,
                isLink: true
            }]
        };
        if (hasOnePage(settings)) return;

        if (settings.totalPages <= buttonsCount) {
            settings.pages = getLinksWithNumbers(settings.totalPages, 1);
        } else {
            if (isFarFromFirstPage(settings, maxButtonsFromEdge)) {
                settings.pages = [{
                    number: 1,
                    isLink: true,
                }, {
                    number: -1,
                    isLink: false
                }];
            } else {
                let toGenerate = maxButtonsFromEdge;
                if (settings.pageNumber === maxButtonsFromEdge)
                    toGenerate++;
                settings.pages = getLinksWithNumbers(toGenerate, 1);
            }

            if (isInTheMiddle(settings, maxButtonsFromEdge)) {
                settings.pages.push(...getLinksWithNumbers(maxButtonsFromEdge, settings.pageNumber - 1));
            }

            if (isFarFromLastPage(settings, maxButtonsFromEdge)) {
                settings.pages.push({
                    number: -2,
                    isLink: false
                }, {
                    number: settings.totalPages,
                    isLink: true
                });
            } else {
                let toGenerate = maxButtonsFromEdge;
                if (settings.pageNumber === settings.totalPages - maxButtonsFromEdge + 1)
                    toGenerate++;
                let offset = settings.pageNumber - 1;
                if (settings.pageNumber === settings.totalPages)
                    offset--;
                settings.pages.push(...getLinksWithNumbers(toGenerate, offset));
            }
        }

        setPagingSettings(settings);
    }

    const hasOnePage = (settings) => settings.totalItemsCount === 0 || settings.totalPages === 1;
    const isInTheMiddle = (settings, maxButtonsFromEdge) => isFarFromFirstPage(settings, maxButtonsFromEdge) && isFarFromLastPage(settings, maxButtonsFromEdge);
    const isFarFromFirstPage = (settings, maxButtonsFromEdge) => settings.pageNumber > maxButtonsFromEdge;
    const isFarFromLastPage = (settings, maxButtonsFromEdge) => settings.totalPages - settings.pageNumber > maxButtonsFromEdge - 1;

    const getLinksWithNumbers = (count, offset) => Array.from(Array(count)).map((el, i) => ({
        number: i + offset,
        isLink: true
    }));

    const renderPageLink = (page) => (
        <li key={page.number}>
            <button className={"button pagination-link " + (page.number === pagingSettings.pageNumber ? "is-current" : "")}
               aria-label={"Goto page " + page.number} onClick={() => handleFetch(page.number)}
               disabled={props.isLoading}>{page.number}</button>
        </li>
    );

    const renderEllipsis = (page) => (
        <li key={page.number}>
            <span className="pagination-ellipsis">&hellip;</span>
        </li>
    );

    return (
        <nav className="pagination" role="navigation" aria-label="pagination">
            <button className="button pagination-previous" disabled={!pagingSettings.hasPreviousPage || props.isLoading}
               onClick={() => handleFetch(pagingSettings.pageNumber - 1)}>Previous</button>
            <button className="button pagination-next" disabled={!pagingSettings.hasNextPage || props.isLoading}
               onClick={() => handleFetch(pagingSettings.pageNumber + 1)}>Next page</button>
            <ul className="pagination-list">
                {pagingSettings.pages.map(page => (
                    page.isLink ? renderPageLink(page) : renderEllipsis(page)
                ))}
            </ul>
        </nav>
    )
}

export {Pagination};