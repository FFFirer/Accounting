import {
  createMemo,
  createSignal,
  JSXElement,
  mergeProps,
  onCleanup,
  onMount,
  ParentProps,
  Show,
  type ParentComponent,
} from "solid-js";
import { addClass, getStyle, removeClass } from "./dom";
import Spinner from "./Spinner";

export interface LoadingProps {
  show?: boolean;
  spinner?: JSXElement;
  delay?: number;
}

export default (p: ParentProps<LoadingProps>) => {
  const props = mergeProps({ delay: 500 }, p);

  const show = createMemo(() => props.show);

  const parentClassList = createMemo(() => {
    return {
      "loading-parent--hidden": show(),
    };
  });

  const maskClassList = createMemo(() => {
    return {
      show: show(),
    };
  });

  const [ref, setRef] = createSignal<HTMLDivElement | null | undefined>(
    undefined
  );

  const [parent, setParent] = createSignal<HTMLElement | null | undefined>(
    undefined
  );

  const loadingSpineer = createMemo(() => {
    return props.spinner ?? <Spinner class="loading-spinner"></Spinner>
  });

  onMount(() => {
    const el = ref();
    const p = el?.parentElement;

    if (p) {
      const elOriginalPosition = getStyle(el);

      if (
        elOriginalPosition !== "absolute" &&
        elOriginalPosition !== "fixed" &&
        elOriginalPosition !== "sticky"
      ) {
        addClass(p, "loading-parent--relative");
      }

      setParent(p);
    }
  });

  onCleanup(() => {
    const p = parent();
    if (!p) {
      return;
    }
    removeClass(p, "loading-parent--relative");
    // removeClass(p, "loading-parent--hidden");
  });

  return (
    <>
      {props.children}
      <div
        ref={setRef}
        class="loading-mask fade bg-base-100/50"
        style={{
          display: show() ? "block" : "none",
        }}
        classList={maskClassList()}
      >
        <div
          class="loading-mask-content flex items-center justify-center h-full"
        >
          <Show when={props.spinner} fallback={loadingSpineer()}>
            {props.spinner}
          </Show>
        </div>
      </div>
    </>
  );
};
