import {Constants} from "Services";

export class User {
    constructor(isLoggedIn, values = {}) {
        
        this.isLoggedIn = isLoggedIn;
        this.userGuid = values.userGuid;
        this.email = values.email;
        this.name = values.name;
        this.avatar = values.avatar;
        this.roles = values.roles;
    }

    isAdmin = () => this.isInRole(Constants.UserRoles.ADMIN);

    isInRole(roleName) {
        return !!this.roles && this.roles.indexOf(roleName) >= 0;
    };
}