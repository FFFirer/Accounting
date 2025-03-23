import { cn } from "@frontend/utils/classHelper";
import {
  createEffect,
  createMemo,
  createRenderEffect,
  JSX,
  on,
  ParentProps,
  Ref,
  Show,
  splitProps,
} from "solid-js";
import { Portal } from "solid-js/web";

export interface DialogProps
  extends JSX.DialogHtmlAttributes<HTMLDialogElement> {
  backdrop?: "static";
}

export const DialogAction = (
  props: ParentProps<JSX.HTMLAttributes<HTMLDivElement>>
) => {
  const [local, others] = splitProps(props, ["class", "classList"]);
  return (
    <div
      class={cn("modal-action", local.class, local.classList)}
      {...others}
    ></div>
  );
};

export const DialogCloseButton = (
  props: ParentProps<JSX.ButtonHTMLAttributes<HTMLButtonElement>>
) => {
  const [local, others] = splitProps(props, ["children", "class", "classList"]);

  return (
    <form method="dialog">
      <button {...others} class={cn("btn", local.class, local.classList)}>
        {local.children ?? "Close"}
      </button>
    </form>
  );
};

export const DialogTopCloseButton = (
  props: ParentProps<JSX.ButtonHTMLAttributes<HTMLButtonElement>>
) => {
  const [local, others] = splitProps(props, ["class", "classList", "children"]);

  return (
    <form method="dialog">
      <button
        class={cn(
          "btn btn-sm btn-circle btn-ghost absolute right-2 top-2",
          local.class,
          local.classList
        )}
        {...others}
      >
        {props.children ?? "✕"}
      </button>
    </form>
  );
};

export const DialogContainer = () => {
  return <div style={{ "z-index": "100" }} id="dialog_container"></div>;
};

export const Dialog = (props: ParentProps<DialogProps>) => {
  const [local, others] = splitProps(props, ["class", "classList", "backdrop"]);

  return (
    <Portal mount={document.querySelector("body")!}>
      <dialog {...others} class={cn("modal", local.class, local.classList)}>
        <div class="modal-box">{props.children}</div>
        <Show when={local.backdrop !== "static"}>
          <form method="dialog" class="modal-backdrop">
            <button>close</button>
          </form>
        </Show>
      </dialog>
    </Portal>
  );
};
