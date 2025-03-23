import { ErrorDto } from "@frontend/services/client";
import { Show } from "solid-js";
import { Table, TableColumn } from "solid-table-context";

export default (props: { errors?: ErrorDto[] }) => {
  return (
    <Show when={props.errors}>
      <Table class="table table-zebra table-pin-rows" datas={errors}>
        <TableColumn name={"code"} header="Code" />
        <TableColumn name={"description"} header="Description" />
      </Table>
    </Show>
  );
};
