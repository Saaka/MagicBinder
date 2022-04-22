import React from "react";

import "./TableRowLoader.scss";

const TableRowLoader = (props) => {

    const getColumnsCount = () =>
        !props.columns || props.columns === 1 ? 0 :
            props.columns % 2 > 0 ? (props.columns - 1) / 2 : props.columns / 2;

    const shouldRenderProceedingColumns = () => !!props.columns && props.columns > 2;

    return (
        <tr className="table-row-loader">
            {
                shouldRenderProceedingColumns() ?
                    [...Array(getColumnsCount())].map((e, i) =>
                        <td key={i + 1}>
                            <div><span/></div>
                        </td>
                    )
                    : <React.Fragment/>
            }
            <td key="loader">
                <div className="center">
                    <progress className="progress is-small is-primary" max="100"/>
                </div>
            </td>
            {
                [...Array(getColumnsCount())].map((e, i) =>
                    <td key={~(i + 1)}>
                        <div><span/></div>
                    </td>
                )
            }
        </tr>
    );
};

export {TableRowLoader};