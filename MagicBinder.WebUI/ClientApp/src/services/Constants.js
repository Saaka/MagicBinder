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
    static get FACEBOOK() {
        return "auth/facebook";
    }
    static get GET_USER() {
        return "auth/user";
    }
};

Constants.ApiRoutes.Admin = class AdminRoutes {
    
};

export {Constants};