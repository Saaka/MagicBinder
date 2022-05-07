import React, {useState} from "react";
import {GoogleLogin} from "react-google-login";
import {ConfigService, AuthService} from "Services";
import {Icon, Loader} from "components/common"
import "./GoogleLogin.scss";

function LoginWithGoogle(props) {
    const configService = new ConfigService();
    const authService = new AuthService();
    const [showLoader, toggleLoader] = useState(false);

    function onLogin(response) {
        toggleLoader(true);
        authService
            .loginWithGoogle(response.tokenId)
            .then(props.onLoggedIn)
            .catch(onLoginFail);
    }

    function onLoginFail(response) {
        props.onError(response);
        toggleLoader(false);
    }

    function onClick(renderProps) {
        toggleLoader(true);
        renderProps.onClick();
        props.onLogin();
    }

    const renderLoader = () => (<Loader size="xs" dark/>);

    return (
        <React.Fragment>
            <GoogleLogin clientId={configService.GoogleClientId}
                         onSuccess={onLogin}
                         onFailure={onLoginFail}
                         disabled={props.disabled}

                         render={renderProps => (
                             <React.Fragment>
                                 <div>
                                     <button className="button is-light"
                                             disabled={props.disabled}
                                             onClick={() => onClick(renderProps)}><Icon name="google"/><span
                                         className="button-text">Sign in with Google</span>
                                     </button>
                                     {showLoader ? <span className="button-loader">{renderLoader()}</span> : ""}
                                 </div>
                             </React.Fragment>
                         )}/>
        </React.Fragment>
    )
}

export {LoginWithGoogle as GoogleLogin};