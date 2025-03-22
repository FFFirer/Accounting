import { FormControl, ValueAccessor } from "solid-form-context";
import { ComponentProps, splitProps } from "solid-js";

export default (
  props: ComponentProps<"input"> 
) => {
  return (
    <FormControl
      control={"input"}
      controlProps={{ ...props, type: "text" }}
      onControlValueChanged={{
        eventName: "onchange",
        generateHandler: (setter) => (e) => setter?.(e.target.value),
      }}
    ></FormControl>
  );
};
