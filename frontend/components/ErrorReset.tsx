import { ErrorBoundary, ParentProps } from "solid-js";

export default (error: any, reset: any) => {
  return (
    <div>
      <p>Something went wrong: {error.message}</p>
      <button class="btn btn-error" onClick={reset}>
        Reset
      </button>
    </div>
  );
};
