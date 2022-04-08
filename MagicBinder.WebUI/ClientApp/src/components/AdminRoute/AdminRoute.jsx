import React from "react";
import {Route, Redirect} from "react-router-dom";
import {RouteNames} from "routes/names";

function AdminRoute(
    {
        component: Component,
        user,
        ...data
    }) {

    return (
        <Route {...data}
               render={props => {
                   if (user && user.isLoggedIn && user.isAdmin())
                       return (<Component {...props} user={user}/>);
                   else if (user && user.isLoggedIn && !user.isAdmin())
                       return (<Redirect to={{
                               pathname: RouteNames.Unauthorized
                           }}/>
                       );
                   else return (
                           <Redirect to={{
                               pathname: RouteNames.Login,
                               search: `?redirect=${props.location.pathname}`,
                               state: {
                                   from: props.location
                               }
                           }}/>
                       );
               }}/>
    );
}

export {AdminRoute};