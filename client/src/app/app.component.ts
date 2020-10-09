import {Component, OnInit} from '@angular/core';
import {BasketService} from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Skinet';

  constructor(private baskService:BasketService) {
  }

  ngOnInit(): void {
    const basketId = localStorage.getItem('basket_id');
    if(basketId){
      this.baskService.getBasket(basketId).subscribe(res => {
        console.log('initializing basket');
      }, error => console.log(error))
    }
  }
}
