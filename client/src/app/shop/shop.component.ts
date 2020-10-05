import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {ShopService} from './shop.service';
import {IProduct} from '../shared/models/product';
import {IProductBrand} from '../shared/models/productBrand';
import {IProductType} from '../shared/models/productType';
import {ProductQueryParameters} from '../shared/models/productQueryParameters';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {
  @ViewChild('search', {static: true}) searchTerm: ElementRef;
  products: IProduct[];
  brands: IProductBrand[];
  types: IProductType[];
  totalCount:number;
  queryParams = new ProductQueryParameters();
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'}
  ]
  constructor(private shopService:ShopService) { }

  ngOnInit(): void {
    this.getProductBrands();
    this.getProducts();
    this.getProductTypes();
  }

  getProducts(){
    this.shopService.getProducts(this.queryParams).subscribe(response => {
      this.products = response.data;
      this.queryParams.pageNumber = response.pageIndex;
      this.queryParams.pageSize = response.pageSize;
      this.totalCount = response.count;
    }, error => {console.log(error)});
  }

  getProductBrands(){
    this.shopService.getProductBrands().subscribe(res => {
      this.brands = [{"id": 0, name: 'All'}, ...res];
      },
        error => {
      console.log(error);
    });
  }

  getProductTypes(){
    this.shopService.getProductTypes().subscribe( res => {this.types = [{"id": 0, name: 'All'}, ...res]} , err => {console.log(err)})
  }

  onSelectingBrand(brandId: number){
    this.queryParams.brandId = brandId;
    this.queryParams.pageNumber = 1;
    this.getProducts();
  }

  onSelectingType(typeId:number){
    this.queryParams.typeId = typeId;
    this.queryParams.pageNumber = 1;
    this.getProducts();
  }

  onSelectingSort(sort: string){
    this.queryParams.sort = sort;
    this.queryParams.pageNumber = 1;
    this.getProducts()
  }

  onPageChanged(pageNumber: number): void {
    if(this.queryParams.pageNumber != pageNumber){
      this.queryParams.pageNumber = pageNumber;
      this.getProducts();
    }
  }

  onSearch(){
    this.queryParams.search = this.searchTerm.nativeElement.value.trim();
    this.queryParams.pageNumber = 1;
    this.getProducts();
  }

  onReset(){
    this.queryParams = new ProductQueryParameters();
    this.searchTerm.nativeElement.value = '';
    this.getProducts();
  }
}
