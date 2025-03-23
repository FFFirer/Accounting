import { BlazorNavLinkGuard } from "@frontend/components/BlazorNavLinkGuard";
import { DialogContainer } from "@frontend/components/Dialog";
import RouteGuard from "@frontend/components/RouteGuard";
import { Navigate, RouteDefinition, Router } from "@solidjs/router";
import { lazy } from "solid-js";
import { render } from "solid-js/web";

const AppTasksRoutes: RouteDefinition[] = [
  {
    path: "/",
    component: () => <Navigate href={"/Jobs"} />,
  },
  {
    path: "/Jobs",
    component: lazy(() => import("./List")),
  },
  {
    path: "/JobDetails",
    component: lazy(() => import("./JobDetails")),
  },
];
const AppTasksRoutePrefix = "/app/Tasks";

const AppTasks = () => {
  return (
    <>
      <DialogContainer />
      <Router base={AppTasksRoutePrefix} root={RouteGuard}>
        {AppTasksRoutes}
      </Router>
    </>
  );
};

const renderAppTasks = (el: HTMLElement) => {
  console.debug("render app [Tasks]", el);

  render(() => <AppTasks />, el);
};

export { renderAppTasks, AppTasks, AppTasksRoutes, AppTasksRoutePrefix };
