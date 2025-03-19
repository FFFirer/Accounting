export class ProblemDetails implements IProblemDetails {
  type?: string | undefined;
  title?: string | undefined;
  status?: number | undefined;
  detail?: string | undefined;
  instance?: string | undefined;

  [key: string]: any;

  constructor(data?: IProblemDetails) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property))
          (<any>this)[property] = (<any>data)[property];
      }
    }
  }

  protected isProblemDetails = true;
  static isProblemDetails(obj: any): obj is ProblemDetails {
    return obj.isProblemDetails === true;
  }

  init(_data?: any) {
    if (_data) {
      for (var property in _data) {
        if (_data.hasOwnProperty(property)) this[property] = _data[property];
      }
      this.type = _data["type"];
      this.title = _data["title"];
      this.status = _data["status"];
      this.detail = _data["detail"];
      this.instance = _data["instance"];
    }
  }

  static fromJS(data: any): ProblemDetails {
    data = typeof data === "object" ? data : {};
    let result = new ProblemDetails();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === "object" ? data : {};
    for (var property in this) {
      if (this.hasOwnProperty(property)) data[property] = this[property];
    }
    data["type"] = this.type;
    data["title"] = this.title;
    data["status"] = this.status;
    data["detail"] = this.detail;
    data["instance"] = this.instance;
    return data;
  }
}

export interface IProblemDetails {
  type?: string | undefined;
  title?: string | undefined;
  status?: number | undefined;
  detail?: string | undefined;
  instance?: string | undefined;

  [key: string]: any;
}

export class HttpValidationProblemDetails
  extends ProblemDetails
  implements IHttpValidationProblemDetails
{
  errors?: { [key: string]: string[] };

  [key: string]: any;

  constructor(data?: IHttpValidationProblemDetails) {
    super(data);
  }

  protected isHttpValidationProblemDetails = true;
  static isHttpValidationProblemDetails(
    obj: any,
  ): obj is HttpValidationProblemDetails {
    return obj.isHttpValidationProblemDetails === true;
  }

  init(_data?: any) {
    super.init(_data);
    if (_data) {
      for (var property in _data) {
        if (_data.hasOwnProperty(property)) this[property] = _data[property];
      }
      if (_data["errors"]) {
        this.errors = {} as any;
        for (let key in _data["errors"]) {
          if (_data["errors"].hasOwnProperty(key))
            (<any>this.errors)![key] =
              _data["errors"][key] !== undefined ? _data["errors"][key] : [];
        }
      }
    }
  }

  static fromJS(data: any): HttpValidationProblemDetails {
    data = typeof data === "object" ? data : {};
    let result = new HttpValidationProblemDetails();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === "object" ? data : {};
    for (var property in this) {
      if (this.hasOwnProperty(property)) data[property] = this[property];
    }
    if (this.errors) {
      data["errors"] = {};
      for (let key in this.errors) {
        if (this.errors.hasOwnProperty(key))
          (<any>data["errors"])[key] = (<any>this.errors)[key];
      }
    }
    super.toJSON(data);
    return data;
  }
}

export interface IHttpValidationProblemDetails extends IProblemDetails {
  errors?: { [key: string]: string[] };

  [key: string]: any;
}
