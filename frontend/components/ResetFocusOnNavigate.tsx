import { useIsRouting, useLocation } from "@solidjs/router";
import {
  Accessor,
  createEffect,
  createSignal,
  onMount,
  ParentProps,
} from "solid-js";

export interface FocusOnNavigateProps {
  selector: string;
  routing: Accessor<boolean>;
}

enum State {
  Init = 0,
  Ready = 1,
  Done = 2,
}

export const createResetFocusOnNavigate = (props: FocusOnNavigateProps) => {
  const [state, setState] = createSignal(State.Init);

  const activeElement = () => document.activeElement;

  const focusOnSelector = () => {
    const focusElement = document.querySelector(props.selector) as any;

    if (focusElement) {
      focusElement.focus();
    }
  };

  const resetFocus = () => {
    const target = activeElement()

    if(target){
      // document.body.click();

      const newTarget = document.activeElement;
    }
  };

  return () => {
    switch (state()) {
      case State.Init:
        if (props.routing()) {
          setState(State.Ready);
        } else {
          setState(State.Done);
        }

        break;

      case State.Ready:
        if (props.routing() === false) {
          setState(State.Done);
          resetFocus();
        }

        break;

      case State.Done:
        if (props.routing()) {
          setState(State.Ready);
        }

        break;
      default:
        break;
    }
  };
};
