import { Component, OnInit } from '@angular/core';
import {BasketService} from '../../basket/basket.service';
import {Observable} from 'rxjs';
import {IBasket} from '../../shared/models/basket';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  service$: Observable<IBasket>;
  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.service$ = this.basketService.basket$;
  }

}
