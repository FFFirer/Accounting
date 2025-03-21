import { JobClient } from "@frontend/services/client";
import { useClient } from "@frontend/utils/useHelper";
import { Search } from "lucide-solid";
import { Form, FormControl, FormField } from "solid-form-context";
import { IndexColumn, Table, TableColumn, TableRow } from "solid-table-context";

const client = useClient(JobClient);

export default () => {
  const query = (page: number, size: number) =>
    client.getApiJobQuery(page, size);

  return (
    <div class="size-full flex flex-1 flex-col">
      <div class="flex flex-row mb-2 gap-2">
        <Form>
          <FormField name={"asd"}>
            <FormControl
              control={"input"}
              controlProps={{ type: "text", class: "input", placeholder: 'Full Name...' }}
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
      <div class="grow overflow-x-auto rounded-box border border-base-content/5 bg-base-100">
        <Table class="table table-zebra table-pin-rows">
          <TableRow class="hover:bg-base-300">
            <IndexColumn cellClass="font-bold" />
            <TableColumn name={"namespace"} header="Namespace" />
            <TableColumn name={"fullName"} header="Full Name" />
          </TableRow>
        </Table>
      </div>
    </div>
  );
};
