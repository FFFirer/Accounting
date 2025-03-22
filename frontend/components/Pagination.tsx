import {
  Accessor,
  createEffect,
  createMemo,
  createSignal,
  JSX,
  mergeProps,
  on,
  Setter,
  splitProps,
} from "solid-js";
import { FaSolidAngleLeft, FaSolidAngleRight } from "solid-icons/fa";
import { cn } from "@frontend/utils/classHelper";

type PageChangedEventHandler = (index: number, size: number) => void;

export interface PaginationValueState {
  index: number;
  size: number;
}

export interface PaginationState {
  value: Accessor<PaginationValueState>;
  setValue: Setter<PaginationValueState>;
  totalCount: Accessor<number>;
  setTotalCount: Setter<number>;
  handlePrev: () => void;
  handleNext: () => void;
  disabledNext: Accessor<boolean>;
  disabledPrev: Accessor<boolean>;
}

export const createPaginationState = (
  initial?: PaginationValueState,
  sizeList?: number[],
  onPageChanged?: PageChangedEventHandler
) => {
  const merged = mergeProps({ index: 1, size: 20 }, initial);

  const [value, setValue] = createSignal(merged);
  const [totalCount, setTotalCount] = createSignal(0);

  createEffect(
    on(value, (v) => {
      onPageChanged?.(v.index, v.size);
    })
  );

  const handlePrev = () =>
    setValue((curr) => {
      return { ...curr, index: curr.index - 1 };
    });

  const disabledPrev = createMemo(() => value().index <= 1);
  const disabledNext = createMemo(
    () => value().index * value().size >= totalCount()
  );

  const handleNext = () =>
    setValue((curr) => {
      return { ...curr, index: curr.index + 1 };
    });

  return {
    value,
    setValue,
    totalCount,
    setTotalCount,
    handlePrev,
    handleNext,
    disabledNext,
    disabledPrev,
  };
};

export const Pagination = (
  props: { state: PaginationState } & JSX.HTMLAttributes<HTMLUListElement>
) => {
  const [local, others] = splitProps(props, ["state", "class", "classList"]);

  const handleEnter = (e: KeyboardEvent) => {
    if (e.code === "Enter") {
      local.state.setValue((curr) => {
        return {
          ...curr,
          index: e.target!.value,
        };
      });
    }
  };

  return (
    <ul class={cn(" menu-horizontal join", local.class, local.classList)} {...others}>
      <li class="pagination-total hover:shadow-none">
        <div class="btn bg-white border-none hover:shadow-none cursor-text">
          <span class="font-medium">Total</span>
          <span>{local.state.totalCount()}</span>
          <span class="font-medium">items</span>
        </div>
      </li>
      <li>
        <button
          disabled={local.state.disabledPrev()}
          class="btn btn-square"
          classList={{
            disabled: local.state.disabledPrev(),
          }}
          type="button"
          onClick={local.state.handlePrev}
        >
          <FaSolidAngleLeft />
        </button>
      </li>
      <li>
        <input
          type="number"
          class="input input-ghost w-[5rem] text-center"
          value={local.state.value().index}
          on:keydown={handleEnter}
        ></input>
      </li>
      <li>
        <button
          class="btn btn-square"
          type="button"
          onClick={local.state.handleNext}
          disabled={local.state.disabledNext()}
          classList={{
            disabled: local.state.disabledNext(),
          }}
        >
          <FaSolidAngleRight />
        </button>
      </li>
    </ul>
  );
};
