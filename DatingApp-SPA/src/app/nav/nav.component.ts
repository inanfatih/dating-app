import { Component, OnInit } from '@angular/core';
import { AuthService } from './../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})

// Component larin app.module da declarations kismina eklenmesi gerekiyor
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private authservice: AuthService) {}

  ngOnInit(): void {}

  login() {
    // We always have to SUBSCRIBE to OBSERVABLES
    this.authservice.login(this.model).subscribe(
      (next) => {
        console.log('Logged in successfully');
      },
      (error) => {
        console.log(error);
      }
    );
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }

  logout() {
    localStorage.removeItem('token');
    console.log('logged out');
  }
}
