import React from "react";

import "./TableRowLoader.scss";

const TableRowLoader = (props) => {

    const getColumnsCount = () => !!props.columns ?  props.columns : 1;
    
    return (
        <tr className="table-row-loader">
            {
                [...Array(getColumnsCount())].map((e, i) =>
                    <td>
                        <div><span/></div>
                    </td>
                )
            }
        </tr>
    );
};

export {TableRowLoader};