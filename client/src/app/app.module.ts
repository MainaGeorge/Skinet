import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {CoreModule} from './core/core.module';
import {ShopModule} from './shop/shop.module';
import {AppRoutingModule} from './app-routing.module';
import {HomeModule} from './home/home.module';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {ErrorInterceptor} from './core/interceptors/error-interceptor';



@NgModule({
  declarations: [
    AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CoreModule,
    AppRoutingModule,
    HomeModule,
    HttpClientModule
  ],
  exports: [],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
