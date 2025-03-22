import {
  createPaginationState,
  Pagination,
} from "@frontend/components/Pagination";
import { JobClient, JobDefinationDto } from "@frontend/services/client";
import { useClient } from "@frontend/utils/useHelper";
import { OcSearch3 } from "solid-icons/oc";
import {
  Form,
  FormControl,
  FormField,
  IFormInstance,
} from "solid-form-context";
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
import { BsPlus } from "solid-icons/bs";
import FormSubmit from "@frontend/components/FormInputs/FormSubmit";
import FormInputText from "@frontend/components/FormInputs/FormInputText";
import {
  Dialog,
  DialogAction,
  DialogCloseButton,
  DialogTopCloseButton,
} from "@frontend/components/Dialog";

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

  let modalRef: any;

  const closeModal = () => modalRef?.close();

  const createJobDetail = (data: JobDefinationDto) => {
    form()?.setValue({
      className: data.fullName,
    });
    modalRef?.showModal();
  };

  const handleSubmit = (values: any) => {
    console.log("submit!", values);
    modalRef?.close();
  };

  const [form, setForm] = createSignal<IFormInstance | undefined>();

  const handleClose = () => {
    console.log("modal closed!");
    form()?.setValue({});
  };

  return (
    <div class="size-full flex flex-1 flex-col">
      <ErrorBoundary fallback={ErrorReset}>
        <div class="flex flex-row mb-2 gap-2">
          <button type="button" class="btn btn-primary" onclick={refetch}>
            <OcSearch3 />
            查询
          </button>
        </div>
        <div class="grow overflow-x-auto rounded-box border border-base-content/5 bg-base-100">
          <Table class="table table-zebra table-pin-rows" datas={jobs()}>
            <IndexColumn cellClass="font-bold" />
            <TableColumn name={"namespace"} header="Namespace" />
            <TableColumn name={"fullName"} header="Full Name" />
            <TableColumn header={"Operations"}>
              {(data) => (
                <button
                  type="button"
                  class="btn btn-primary btn-xs"
                  onClick={() => createJobDetail(data)}
                >
                  <BsPlus size={20} />
                  添加实例
                </button>
              )}
            </TableColumn>
          </Table>
        </div>
        <Pagination class=" rounded-box" state={pagination}></Pagination>
        <Dialog ref={modalRef} onClose={handleClose}>
          <DialogTopCloseButton />

          <h3 class="text-lg font-bold mb-3">添加一个新的任务实例</h3>

          <Form onRef={setForm} onSubmit={handleSubmit}>
            <FormField name={"className"}>
              <div class="mb-2">
                <label class="input">
                  Class Name
                  <FormInputText class="grow" readonly></FormInputText>
                </label>
              </div>
            </FormField>
            <FormField name={"group"}>
              <div class="mb-2">
                <label class="input">
                  Group
                  <FormInputText
                    class="grow"
                    placeholder="给任务一个分组名称"
                  ></FormInputText>
                </label>
              </div>
            </FormField>
            <FormField name={"name"}>
              <div class="mb-2">
                <label class="input">
                  Name
                  <FormInputText class="grow" placeholder="给任务起个名字" />
                </label>
              </div>
            </FormField>

            <DialogAction>
              <FormSubmit class="btn btn-outline">创建</FormSubmit>
              <DialogCloseButton class=" relative">取消</DialogCloseButton>
            </DialogAction>
          </Form>
        </Dialog>
      </ErrorBoundary>
    </div>
  );
};
