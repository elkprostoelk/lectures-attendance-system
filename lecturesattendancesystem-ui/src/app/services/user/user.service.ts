import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {UserForAdminPanelDto} from "../../models/userForAdminPanelDTO";
import {environment} from "../../../environments/environment";
import {ShortUserDTO} from "../../models/shortUserDTO";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly userPath: string;

  constructor(private http: HttpClient) {
    this.userPath = 'user/';
  }

  deleteUser(userId: number): Observable<void> {
    return this.http.delete<void>(environment.apiPath + this.userPath + userId);
  }

  createUser(value: any): Observable<void> {
    return this.http.post<void>(`${environment.apiPath}${this.userPath}`, {
      name: value.name,
      firstName: value.firstName,
      lastName: value.lastName,
      roleId: value.roleId,
      password: value.password
    });
  }

  getAllUsers(): Observable<UserForAdminPanelDto[]> {
    return this.http.get<UserForAdminPanelDto[]>(environment.apiPath + this.userPath + 'all');
  }

  getUsers(): Observable<ShortUserDTO[]> {
    return this.http.get<ShortUserDTO[]>(environment.apiPath + this.userPath + 'without-admins');
  }
}
