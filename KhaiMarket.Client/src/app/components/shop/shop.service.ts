import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProductDto } from '../../Models/ProductDto';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'http://localhost:5018/api/v2/';

  constructor(private http: HttpClient) {}

  getProducts() {
    return this.http.get<ProductDto[]>(this.baseUrl + 'products');
  }
}
