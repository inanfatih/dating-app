import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css'],
})
export class ValueComponent implements OnInit {
  // Service injection constructor'da yapiliyor
  constructor(private http: HttpClient) {}
  values: any;

  ngOnInit() {
    this.getValues();
  }

  getValues() {
    this.http.get('http://localhost:5000/api/values').subscribe(
      (response) => {
        this.values = response;
        console.log('values', this.values);
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
