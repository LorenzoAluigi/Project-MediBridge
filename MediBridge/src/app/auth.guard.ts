import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth.service';
import { map } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  return inject(AuthService).isLoggedIn$.pipe(
    map((isloggedIn) => {
      if (!isloggedIn) {
        inject(Router).navigate(['/login']);
      }
      return isloggedIn;
    })
  );
};
