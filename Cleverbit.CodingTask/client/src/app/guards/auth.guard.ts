import {CanActivateFn, Router} from '@angular/router';
import {inject, Injectable} from "@angular/core";
import {AuthService} from "../services/auth.service";

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router, {});
  if (!inject(AuthService).isLoggedIn()) {
    router.navigate(['/login']);
    return false;
  }
  return true
};
