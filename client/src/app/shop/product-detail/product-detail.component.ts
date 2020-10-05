import { Component, OnInit } from '@angular/core';
import {ShopService} from '../shop.service';
import {ActivatedRoute} from '@angular/router';
import {IProduct} from '../../shared/models/product';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
  product: IProduct
  constructor(private shopService: ShopService,
              private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.getProduct();
  }

  getProduct(){
    let productId = +this.activatedRoute.snapshot.paramMap.get('productId');
    this.shopService.getProduct(productId).subscribe( product => {
      this.product = product;
    }, error => console.log(error))
  }
}
