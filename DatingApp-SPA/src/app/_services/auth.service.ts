import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

// app.module => root module
// Components are injectable by default but services are not.
// Bir service in tum component'larda kullanilabilmesi icin, app.module'da providers a eklenmesi gerekiyor.
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';

  constructor(private http: HttpClient) {}

  login(model: any) {
    // asagiya content-type i eklemedik cunku default olarak application/json gonderiliyor.
    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model);
  }
}