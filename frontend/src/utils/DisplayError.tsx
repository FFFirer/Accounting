import { ApiException, IdentityError } from "~/services/client";

export const DisplayIdentityError = (e: IdentityError[] | unknown) => {
  return Array.isArray(e)
    ? e.map((item) => item.description).join("\r\n")
    : ApiException.isApiException(e) 
        ? e.message
        : "未知错误";
};
