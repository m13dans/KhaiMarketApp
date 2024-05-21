import { Component, OnInit } from '@angular/core';
import { ProductDto } from '../../Models/ProductDto';
import { ShopService } from './shop.service';
import { ProductItemComponent } from '../product-item/product-item.component';
import { Category } from '../../Models/Category';
import { ProductBrand } from '../../Models/ProductBrand';
import { ProductFilterByOptions } from '../../Models/ProductFilterByOptions';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [ProductItemComponent],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
})
export class ShopComponent implements OnInit {
  products?: ProductDto[] | null;
  categories?: Category[];
  productBrands?: ProductBrand[];

  filterByOptions?: string;
  filterValue?: string;

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getCategories();
    this.getProductBrands();
  }

  getProducts() {
    this.shopService
      .getProducts(this.filterByOptions, this.filterValue)
      .subscribe((response) => {
        this.products = response;
      });
  }
  getCategories() {
    this.shopService.getCategories().subscribe({
      next: (r: Category[]) => {
        this.categories = [{ id: 0, name: 'All' }, ...r];
      },
      error: (e: Error) => {
        console.log('this is from my error');
        console.log(e);
      },
      complete: () => console.log('complete'),
    });
  }
  getProductBrands() {
    this.shopService.getProductBrands().subscribe({
      next: (r: ProductBrand[]) => {
        this.productBrands = [{ id: 0, name: 'All' }, ...r];
      },
      error: (e: Error) => {
        console.log('this is from my error');
        console.log(e);
      },
      complete: () => console.log('complete'),
    });
  }

  onFilterByOption(filterOption: string) {
    this.filterByOptions = filterOption;
    this.getProducts();
  }

  onFilterValue(filterValue: string) {
    this.filterValue = filterValue;
    this.getProducts();
  }
}
