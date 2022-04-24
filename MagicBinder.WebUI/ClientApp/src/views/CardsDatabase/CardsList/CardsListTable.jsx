import React from "react";
import {Icon, Loader, TableRowLoader, TooltipImage} from "components/common";
import {Pagination} from "components/navigation";
import {useHistory} from "react-router-dom";
import {RouteNames} from "routes/names";
import "./CardsListTable.scss";

export const CardsListTable = ({cards, pageOptions, isLoading, setIsLoading, onPaginationChanged}) => {
    const history = useHistory();
    
    const renderRows = () => cards.map(card =>
        (
            <tr key={card.oracleId} className="card-row" onClick={ev => openCardPage(card.oracleId)}>
                <td>
                    <Icon data-tip={card.image} data-for={`image-tooltip-${card.oracleId}`} name="image"
                          onClick={(ev) => ev.stopPropagation()}/> {card.name}
                    <TooltipImage id={`image-tooltip-${card.oracleId}`}/>
                </td>
                <td>{card.typeLine}</td>
            </tr>
        ));

    const openCardPage = (oracleId) => history.push(RouteNames.Card + oracleId);
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
        <div className="block">
            {renderTable()}
            <Pagination options={pageOptions} onPaginationChanged={onPaginationChanged} isLoading={isLoading}
                        setIsLoading={setIsLoading}/>
        </div>
    );
}