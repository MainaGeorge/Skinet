import {Component, Input, OnInit} from '@angular/core';
import {IProduct} from '../../shared/models/product';
import {BasketService} from '../../basket/basket.service';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.css']
})
export class ProductItemComponent implements OnInit {

  @Input() productItem: IProduct;

  constructor(private basketService:BasketService) { }

  ngOnInit(): void {
  }

  AddProductToCart(){
    this.basketService.AddItemToBasket(this.productItem);
  }

}
