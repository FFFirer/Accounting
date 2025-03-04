const API_SERVER_URL = import.meta.env.VITE_API_SERVER_URL;

export interface FetchClient {
  fetch(url: RequestInfo, init?: RequestInit): Promise<Response>;
}
export class FetchException extends Error {
  constructor(message: string, stack: any) {
    super();

    this.message = message;
    this.stack = stack;
  }

  protected isFetchException = true;

  static isFetchException(obj: any): obj is FetchException {
    return obj.isFetchException === true;
  }
}

const throwException = (err: any) => {
  throw new FetchException(err.message, err.stack);
};


export const fetchClient: FetchClient = {
  fetch(url, init) {
    return window.fetch(url, init)
      .catch(throwException);
  },
};


const useHttp = () => {
  return fetchClient;
};

const useBaseUrl = () => {
  return API_SERVER_URL;
};

export const useApiConfig = () => {
  const baseUrl = useBaseUrl();

  return {
    baseUrl: (baseUrl ?? "")?.trim() === "" ? window.location.origin : baseUrl,
    http: useHttp(),
  };
};

/**
 * 根据client类型创建实例
 * @param clientType client类型
 * @returns
 */
export function useClient<TClient>(
  clientType: new (baseUrl?: string, http?: FetchClient) => TClient,
): TClient {
  const { baseUrl, http } = useApiConfig();

  return new clientType(baseUrl, http);
}
