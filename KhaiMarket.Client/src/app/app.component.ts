import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { ShopComponent } from './components/shop/shop.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavBarComponent, ShopComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss', './components/shop/shop.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'KhaiMarket';

  constructor() {}

  ngOnInit(): void {}
}
