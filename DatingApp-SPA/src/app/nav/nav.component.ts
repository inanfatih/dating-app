import { Component, OnInit } from '@angular/core';
import { AuthService } from './../_services/auth.service';
import { AlertifyService } from './../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})

// Component larin app.module da declarations kismina eklenmesi gerekiyor
export class NavComponent implements OnInit {
  model: any = {};

  photoUrl: string;

  constructor(
    public authservice: AuthService,
    private alertify: AlertifyService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authservice.currentPhotUrl.subscribe((photoUrl) => {
      this.photoUrl = photoUrl;
    });
  }

  login() {
    // We always have to SUBSCRIBE to OBSERVABLES
    this.authservice.login(this.model).subscribe(
      (next) => {
        this.alertify.success('logged in successfully');
      },
      (error) => {
        this.alertify.error(error);
      },
      () => {
        this.router.navigate(['/members']);
      }
    );
  }

  loggedIn() {
    return this.authservice.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authservice.decodedToken = null;
    this.authservice.currentUser = null;
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
  }
}
