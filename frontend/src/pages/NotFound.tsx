import { A, RouteDefinition } from "@solidjs/router";

export const NotFound = () => {
  return (
    <div>
      <A href="/">Home</A>
    </div>
  );
};

export const NotFoundRoute: RouteDefinition = {
  path: "**catchall",
  component: NotFound,
};
