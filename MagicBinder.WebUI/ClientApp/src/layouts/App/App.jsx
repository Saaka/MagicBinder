import React from 'react';
import {Routes , Route, Navigate} from "react-router-dom";
import {AuthRoute, AdminRoute, RegularRoute} from "components/routing";
import {AppNavbar} from "./AppNavbar/AppNavbar";
import appRoutes from "routes/appRoutes";
import "./App.scss";

function App(props) {

    return (
        <div className="app-container has-background-gradient">
            <AppNavbar {...props} user={props.user}/>
            <div className="app-container-content">
                <Routes>
                    {appRoutes.map((prop, key) => {
                        if (prop.redirect)
                            return (<Navigate from={prop.path} to={prop.to} key={key}
                                              updateUser={props.updateUser}/>);
                        else if (prop.requireAuth)
                            if (prop.requireAdmin)
                                return (
                                    <AdminRoute path={prop.path} component={prop.component} name={prop.name} key={key}
                                                user={props.user}
                                                updateUser={props.updateUser}/>);
                            else
                                return (
                                    <AuthRoute path={prop.path} component={prop.component} name={prop.name} key={key}
                                               user={props.user}
                                               updateUser={props.updateUser}/>);
                        else
                            return <RegularRoute path={prop.path} component={prop.component} name={prop.name} key={key}
                                                 user={props.user}
                                                 updateUser={props.updateUser}/>;
                    })}
                </Routes>
            </div>
        </div>
    );
}

export {App};