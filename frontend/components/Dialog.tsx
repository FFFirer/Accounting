import { cn } from "@frontend/utils/classHelper";
import {
  Accessor,
  createEffect,
  createMemo,
  createRenderEffect,
  createSignal,
  JSX,
  mergeProps,
  on,
  ParentProps,
  Ref,
  Setter,
  Show,
  splitProps,
} from "solid-js";
import { Portal } from "solid-js/web";

export interface DialogProps
  extends Omit<JSX.DialogHtmlAttributes<HTMLDialogElement>, "onClose"> {
  backdrop?: "static";
  onShow?: () => void;
  shell?: IDialogShell;
  onClose?: () => void;
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

export interface IDialogShell {
  show: () => void;
  close: () => void;
  open: Accessor<boolean>;
  setOpen: Setter<boolean>;
  ref: Accessor<HTMLDialogElement | undefined>;
  setRef: Setter<HTMLDialogElement | undefined>;
}

export const useDialog: () => IDialogShell = () => {
  const [ref, setRef] = createSignal<HTMLDialogElement>();

  const [open, setOpen] = createSignal(false);

  const show = () => {
    ref()?.showModal();
    setOpen(true);
  };

  const close = () => {
    ref()?.close();
    setOpen(false);
  };

  return {
    show,
    close,
    ref,
    setRef,
    open,
    setOpen,
  };
};

export const Dialog = (props: ParentProps<DialogProps>) => {
  const [local, others] = splitProps(
    mergeProps({ shell: useDialog() }, props),
    ["class", "classList", "backdrop", "onShow", "shell", "onClose"]
  );

  const actualOpen = createMemo(() => {
    if (local.shell.open()) {
    } else {
    }

    return local.shell.ref()?.hasAttribute("open");
  });

  createEffect(
    on(actualOpen, (v) => {
      if (v) {
        setTimeout(() => local.onShow?.(), 50); // 防止要对dialog内容操作但是还未展示
      }
    })
  );

  const handleClose = () => {
    local.onClose?.();
    local.shell.setOpen(false);
  };

  return (
    <Portal mount={document.querySelector("body")!}>
      <dialog
        {...others}
        ref={local.shell.setRef}
        class={cn("modal", local.class, local.classList)}
        onClose={handleClose}
      >
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

export const AutoFocusFormInputs = (el?: HTMLElement) => {
  return () => el?.querySelector("[autofocus]")?.focus();
}