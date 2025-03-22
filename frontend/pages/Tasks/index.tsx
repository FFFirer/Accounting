import { BlazorNavLinkGuard } from "@frontend/components/BlazorNavLinkGuard";
import { Navigate, RouteDefinition, Router } from "@solidjs/router";
import { lazy } from "solid-js";
import { render } from "solid-js/web";

const AppTasksRoutes: RouteDefinition[] = [
  {
    path: "/",
    component: () => <Navigate href={'/Jobs'} />,
  },
  {
    path: "/Jobs",
    component: lazy(() => import("./List"))
  },
  {
    path: "/JobDetails",
    component: lazy(() => import("./JobDetails"))
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
