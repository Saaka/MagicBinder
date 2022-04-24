import React from "react";
import {useHistory, useLocation} from "react-router-dom";
import qs from "query-string";

function useQueryString() {
    const history = useHistory();
    const {search} = useLocation();
    const getParsedQuery = () => qs.parse(search);

    const update = (params) => {
        const currentQuery = getParsedQuery();
        const newQuery = clearEmptyFields({...currentQuery, ...params});        
        const updatedParams = qs.stringify(newQuery);
        history.push({search: updatedParams});
    }
    
    const clearEmptyFields = (obj) => {
        Object.keys(obj).forEach(key => {
            if (obj[key] == null || obj[key] === "")
                delete obj[key];
        });
        return obj;
    }

    return React.useMemo(() => [getParsedQuery(), update], [search]);
}

export {useQueryString};