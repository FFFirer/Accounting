import { createEffect, onMount, ParentProps } from "solid-js";
import { createBlazorNavLinkGuard } from "./BlazorNavLinkGuard";
import { useIsRouting } from "@solidjs/router";
import { createResetFocusOnNavigate } from "./ResetFocusOnNavigate";

export interface RouteGuardProps {}

export default (props: ParentProps<RouteGuardProps>) => {
  const routing = useIsRouting();
  const blazorNavLinkGuard = createBlazorNavLinkGuard();
  const resetFocusOnNavigate = createResetFocusOnNavigate({
    selector: "h1",
    routing: routing,
  });

  createEffect(blazorNavLinkGuard);
  createEffect(resetFocusOnNavigate);

  return props.children;
};
