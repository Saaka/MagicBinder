import React from "react";
import {Icon, Loader, TableRowLoader} from "components/common";
import {Pagination} from "components/navigation";
import "./CardsDatabaseList.scss";

export const CardsDatabaseList = ({cards, pageOptions, isLoading, setIsLoading, onPaginationChanged}) => {

    const renderLoader = () => <div className="center"><Loader size="xs" dark/></div>;

    const renderRows = () => cards.map(card =>
        (
            <tr key={card.oracleId} className="card-row" onClick={(ev) => console.log(card.image)}>
                <td>{card.name}</td>
            </tr>
        ));

    const renderRowLoaders = () => [...Array(getPageSize())].map((e, i) => <TableRowLoader key={i}/>);

    const getPageSize = () => !!pageOptions && pageOptions.pageSize ? pageOptions.pageSize : 10;

    const renderTable = () => (
        <table className="table is-hoverable is-fullwidth cards-table">
            <thead>
            <tr>
                <td>Card Name</td>
            </tr>
            </thead>
            <tbody>
            {isLoading ? renderRowLoaders() : renderRows()}
            </tbody>
        </table>
    );

    return (
        <div>
            {renderTable()}
            <Pagination options={pageOptions} onPaginationChanged={onPaginationChanged} isLoading={isLoading}
                        setIsLoading={setIsLoading}/>
        </div>
    );
}