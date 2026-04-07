import { ApiError } from "@/types/api";

export class HttpError<T = ApiError> extends Error {
  status: number;
  data?: T;

  constructor(status: number, message: string, data?: T) {
    super(message);

    Object.setPrototypeOf(this, new.target.prototype);

    this.name = "HttpError";
    this.status = status;
    this.data = data;
  }
}
