import React from "react";
import {useHistory, useLocation} from "react-router-dom";
import qs from "query-string";

function useQueryString() {
    const history = useHistory();
    const { search } = useLocation();
    const getParsedQuery = () => qs.parse(search);

    function update(param) {
        const prev = getParsedQuery();
        const updatedParams = qs.stringify({...prev, ...param});
        history.push({search: updatedParams});
    }

    return React.useMemo(() => [getParsedQuery(), update], [search]);
}

export {useQueryString};