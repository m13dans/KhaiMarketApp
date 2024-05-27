import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ShopComponent } from './shop/shop.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavBarComponent, ShopComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss', './shop/shop.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'KhaiMarket';

  constructor() {}

  ngOnInit(): void {}
}
