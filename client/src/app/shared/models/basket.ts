import * as uuid from 'uuid';

export interface IBasket {
  id: string;
  items: IBasketItem[];
}

export interface IBasketItem {
  id: number;
  productName: string;
  type: string;
  brand: string;
  pictureUrl: string;
  price: number;
  quantity: number;
}

export class Basket implements IBasket {
  id: string = uuid.v4();
  items: IBasketItem[] = [];

}

export interface IBasketTotals {
  subtotal: number,
  total:number,
  shipping:number
}
