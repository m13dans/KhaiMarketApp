import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { HttpClient } from '@angular/common/http';
import { ProductDto } from './Models/ProductDto';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavBarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  title = 'KhaiMarket';
  products: ProductDto[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http
      .get<ProductDto[]>('http://localhost:5018/api/v2/products')
      .subscribe(
        (response: ProductDto[]) => {
          this.products = response;
          console.log(response);
        },
        (error) => {
          console.log(error);
        }
      );
  }
}
