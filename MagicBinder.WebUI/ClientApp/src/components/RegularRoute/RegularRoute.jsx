import React from "react";
import {Route} from "react-router-dom";

function RegularRoute(
    {
        component: Component,
        user,
        updateUser,
        ...data
    }) {
    return (
        <Route {...data}
               render={props => (
                   <Component {...props} user={user} updateUser={updateUser}/>
               )}/>
    );
}

export {RegularRoute};