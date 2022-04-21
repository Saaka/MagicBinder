import React from "react";
import {Icon, Loader} from "components/common";
import {Pagination} from "components/navigation";
import "./CardsDatabaseList.scss";

export const CardsDatabaseList = ({cards, pageOptions, isLoading, setIsLoading}) => {

    const renderLoader = () => <div className="center"><Loader size="xs" dark/></div>;

    const renderTable = () => (
        <table className="table is-hoverable is-fullwidth products-table">
            <thead>
            <tr>
                <td>Card Name</td>
                <td/>
            </tr>
            </thead>
            <tbody>
            {cards.map(card =>
                (
                    <tr key={card.oracleId} className="product-row" onClick={(ev) => console.log(card.image)}>
                        <td>{card.name}</td>
                    </tr>
                )
            )}
            </tbody>
        </table>
    );
    
    return (
      <div>
          {isLoading ? renderLoader() : renderTable()}
          <Pagination options={pageOptions} fetch={fetch} isLoading={isLoading} setIsLoading={setIsLoading}/>
      </div>  
    );
}