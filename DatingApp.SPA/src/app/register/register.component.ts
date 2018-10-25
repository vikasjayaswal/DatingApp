import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../../_services/auth.service';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};

  @Output() cancelRegister  = new EventEmitter();
  isRegisterMode: any = false;
  authservice: any;

  constructor(authservice: AuthService, private alertify: AlertifyService) {
    this.authservice = authservice;

   }


  ngOnInit() {
  }


register() {
console.log(this.model);
this.authservice.register(this.model).subscribe( () => {
  
  this.alertify.success('Registration Successful');
  console.log('Registration Success');
  this.cancelRegister.emit(false);
}, error => {
   this.alertify.error("Something went wrong");
  console.log(this.model);
}


);
}
cancel() {
this.cancelRegister.emit(false);

}

}
