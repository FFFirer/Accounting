import {
  createPaginationState,
  Pagination,
} from "@frontend/components/Pagination";
import { JobClient } from "@frontend/services/client";
import { useClient } from "@frontend/utils/useHelper";
// import { Search } from "lucide-solid";
import { OcSearch3 } from "solid-icons/oc";
import { Form, FormControl, FormField } from "solid-form-context";
import {
  createEffect,
  createMemo,
  createResource,
  createSignal,
  ErrorBoundary,
  on,
} from "solid-js";
import { IndexColumn, Table, TableColumn, TableRow } from "solid-table-context";
import ErrorReset from "@frontend/components/ErrorReset";

const client = useClient(JobClient);

export default () => {
  const pagination = createPaginationState({ index: 1, size: 20 });
  const page = createMemo(() => pagination.value());

  const query = () => client.getApiJobQuery(page().index, page().size);

  const [resource, { mutate, refetch }] = createResource(query);

  createEffect(
    on(resource, (r) => pagination.setTotalCount(r?.totalCount ?? 0))
  );

  const jobs = createMemo(() => resource()?.datas);

  return (
    <div class="size-full flex flex-1 flex-col">
      <ErrorBoundary fallback={ErrorReset}>
        <div class="flex flex-row mb-2 gap-2">
          <Form>
            <FormField name={"asd"}>
              <FormControl
                control={"input"}
                controlProps={{
                  type: "text",
                  class: "input",
                  placeholder: "Full Name...",
                }}
                controlValuePropName="value"
                onControlValueChanged={{
                  eventName: "onchange",
                  generateHandler: (setter) => (e) => setter?.(e.target.value),
                }}
              ></FormControl>
            </FormField>
          </Form>
          <button type="button" class="btn btn-primary" onclick={refetch}>
            <OcSearch3 />
            查询
          </button>
        </div>
        <div class="grow overflow-x-auto rounded-box border border-base-content/5 bg-base-100">
          <Table
            class="table table-zebra table-pin-rows"
            datas={jobs()}
          >
            <IndexColumn cellClass="font-bold" />
            <TableColumn name={"namespace"} header="Namespace" />
            <TableColumn name={"fullName"} header="Full Name" />
          </Table>
        </div>
        <Pagination state={pagination}></Pagination>
      </ErrorBoundary>
    </div>
  );
};
