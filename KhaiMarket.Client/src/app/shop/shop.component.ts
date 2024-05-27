import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop.service';
import { ProductFilterByOptions } from '../Models/ProductFilterByOptions';
import { ProductItemComponent } from './product-item/product-item.component';
import { ProductBrand } from '../Models/ProductBrand';
import { ProductDto } from '../Models/ProductDto';
import { Category } from '../Models/Category';
import { SearchService } from './search/search.service';

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
  searchResult?: string;

  constructor(
    private shopService: ShopService,
    private searchService: SearchService
  ) {}

  ngOnInit(): void {
    this.getProducts();
    this.getCategories();
    this.getProductBrands();

    this.searchService.currentSearchResult.subscribe(
      (x) => (this.searchResult = x)
    );

    this.searchService.productsBySearch.subscribe((x) => (this.products = x));
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
