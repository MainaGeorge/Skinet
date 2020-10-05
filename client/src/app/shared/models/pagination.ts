import {IProduct} from './product';

export interface IPagination {
  pageIndex: number;
  count: number;
  pageSize: number;
  data: IProduct[];
}
