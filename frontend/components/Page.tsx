import { cn } from "@frontend/utils/classHelper";
import { JSXElement, ParentProps, Show } from "solid-js";

export interface PageProps {
  title?: string;
  class?: string;
}

export default (props: ParentProps<PageProps>) => {
  return (
    <div class={cn("size-full flex flex-col flex-1", props.class)}>
      <Show when={props.title}>
        <h1 class="mb-2">{props.title}</h1>
      </Show>
      {props.children}
    </div>
  );
};
