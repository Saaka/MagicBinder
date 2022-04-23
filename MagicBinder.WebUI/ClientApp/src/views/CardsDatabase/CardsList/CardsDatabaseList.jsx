import React from "react";
import {Icon, Loader, TableRowLoader, TooltipImage} from "components/common";
import {Pagination} from "components/navigation";
import "./CardsDatabaseList.scss";

export const CardsDatabaseList = ({cards, pageOptions, isLoading, setIsLoading, onPaginationChanged}) => {

    const renderLoader = () => <div className="center"><Loader size="xs" dark/></div>;

    const renderRows = () => cards.map(card =>
        (
            <tr key={card.oracleId} className="card-row" onClick={(ev) => console.log("open card page " + card.oracleId)}>
                <td>
                    <Icon data-tip={card.image} data-for={`image-tooltip-${card.oracleId}`} name="image"
                          onClick={(ev) => ev.stopPropagation()}/> {card.name}
                    <TooltipImage id={`image-tooltip-${card.oracleId}`}/>
                </td>
                <td>{card.typeLine}</td>
            </tr>
        ));

    const renderRowLoaders = () => [...Array(getPageSize())].map((e, i) => <TableRowLoader columns={2} key={i}/>);

    const getPageSize = () => !!pageOptions && pageOptions.pageSize ? pageOptions.pageSize : 10;

    const renderTable = () => (
        <table className="table is-hoverable is-fullwidth cards-table">
            <thead>
            <tr>
                <th>Card Name</th>
                <th>Type</th>
            </tr>
            </thead>
            <tbody>
            {isLoading ? renderRowLoaders() : renderRows()}
            </tbody>
        </table>
    );

    return (
        <div className="cards-table-container">
            {renderTable()}
            <Pagination options={pageOptions} onPaginationChanged={onPaginationChanged} isLoading={isLoading}
                        setIsLoading={setIsLoading}/>
        </div>
    );
}