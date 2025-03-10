import { RouteDefinition } from "@solidjs/router";
import Home from "./pages/Home";
import About from "./pages/About";

export const Routes: RouteDefinition[] = [
  { path: "/", component: Home },
  { path: "/about", component: About },
];
