import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BsDropdownModule } from 'ngx-bootstrap';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title = 'app';
  jwtHelper = new JwtHelperService();
  constructor(private authService: AuthService) {}

ngOnInit() {
  const token = localStorage.getItem('token');
  if (token) {
    this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    console.log( this.authService.decodedToken);
    console.log( this.authService.decodedToken.unique_name);
  }

}

}
