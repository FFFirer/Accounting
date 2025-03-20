import { RouteDefinition, Router } from "@solidjs/router";
import { lazy } from "solid-js";
import { render } from "solid-js/web";

const AppTasksRoutes: RouteDefinition[] = [
  {
    path: "/",
    component: lazy(() => import("./List")),
  },
];
const AppTasksRoutePrefix = "/app/Tasks";

const AppTasks = () => {
  return <Router base={AppTasksRoutePrefix}>{AppTasksRoutes}</Router>;
};

const renderAppTasks = (el: HTMLElement) => render(() => <AppTasks />, el);

export { renderAppTasks, AppTasks, AppTasksRoutes, AppTasksRoutePrefix };
