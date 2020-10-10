import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {BehaviorSubject} from 'rxjs';
import {Basket, IBasket, IBasketItem, IBasketTotals} from '../shared/models/basket';
import {map} from 'rxjs/operators';
import {IProduct} from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.baseUrl;
  private basketSource = new BehaviorSubject<IBasket>(null);
  private basketTotalsSource$ = new BehaviorSubject<IBasketTotals>(null);
  basket$ = this.basketSource.asObservable();
  basketTotals$ = this.basketTotalsSource$.asObservable();

  constructor(private http: HttpClient) { }

  getBasket(basketId: string){
    return this.http.get(this.baseUrl + 'basket?basketId=' + basketId).pipe(
      map( (res: IBasket) => {
        this.basketSource.next(res);
        this.calculateTotals();
      })
    );
  }

  setBasket(basket: IBasket){
    return this.http.post(this.baseUrl + 'basket', basket).subscribe((res:IBasket) => {
      this.basketSource.next(res);
      this.calculateTotals();
    },
error => console.log(error));
  }

  getCurrentBasketValue(){
    return this.basketSource.value;
  }

  AddItemToBasket(item: IProduct, quantity=1){
    const itemToAdd: IBasketItem = BasketService.MapFromProductToBasketItem(item, quantity);
    const basket: IBasket = this.getCurrentBasketValue() ?? BasketService.CreateBasket();
    basket.items = this.AddOrUpdateItemInTheBasket(basket, itemToAdd, quantity);
    console.log(basket);
    this.setBasket(basket);
  }

  incrementItemQuantity(item: IBasketItem){
    const basket = this.getCurrentBasketValue();
    const itemIndex = basket.items.findIndex(i => i.id === item.id);

    basket.items[itemIndex].quantity += 1;
    this.setBasket(basket);
  }

  decrementItemQuantity(item: IBasketItem){
    const basket = this.getCurrentBasketValue();
    const itemIndex = basket.items.findIndex(i => i.id === item.id);
    let itemQuantity = basket.items[itemIndex].quantity;

    if(itemQuantity > 1){
      basket.items[itemIndex].quantity--;
      this.setBasket(basket);
    }else {
      this.removeItemFromBasket(item);
    }


  }

  removeItemFromBasket(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    if(basket.items.some(x => x.id === item.id)){
      basket.items = basket.items.filter(i => i.id !== item.id);
      if(basket.items.length > 0){
        this.setBasket(basket);
      }else{
        this.deleteBasket(basket);
      }
    }
  }

  deleteBasket(basket: IBasket) {
    return this.http.delete(this.baseUrl + 'basket?basketId=' + basket.id).subscribe(() => {
      this.basketSource.next(null);
      this.basketTotalsSource$.next(null);
      localStorage.removeItem('basket_id');
    }, error => {
      console.log(error);
    });
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

 private calculateTotals(){
    const basket = this.getCurrentBasketValue();
    const shipping = 0;
    const subtotal = basket.items.reduce((total, currentProduct) => {
      return total + (currentProduct.price * currentProduct.quantity)
    }, 0);
    const total = subtotal + shipping;
    this.basketTotalsSource$.next({shipping, subtotal, total});
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
