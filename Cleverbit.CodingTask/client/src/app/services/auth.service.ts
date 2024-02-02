import { Injectable } from '@angular/core';
import {login} from "../constants/endpoints";
import {HttpClient} from "@angular/common/http";
import {tap} from "rxjs";
import {Router} from "@angular/router";
import {TOKEN} from "../constants/misc";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private router: Router) { }
  setAuthorizationToken(token: string) {
    return window.localStorage.setItem(TOKEN, token);
  }
  getAuthorizationToken() {
    return window.localStorage.getItem(TOKEN);
  }
  isLoggedIn() {
    return Boolean(this.getAuthorizationToken());
  }
  logOut() {
    window.localStorage.removeItem(TOKEN);
    this.router.navigate(['/login'])
  }

  requestLogin(user: {username: string, password: string}) {
    return this.http.post(login, user,
      {responseType: 'text'}).pipe(tap((token) => {
      console.log(JSON.stringify(token));
      this.setAuthorizationToken(token)
      this.router.navigate(['/productlist'])
    }));
  }
}
