import {Component, OnDestroy, OnInit} from '@angular/core';
import {ProductService} from "../../services/product.service";
import {AuthService} from "../../services/auth.service";
import {Product} from "../../types/types";
import {Subscription} from "rxjs";
import {NgForOf} from "@angular/common";
import {ActivatedRoute, Route, RouterLink} from "@angular/router";
import {NUM_POPULAR} from "../../constants/misc";

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [
    NgForOf,
    RouterLink
  ],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent  implements OnInit, OnDestroy {
  constructor(private productService: ProductService, private authService: AuthService, private route: ActivatedRoute) {
  }
  public product?: Product;
  private getProductSub: Subscription | undefined;
  private buyProductSub: Subscription | undefined;
  protected readonly Boolean = Boolean;

  buyProduct(productId: number) {
    this.buyProductSub = this.productService.buyProduct(productId).subscribe((response)=>{
      this.product!.ordersCount = response.ordersCount;
    });
  }

  logOut() {
    this.authService.logOut();
  }
  ngOnInit() {
    const routeParams = this.route.snapshot.paramMap;
    const productIdFromRoute = Number(routeParams.get('productId'));
    this.getProductSub = this.productService.getOneProduct(productIdFromRoute).subscribe((product) => {
      this.product = product;
    })
  }

  isPopular() {
    return Boolean(this.product?.ordersCount && this.product?.ordersCount > NUM_POPULAR);
  }

  ngOnDestroy() {
    this.buyProductSub?.unsubscribe();
    this.getProductSub?.unsubscribe();
  }
}
