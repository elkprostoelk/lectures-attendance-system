import { Component } from '@angular/core';
import {AuthService} from "./services/auth/auth.service";
import {UserModel} from "./models/userModel";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  date: Date;
  user: UserModel | null;

  constructor(
    private readonly authService: AuthService
  ) {
    this.date = new Date();
    this.user = this.authService.parseJwt();
  }

  signOut(): void {
    this.authService.logout();
  }

  signedIn(): boolean {
    this.user = this.authService.parseJwt();
    return this.authService.isSignedIn();
  }

  isInRole(role: string): boolean {
    return this.authService.isInRole(role);
  }
}
