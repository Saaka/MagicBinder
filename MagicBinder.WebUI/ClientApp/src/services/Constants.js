let Constants = class Constants {};

Constants.UserRoles = class UserRoles {
    static get ADMIN() {
        return "Admin";
    }
};

Constants.ApiRoutes = class ApiRoutes {
    static get GOOGLE() {
        return "auth/google";
    }
    static get GET_USER() {
        return "auth/user";
    }
};

Constants.ApiRoutes.CardsDatabase = class CardsDatabaseRoutes {
    static get LIST_SIMPLE() {
        return "cards/list/simple";
    }
    static get GET_DETAILS() {
        return "cards/";
    }
}

Constants.ApiRoutes.Inventories = class InventoriesRoutes {
    static get GET_CARD_INVENTORY() {
        return "inventories/cards/";
    }
}

Constants.ApiRoutes.Admin = class AdminRoutes {
    
};

export {Constants};