import { ApiException, ErrorDto, Result } from "@frontend/services/client";
import { withSolidContent } from "sweetalert2-solid-content";
import { solidSwal } from "./swal2";
import { Table, TableColumn } from "solid-table-context";
import { HttpValidationProblemDetails } from "./ProblemDetails";
import ErrorsTable from "@frontend/components/ErrorsTable";
import { JSXElement } from "solid-js";

export const DisplayIdentityError = (e: any[] | unknown) => {
  return Array.isArray(e)
    ? e.map((item) => item.description).join("\r\n")
    : ApiException.isApiException(e)
    ? e.message
    : "未知错误";
};

export const DisplayErrors = (errors?: ErrorDto[]) => {
  return solidSwal.fire({
    icon: "warning",
    title: "发生错误",
    html: <ErrorsTable errors={errors} />,
  });
};

export const ConvertError = (error: any) => {
  if (HttpValidationProblemDetails.isHttpValidationProblemDetails(error)) {
    return error.detail;
  }

  if(ApiException.isApiException(error)) {
    return error.message;
  }

  return error.message;
};

export const DisplayException = (error: any) => {
  if (HttpValidationProblemDetails.isHttpValidationProblemDetails(error)) {
    return solidSwal.fire({
      icon: "error",
      title: "发生异常",
      html: error.detail,
    });
  }

  if (ApiException.isApiException(error)) {
    return solidSwal.fire({
      icon: "error",
      title: "发生异常",
      html: error.message,
    });
  }

  return solidSwal.fire({
    icon: "error",
    title: "发生异常",
    html: error.message,
  });
};
