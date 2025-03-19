import { RouteDefinition } from "@solidjs/router";
import Home from "./pages/Home";
import About from "./pages/About";
import { TaskList } from "./pages/Tasks/List";

export const Routes: RouteDefinition[] = [
  { path: "/", component: Home },
  { path: "/about", component: About },
  { path: "/Tasks", children: [{ path: "/", component: TaskList }] },
];
