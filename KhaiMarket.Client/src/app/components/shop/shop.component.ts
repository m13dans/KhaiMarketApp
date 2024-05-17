import { Component, OnInit } from '@angular/core';
import { ProductDto } from '../../Models/ProductDto';
import { ShopService } from './shop.service';
import { ProductItemComponent } from '../product-item/product-item.component';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [ProductItemComponent],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
})
export class ShopComponent implements OnInit {
  products: ProductDto[] = [];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.shopService.getProducts().subscribe({
      next: (r: ProductDto[]) => {
        this.products = r;
      },
      error: (e: Error) => {
        console.log('this is from my error');
        console.log(e);
      },
      complete: () => console.log('complete'),
    });
  }
}
