import { RouteDefinition } from "@solidjs/router";
import { HomeRoute } from "./pages/Home";
import { LoginRoute } from "./pages/Login";
import { NotFoundRoute } from "./pages/NotFound";

export const routes: RouteDefinition[] = [
    HomeRoute,
    LoginRoute,
    NotFoundRoute
];