import { ApiException } from "@frontend/services/client";

export const DisplayIdentityError = (e: any[] | unknown) => {
  return Array.isArray(e)
    ? e.map((item) => item.description).join("\r\n")
    : ApiException.isApiException(e) 
        ? e.message
        : "未知错误";
};
