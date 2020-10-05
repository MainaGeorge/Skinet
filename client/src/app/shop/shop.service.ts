import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {IPagination} from '../shared/models/pagination';
import {IProductType} from '../shared/models/productType';
import {IProductBrand} from '../shared/models/productBrand';
import {map} from 'rxjs/operators';
import {ProductQueryParameters} from '../shared/models/productQueryParameters';
import {IProduct} from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:44334/api/'
  constructor(private http: HttpClient) { }

  getProducts(queryParams: ProductQueryParameters):Observable<IPagination>{
    let params = new HttpParams();
    if(queryParams.brandId != 0){
      params = params.append('brandId', queryParams.brandId.toString());
    }

    if(queryParams.typeId != 0){
      params = params.append('typeId', queryParams.typeId.toString());
    }

    if(queryParams.search){
      params = params.append('search', queryParams.search);
    }

    params = params.append('sort', queryParams.sort);
    params = params.append('pageIndex', queryParams.pageNumber.toString());
    params = params.append('pageSize', queryParams.pageSize.toString());


    return this.http.get<IPagination>(this.baseUrl + 'products', {observe: 'response', params})
      .pipe(map(response =>  response.body ) );

  }
  getProductBrands():Observable<IProductBrand[]> {
    return this.http.get<IProductBrand[]>(this.baseUrl + 'products/brands');
  }

  getProductTypes():Observable<IProductType[]>{
    return this.http.get<IProductType[]>(this.baseUrl + 'products/types');
  }

  getProduct(productId:number): Observable<IProduct> {
    return this.http.get<IProduct>(this.baseUrl + 'products/' + productId);
  }

}
