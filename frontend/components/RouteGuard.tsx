import { createEffect, ParentProps } from "solid-js";
import { createBlazorNavLinkGuard } from "./BlazorNavLinkGuard";

export interface RouteGuardProps {}

export default (props: ParentProps<RouteGuardProps>) => {
  const blazorNavLinkGuard = createBlazorNavLinkGuard();

  createEffect(blazorNavLinkGuard);

  return props.children;
};
