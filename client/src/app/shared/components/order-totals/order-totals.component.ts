import { Component, OnInit } from '@angular/core';
import {BasketService} from '../../../basket/basket.service';
import {BehaviorSubject, Observable} from 'rxjs';
import {IBasketTotals} from '../../models/basket';

@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.css']
})
export class OrderTotalsComponent implements OnInit {

  basketTotal$: Observable<IBasketTotals>;
  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basketTotal$ = this.basketService.basketTotals$;
  }

}
