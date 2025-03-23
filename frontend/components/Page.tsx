import { cn } from "@frontend/utils/classHelper";
import {
  createRenderEffect,
  JSX,
  JSXElement,
  mergeProps,
  onMount,
  ParentProps,
  Show,
  splitProps,
} from "solid-js";

export interface PageProps extends JSX.HTMLAttributes<HTMLDivElement> {
  autoFocus?: boolean;
}

export default (props: ParentProps<PageProps>) => {
  const [local, others] = splitProps(
    mergeProps({ tabIndex: 0, autoFocus: true }, props),
    ["title", "class", "classList", "ref", "autoFocus", "children"]
  );

  let ref: any;

  onMount(() => {
    if (local.autoFocus && ref) {
      ref!.focus();
    }
  });

  return (
    <div
      ref={ref}
      {...others}
      class={cn(
        " focus:outline-0 size-full flex flex-col flex-1",
        local.class,
        local.classList
      )}
    >
      <Show when={local.title}>
        <h1 class="mb-2">{local.title}</h1>
      </Show>
      {local.children}
    </div>
  );
};
