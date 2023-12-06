import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, take, map, switchMap } from 'rxjs';
import { AuthService } from './auth.service';
import { AccessData } from './classes/access-data';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authSvc: AuthService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return this.authSvc.user$.pipe(
      take(1),

      switchMap((user: AccessData | null) => {
        if (!user) return next.handle(request);
        const newRequest = request.clone({
          headers: request.headers.append(
            'Authorization',
            `Bearer ${user.token}`
          ),
        });
        return next.handle(newRequest);
      })
    );
  }
}
