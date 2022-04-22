import React from "react";

import "./TableRowLoader.scss";

const TableRowLoader = (props) => {

    const getColumnsCount = () => !!props.columns && props.columns > 1 ? props.columns - 1 : 0;

    return (
        <tr className="table-row-loader">
            <td key="loader">
                <div className="center">
                    <progress className="progress is-small is-primary" max="100"/>
                </div>
            </td>
            {
                [...Array(getColumnsCount())].map((e, i) =>
                    <td key={i}>
                        <div><span/></div>
                    </td>
                )
            }
        </tr>
    );
};

export {TableRowLoader};