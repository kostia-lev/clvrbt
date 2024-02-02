import {Component, OnDestroy, OnInit} from '@angular/core';
import {ProductService} from "../../services/product.service";
import {NgForOf} from "@angular/common";
import {Product} from "../../types/types";
import {AuthService} from "../../services/auth.service";
import {Subscription} from "rxjs";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-productlist',
  standalone: true,
  imports: [
    NgForOf,
    RouterLink
  ],
  templateUrl: './productlist.component.html',
  styleUrl: './productlist.component.css'
})
export class ProductlistComponent implements OnInit, OnDestroy {
  constructor(private productService: ProductService, private authService: AuthService) {
  }
  public products: Product[] = [];
  private getProductsSub: Subscription | undefined;
  protected readonly Boolean = Boolean;

  logOut() {
    this.authService.logOut();
  }
  ngOnInit() {
    this.getProductsSub = this.productService.getProducts().subscribe((products) => {
      this.products = products;
    })
  }

  ngOnDestroy() {
    this.getProductsSub?.unsubscribe();
  }
}
