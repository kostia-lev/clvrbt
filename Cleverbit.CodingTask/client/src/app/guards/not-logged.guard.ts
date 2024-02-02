import {CanActivateFn, Router} from '@angular/router';
import {inject} from "@angular/core";
import {AuthService} from "../services/auth.service";

export const notLoggedGuard: CanActivateFn = async (route, state) => {
  const router = inject(Router);
  let auth = inject(AuthService);
  if (auth.isLoggedIn()) {
    router.navigate(['/productlist']);
    return false;
  }
    return true
};
