import { Component, Input } from '@angular/core';
import { ProductDto } from '../../Models/ProductDto';
import { CurrencyPipe } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faShoppingCart } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [CurrencyPipe, FontAwesomeModule],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.scss',
})
export class ProductItemComponent {
  incrementCart() {}
  faShoppingCart = faShoppingCart;
  @Input() product?: ProductDto;
}
