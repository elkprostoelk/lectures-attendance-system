import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {RoleDto} from "../../models/roleDTO";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  rolePath: string;

  constructor(
    private http: HttpClient) {
    this.rolePath = 'role/';
  }

  getAllRoles(): Observable<RoleDto[]> {
    return this.http.get<RoleDto[]>(environment.apiPath + this.rolePath + 'all');
  }
}
