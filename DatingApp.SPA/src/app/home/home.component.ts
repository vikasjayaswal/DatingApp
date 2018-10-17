import { Component, OnInit } from '@angular/core';

import { HttpClient } from '../../../node_modules/@angular/common/http';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  values: any;
  isRegisterMode: any = {};
  constructor(private http: HttpClient) {
    this.isRegisterMode = false;
    }

  ngOnInit() {

  }


  registerMode() {
    this.isRegisterMode = true;
    }



cancelRegisterMode(registerMode: boolean)
{

  this.isRegisterMode = registerMode;
  console.log(this.isRegisterMode);


}

}
