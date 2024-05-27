import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ProductDto } from '../../Models/ProductDto';
import { ShopService } from '../shop.service';

@Injectable({
  providedIn: 'root',
})
export class SearchService {
  constructor(private shopService: ShopService) {}

  private searchResult = new BehaviorSubject('');
  currentSearchResult = this.searchResult.asObservable();

  productsBySearch = this.shopService.getProductBySearch(
    this.searchResult.getValue()
  );

  changeProductResult(products: ProductDto[] | undefined) {
    this.productsBySearch;
  }

  changeSearchValue(searchText: string) {
    this.searchResult.next(searchText);
  }

  // changeProductResult(listProduct: ProductDto[]) {
  //   this.productsBySearch = listProduct;
}
