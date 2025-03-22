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

  const disabledPrev = createMemo(() => value().index == 1);
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
  props: { state: PaginationState } & JSX.HTMLAttributes<HTMLDivElement>
) => {
  const [local, others] = splitProps(props, ["state"]);

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
    <div {...others}>
      <ul class=" menu-horizontal">
        <li class="pagination-total">
          <span>Total</span>
          <span>{local.state.totalCount()}</span>
        </li>
        <li>
          <button
            disabled={local.state.disabledPrev()}
            class="btn btn-outline"
            classList={{
              disabled: local.state.disabledPrev(),
            }}
            type="button"
            onClick={local.state.handlePrev}
          >
            Prev
          </button>
        </li>
        <li>
          <input
            type="number"
            class="input w-[5rem] text-center"
            value={local.state.value().index}
            on:keydown={handleEnter}
          ></input>
        </li>
        <li>
          <button
            class="btn btn-outline"
            type="button"
            onClick={local.state.handleNext}
            disabled={local.state.disabledNext()}
            classList={{
              disabled: local.state.disabledNext(),
            }}
          >
            Next
          </button>
        </li>
      </ul>
    </div>
  );
};
