import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {getProducts, getOneProduct, buyProduct} from "../constants/endpoints";
import {Order, Product} from "../types/types";

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient, private router: Router) { }

   getProducts() {
     return this.http.get<Product[]>(getProducts);
   }
  getOneProduct(productId: number) {
     return this.http.get<Product>(getOneProduct + '/' + productId);
  }
  buyProduct(productId: number) {
     return this.http.post<Order>(buyProduct, {productId});
  }
}
