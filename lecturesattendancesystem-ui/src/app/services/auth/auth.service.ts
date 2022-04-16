import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {Observable} from "rxjs";
import {JwtDto} from "../../models/jwtDTO";
import {environment} from "../../../environments/environment";
import {UserModel} from "../../models/userModel";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly loginPath: string;

  constructor(
    private http: HttpClient,
    private router: Router) {
    this.loginPath = 'auth/login';
  }

  login(userName: string, password: string): Observable<JwtDto> {
    return this.http.post<JwtDto>(environment.apiPath + this.loginPath, {
      userName: userName,
      password: password
    });
  }

  logout(): void {
    if (localStorage.getItem('jwt')) {
      localStorage.removeItem('jwt');
      this.router.navigateByUrl('/login');
    }
  }

  isSignedIn(): boolean {
    return localStorage.getItem('jwt') !== null;
  }

  parseJwt(): UserModel | null {
    const token = localStorage.getItem('jwt');
    if (token) {
      const identity = JSON.parse(atob(token.split('.')[1]));
      const idClaim: string = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier';
      const nameClaim: string = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name';
      const roleClaim: string = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
      const expires: Date = new Date(0);
      expires.setUTCSeconds(identity['exp']);
      if (expires < new Date()) {
        localStorage.removeItem('jwt');
        return null;
      }
      return {
        id: identity[idClaim],
        name: identity[nameClaim],
        role: identity[roleClaim]
      };
    }
    return null;
  }

  isInRole(role: string): boolean {
    let user = this.parseJwt();
    if (user) {
      return user.role === role;
    }
    return false;
  }
}
