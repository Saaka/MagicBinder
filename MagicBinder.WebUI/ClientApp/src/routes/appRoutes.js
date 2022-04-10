import {Admin, Dashboard, Unauthorized, About, Login} from "views/exports";
import {Products as AdminProducts, ProductCategories, UnitsOfMeasures, ProductForm as AdminProduct} from "views/Admin/exports";
import {RouteNames} from "./names";

const appRoutes = [
    {
        requireAuth: true,
        requireAdmin: false,
        path: RouteNames.Dashboard,
        component: Dashboard,
        name: "Dashboard",
        icon: "home"
    },
    {
        requireAuth: true,
        requireAdmin: true,
        path: RouteNames.Admin,
        component: Admin,
        name: "Admin",
        icon: "admin"
    },
    {
        requireAuth: false,
        requireAdmin: false,
        path: RouteNames.About,
        component: About,
        name: "About",
        icon: "about"
    }, 
    {
        requireAuth: false,
        requireAdmin: false,
        path: RouteNames.Login,
        component: Login,
        name: "Login"
    },
    {
        requireAuth: false,
        path: RouteNames.Unauthorized,
        component: Unauthorized,
        name: "Unauthorized"
    },
    {
        requireAuth: false,
        requireAdmin: false,
        path: RouteNames.App,
        component: About,
        name: "About",
        icon: "about"
    }
];

export default appRoutes;