import {AuthHttpService, Constants} from "Services";

export class InventoriesService {
    authHttpService = new AuthHttpService();

    getCardInventory = (oracleId) => {
        return this.authHttpService
            .get(Constants.ApiRoutes.Inventories.GET_CARD_INVENTORY + oracleId)
            .then(resp => resp.data);
    };

    saveCardInventory = (request) => {
        return this.authHttpService
            .put(Constants.ApiRoutes.Inventories.SAVE_CARD_INVENTORY, request)
            .then(resp => resp.data);
    };
}