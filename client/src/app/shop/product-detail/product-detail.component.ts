import { Component, OnInit } from '@angular/core';
import {ShopService} from '../shop.service';
import {ActivatedRoute} from '@angular/router';
import {IProduct} from '../../shared/models/product';
import {BasketService} from '../../basket/basket.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
  product: IProduct
  quantity = 1;

  constructor(private shopService: ShopService,
              private activatedRoute: ActivatedRoute,
              private basketService: BasketService) { }

  ngOnInit(): void {
    this.getProduct();
  }

  getProduct(){
    let productId = +this.activatedRoute.snapshot.paramMap.get('productId');
    this.shopService.getProduct(productId).subscribe( product => {
      this.product = product;
    }, error => console.log(error))
  }

  onIncrementingQuantity(){
    this.quantity++;
  }

  onDecrementingQuantity(){
    if(this.quantity > 1){
      this.quantity--;
    }
  }

  onAddingItemToBasket(){
    this.basketService.AddItemToBasket(this.product, this.quantity);
  }
}
