import { RouteDefinition } from "@solidjs/router";
import { Component, lazy } from "solid-js";

export const Home: Component = () => {
  return <div>Hello world</div>;
};

export const HomeRoute: RouteDefinition = {
  path: "/",
  component: Home,
};
