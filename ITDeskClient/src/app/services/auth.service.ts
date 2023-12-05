import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { TokenModel } from '../models/token.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  token: TokenModel = new TokenModel();

  constructor(private router: Router) { }

  checkAuthentication() {
    //if (localStorage.getItem("response")) return true;
    const responseString = localStorage.getItem("response");

    if (responseString != null) {
      const responseJson = JSON.parse(responseString);

      if (responseJson != null) {
        const token = responseJson?.accessToken;
      
        if (token != null) {
          const decode:any= jwtDecode(token);
          this.token.email=decode?.Email;
          this.token.name=decode?.Name;
          this.token.userName=decode?.UserName;
          this.token.userId=decode?.UserId;
          this.token.exp=decode?.Exp;


          return true;

        } else {
          this.router.navigateByUrl("/login");
          return false;
        }
      } else {
        this.router.navigateByUrl("/login");
        return false;
      }
    }

    this.router.navigateByUrl("/login");
    return false;
  }
}
