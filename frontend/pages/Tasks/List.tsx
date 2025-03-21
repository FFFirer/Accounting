import { Search } from "lucide-solid";
import { Form, FormControl, FormField } from "solid-form-context";

export default () => {
  return (
    <div class="size-full flex flex-1 flex-col">
      <div class="flex flex-row mb-2 gap-2">
        <Form>
          <FormField name={"asd"}>
            <FormControl
              control={"input"}
              controlValuePropName="value"
              onControlValueChanged={{
                eventName: "onchange",
                generateHandler: (setter) => (e) => setter?.(e.target.value),
              }}
            ></FormControl>
          </FormField>
        </Form>
        <button type="button" class="btn btn-primary">
          <Search />
          查询
        </button>
      </div>
      <div class="grow overflow-x-auto rounded-box border border-base-content/5 bg-base-100"></div>
    </div>
  );
};
