import { useIsRouting, useLocation } from "@solidjs/router";
import {
  ComponentProps,
  createEffect,
  JSX,
  onCleanup,
  onMount,
  ParentComponent,
} from "solid-js";

/** Route Guard，针对未启用Blazor增强导航的连接的匹配状态进行更新 */
export const BlazorNavLinkGuard: ParentComponent = (props) => {
  const loc = useLocation();
  const routing = useIsRouting();

  createEffect(() => {
    const disabledBlazorEnhanceNavLinks = document.querySelectorAll(
      "a[data-enhance-nav=false][data-not-solid]"
    );

    disabledBlazorEnhanceNavLinks.forEach((el: Element) => {
      const anchor = el as HTMLAnchorElement;
      const activeClass = anchor.dataset["activeClass"] || "active";

      const url = new URL(anchor.href);
      const match = loc.pathname.startsWith(url.pathname);

      if (match) {
        anchor.classList.add(activeClass!);
      } else {
        anchor.classList.remove(activeClass);
      }
    });
  });

  return props.children;
};
