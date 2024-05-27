import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SearchService } from './search.service';
import { HttpClient } from '@angular/common/http';
import { ShopService } from '../shop.service';
import { ProductDto } from '../../Models/ProductDto';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss',
})
export class SearchComponent {
  constructor(
    private searchService: SearchService,
    private shopService: ShopService
  ) {}

  searchValue: string = '';
  products?: ProductDto[];

  submitSearch() {
    this.searchService.changeSearchValue(this.searchValue);
    this.searchService.changeProductResult(this.products);
    this.shopService.getProductBySearch(this.searchValue).subscribe((next) => {
      console.log(next);
      this.products = next;
    });
  }
}
