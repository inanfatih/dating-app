import { Injectable } from '@angular/core';
import { environment } from './../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  baseURl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUsers(): Observable<User[]> {
    // asagidaki get normalde Observable<Object> donuyor. Bunu Observable<User[]> a donusturmek icin get'ten sonra <User[]> ekledik
    return this.http.get<User[]>(this.baseURl + 'users');
  }

  getUser(id): Observable<User> {
    return this.http.get<User>(this.baseURl + 'users/' + id);
  }

  updateUser(id: number, user: User) {
    return this.http.put(this.baseURl + 'users/' + id, user);
  }
}
