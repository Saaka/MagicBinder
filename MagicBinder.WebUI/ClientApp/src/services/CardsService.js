import {AuthHttpService, Constants} from "Services";

export class CardsService {
    authHttpService = new AuthHttpService();

    getCards = (request) => {
        return this.authHttpService
            .post(Constants.ApiRoutes.CardsDatabase.LIST_SIMPLE, request)
            .then(resp => resp.data);
    };

    getCardDetails = (oracleId) => {
        return this.authHttpService
            .get(Constants.ApiRoutes.CardsDatabase.GET_DETAILS + oracleId)
            .then(resp => resp.data);
    };
}
