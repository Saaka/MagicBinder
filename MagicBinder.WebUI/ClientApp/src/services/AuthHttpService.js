import { HttpService, UserTokenService } from "Services";

class AuthHttpService extends HttpService {
    getHeaders = () => {
        let tokenService = new UserTokenService();
        let token = tokenService.getToken();

        return {
            "Authorization": `Bearer ${token}`
        };
    };
}

export { AuthHttpService };