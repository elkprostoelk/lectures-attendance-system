import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = localStorage.getItem('jwt');
    if (token) {
      const modifiedRequest = request.clone({
        headers: request.headers.set('authorization', `Bearer ${token}`)
      });
      return next.handle(modifiedRequest);
    }
    return next.handle(request);
  }
}
