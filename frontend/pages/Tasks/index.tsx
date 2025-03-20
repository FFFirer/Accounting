import { BlazorNavLinkGuard } from "@frontend/components/BlazorNavLinkGuard";
import { RouteDefinition, Router } from "@solidjs/router";
import { lazy } from "solid-js";
import { render } from "solid-js/web";

const AppTasksRoutes: RouteDefinition[] = [
  {
    path: "/",
    component: lazy(() => import("./List")),
  },
  {
    path: "/Demo1",
    component: () => <h1>Demo1</h1>
  },
  {
    path: "/Demo2",
    component: () => <h1>Demo2</h1>
  }
];
const AppTasksRoutePrefix = "/app/Tasks";

const AppTasks = () => {
  return <Router base={AppTasksRoutePrefix} root={BlazorNavLinkGuard}>{AppTasksRoutes}</Router>;
};

const renderAppTasks = (el: HTMLElement) => {
  console.debug('render app [Tasks]', el);

  render(() => <AppTasks />, el)
};

export { renderAppTasks, AppTasks, AppTasksRoutes, AppTasksRoutePrefix };
