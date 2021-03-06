let RouteNames = class RouteNames {};

RouteNames.Root = "/";

RouteNames.Auth = "/auth";
RouteNames.Logout = "/auth/logout";

RouteNames.App = "/app";
RouteNames.Home = "/app/home";
RouteNames.Login = "/app/login";
RouteNames.Dashboard = "/app/dashboard";
RouteNames.Unauthorized = "/app/unauthorized";
RouteNames.About = "/app/about";
RouteNames.CardsDatabase = "/app/cards-database";
RouteNames.Card = "/app/card/";
RouteNames.CardRoute = RouteNames.Card + ":oracleId";

RouteNames.Admin = "/app/admin";

export {RouteNames};