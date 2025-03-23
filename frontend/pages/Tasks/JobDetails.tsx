import ErrorReset from "@frontend/components/ErrorReset";
import Page from "@frontend/components/Page";
import {
  createPaginationState,
  Pagination,
} from "@frontend/components/Pagination";
import { JobDetailClient } from "@frontend/services/client";
import { useClient } from "@frontend/utils/useHelper";
import { Form } from "solid-form-context";
import { OcSearch3 } from "solid-icons/oc";
import { createEffect, createResource, ErrorBoundary, on } from "solid-js";
import { IndexColumn, Table, TableColumn } from "solid-table-context";

const client = useClient(JobDetailClient);

export default () => {
  const pagination = createPaginationState();

  const query = () =>
    client.getApiJobDetailQuery(
      pagination.value().index,
      pagination.value().size
    );

  const [resource, { mutate, refetch }] = createResource(query);

  createEffect(
    on(resource, (r) => pagination.setTotalCount(r?.totalCount ?? 0))
  );

  return (
    <Page>
      <ErrorBoundary fallback={ErrorReset}>
        <div class="flex flex-row mb-2 gap-2">
          <button type="button" class="btn btn-primary" onclick={refetch}>
            <OcSearch3 /> 查询
          </button>
        </div>
        <div class="grow overflow-auto rounded-box border border-base-content/5 bg-base-100">
          <Table
            class="table table-zebra table-pin-rows"
            datas={resource()?.datas}
          >
            <IndexColumn class="w-[10px]" header="No." />
            <TableColumn name={"jobGroup"} header="Group"></TableColumn>
            <TableColumn name={"jobName"} header="Name" />
            <TableColumn name={"jobClassName"} header="Class" />
          </Table>
        </div>
        <Pagination class=" rounded-box" state={pagination} />
      </ErrorBoundary>
    </Page>
  );
};
