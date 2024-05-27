import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProductDto } from '../Models/ProductDto';
import { Category } from '../Models/Category';
import { ProductBrand } from '../Models/ProductBrand';
import { ProductFilterByOptions } from '../Models/ProductFilterByOptions';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'http://localhost:5018/api/v2/';

  constructor(private http: HttpClient) {}

  getProducts(filterByOption?: string, filterValue?: string) {
    let params = new HttpParams();
    if (filterByOption) {
      params = params.append(
        'ProductFilterByOptions',
        filterByOption === 'ByCategory'
          ? 'ByCategory'
          : filterByOption === 'ByMaterial'
          ? 'ByMaterial'
          : filterByOption === 'ByBrand'
          ? 'ByBrand'
          : filterByOption === 'ByPrice'
          ? 'ByPrice'
          : filterByOption === 'ByRating'
          ? 'ByRating'
          : 'NoFilter'
      );

      if (filterValue) {
        params = params.append('FilterValue', filterValue);
      }
    }
    return this.http
      .get<ProductDto[]>(this.baseUrl + 'products', {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }

  getProductBySearch(search: string) {
    let params = new HttpParams();
    let searchParams = params.append('Search', search);
    return this.http.get<ProductDto[]>(
      this.baseUrl + 'Products?' + searchParams
    );
  }

  getProductById(id: number) {
    return this.http.get<ProductDto>(this.baseUrl + id.toString());
  }
  getCategories() {
    return this.http.get<Category[]>(this.baseUrl + 'categories');
  }
  getProductBrands() {
    return this.http.get<ProductBrand[]>(this.baseUrl + 'productbrands');
  }
}
