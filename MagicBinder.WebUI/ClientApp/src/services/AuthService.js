import {AuthHttpService, Constants, HttpService, UserTokenService} from "Services";
import {User} from "Models";

export class AuthService {
    httpService = new HttpService();
    authHttpService = new AuthHttpService();
    tokenService = new UserTokenService();

    isLoggedIn = () => this.tokenService.isTokenValid();

    logout = () => this.tokenService.removeToken();

    loginWithGoogle = (token) => {
        return this.httpService
            .post(Constants.ApiRoutes.GOOGLE, {
                googleToken: token
            })
            .then(response => {
                return this.onLogin(response);
            });
    };

    onLogin = (response) => {

        this.tokenService
            .setToken(response.data.token);

        return this.getUser();
    };

    getUser = () => {
        let authData = this.tokenService
            .getTokenData();

        return this.getUserFromTokenData(authData);
    };

    getUserFromTokenData = (authData) => {
        if (authData === null)
            return new User(false);

        return new User(true, {
            userId: authData.sub,
            name: authData.name,
            email: authData.email,
            avatar: authData.avatar,
            roles: authData.role
        });
    };
}