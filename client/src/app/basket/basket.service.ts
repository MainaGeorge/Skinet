import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {BehaviorSubject} from 'rxjs';
import {Basket, IBasket, IBasketItem} from '../shared/models/basket';
import {map} from 'rxjs/operators';
import {IProduct} from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.baseUrl;
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();


  constructor(private http: HttpClient) { }

  getBasket(basketId: string){
    return this.http.get(this.baseUrl + 'basket?basketId=' + basketId).pipe(
      map( (res: IBasket) => {
        this.basketSource.next(res);
        this.getCurrentBasketValue();
      })
    );
  }

  setBasket(basket: IBasket){
    return this.http.post(this.baseUrl + 'basket', basket).subscribe((res:IBasket) => {
      this.basketSource.next(res);
    },
error => console.log(error));
  }

  getCurrentBasketValue(){
    console.log(this.basketSource.value);
    return this.basketSource.value;
  }

  AddItemToBasket(item: IProduct, quantity=1){
    const itemToAdd: IBasketItem = BasketService.MapFromProductToBasketItem(item, quantity);
    const basket: IBasket = this.getCurrentBasketValue() ?? BasketService.CreateBasket();
    basket.items = this.AddOrUpdateItemInTheBasket(basket, itemToAdd, quantity);
    console.log(basket);
    this.setBasket(basket);
  }

  private static MapFromProductToBasketItem(item: IProduct, quantity: number): IBasketItem {
    return {
      price : item.price,
      pictureUrl : item.pictureUrl,
      brand : item.productBrand,
      type : item.productType,
      quantity: quantity,
      productName : item.name,
      id : item.id,
    };
  }

  private static CreateBasket() : IBasket{
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  private AddOrUpdateItemInTheBasket(basket: IBasket, itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const productIndex = basket.items.findIndex(p => p.id == itemToAdd.id);
    if(productIndex === -1){
      itemToAdd.quantity = quantity;
      basket.items.push(itemToAdd);
    }else {
      basket.items[productIndex].quantity += quantity;
    }
    return basket.items;
  }
}
