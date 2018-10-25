import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable()
export class AuthService {
baseUrl = 'http://localhost:5000/api/auth/';
jwtHelper = new JwtHelperService();
decodedToken: any;
user_name: any;

constructor(private http: HttpClient) {}

login(model: any) {
    return this.http.post(this.baseUrl + 'login', model).pipe(
        map((response: any) => {
            const user = response;
            if (user) {
                localStorage.setItem('token', user.token);
                this.decodedToken = this.jwtHelper.decodeToken(user.token);
                console.log(this.decodedToken);
            }
        })
    );
}

register(model: any) {
    console.log('RegisterService!!!');
    return this.http.post(this.baseUrl + 'register', model);
}

loggedIn() {

const token = localStorage.getItem('token');
return !this.jwtHelper.isTokenExpired(token);


}



private requestOptions() {
    const headers = new Headers({'Content-type' : 'application/json'});
    return new RequestOptions({ headers: headers});
}


}
