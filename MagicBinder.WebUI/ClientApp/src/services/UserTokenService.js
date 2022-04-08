import decode from "jwt-decode";

export class UserTokenService {
    tokenName = "user_token_id_dts";

    setToken = (token) => localStorage.setItem(this.tokenName, token);

    getToken = () => localStorage.getItem(this.tokenName);

    removeToken = () => localStorage.removeItem(this.tokenName);

    getTokenData = () => {
        let token = this.getToken();
        return !!token ? decode(token) : null;
    };

    isTokenValid = () => {
        const token = this.getToken();
        return !!token && !this.isTokenExpired(token);
    };

    isTokenExpired = (token) => {
        const decoded = decode(token);
        return decoded.exp < Date.now() / 1000;
    };
}