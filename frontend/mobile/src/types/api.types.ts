export interface IResult {
  success: boolean;
  message: string | null;
}

export interface IDataResult<T> extends IResult {
  data: T;
}

export interface IPaginatedResult<T> extends IDataResult<T[]> {
  totalCount: number;
}
