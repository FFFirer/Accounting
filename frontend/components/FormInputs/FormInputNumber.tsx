import { FormControl, ValueAccessor } from "solid-form-context";
import { ComponentProps } from "solid-js";

export default (
  props: ComponentProps<"input"> 
) => {
  return (
    <FormControl
      control={"input"}
      controlProps={{ ...props, type: "number" }}
      onControlValueChanged={{
        eventName: "onchange",
        generateHandler: (setter) => (e) => setter?.(e.target.value),
      }}
    ></FormControl>
  );
};
