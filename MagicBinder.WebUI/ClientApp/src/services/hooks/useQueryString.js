import React from "react";
import {useHistory} from "react-router-dom";
import qs from "query-string";

function useQueryString() {
    const history = useHistory();
    
    function update(param) {
        const queryParams =  qs.parse(history.location.search);
        const updatedParams = {...queryParams, ...param};
        history.push({search: qs.stringify(updatedParams)});
    }
    
    return {
        update: update
    }
}

export {useQueryString};