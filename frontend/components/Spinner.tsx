import { cn } from "@frontend/utils/classHelper";

export default (props: { class?: string }) => {
  return <span class={cn("loading", props.class)}></span>;
};
