import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../_services/auth.service';
import { AlertifyService } from '../../_services/alertify.service';
import { BsDropdownModule } from 'ngx-bootstrap';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};

  constructor(public authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in Sucessfully'); },
       error => {this.alertify.error('Failed ot Login'); });
  }


loggedIn() {
  return this.authService.loggedIn();
}

logout() {
localStorage.removeItem('token');
this.alertify.message('Logged OUt');
}


}
