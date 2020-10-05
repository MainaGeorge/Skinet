import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from './home/home.component';
import {ShopComponent} from './shop/shop.component';
import {ProductDetailComponent} from './shop/product-detail/product-detail.component';

const routes:Routes = [
  {path: '', component: HomeComponent},
  {path: 'shop', component: ShopComponent},
  {path: 'shop/:productId', component: ProductDetailComponent},
  {path: '**', component:HomeComponent, pathMatch:'full'}
]
@NgModule({
  imports: [ RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule{
}
