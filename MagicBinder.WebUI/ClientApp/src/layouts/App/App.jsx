import React from 'react';
import {Switch, Redirect} from "react-router";
import {AuthRoute, AdminRoute, RegularRoute} from "components/routing";
import {AppNavbar} from "./AppNavbar/AppNavbar";
import {AppFooter} from "./AppFooter/AppFooter";
import {ResponsiveContainer} from "components/common";
import appRoutes from "routes/appRoutes";
import "./App.scss";

function App(props) {

    return (
        <React.Fragment>
            <div className="app-container has-custom-background">
                <AppNavbar {...props} user={props.user}/>
                <div className="app-container-content">
                    <ResponsiveContainer>
                        <Switch>
                            {appRoutes.map((prop, key) => {
                                if (prop.redirect)
                                    return (<Redirect from={prop.path} to={prop.to} key={key}
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
                        </Switch>
                    </ResponsiveContainer>
                </div>
            </div>
            <AppFooter/>
        </React.Fragment>
    );
}

export {App};