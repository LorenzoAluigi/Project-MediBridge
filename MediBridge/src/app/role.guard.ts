import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth.service';
import { map, take } from 'rxjs';

export const roleGuard: CanActivateFn = (route, state) => {
  return inject(AuthService).user$.pipe(
    take(1), // prendi solo il primo valore emesso (non ascoltare continuamente)
    map((user) => {
      console.log(user);

      const expectedRole = route.data['expectedRole'];
      console.log(expectedRole);

      if (!user || !user.role?.includes('Doctor')) {
        // Se l'utente non è autenticato o non ha il ruolo aspettato, reindirizzalo alla pagina di login
        inject(Router).navigate(['/login']);
        return false;
      }
      // L'utente è autenticato e ha il ruolo aspettato, consenti l'accesso alla route
      return true;
    })
  );
};
