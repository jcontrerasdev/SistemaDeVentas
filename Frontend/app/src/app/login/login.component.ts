import { Component, OnInit } from "@angular/core";
import { ApiauthService } from "../security/apiauth.service";

@Component({ templateUrl: 'login.component.html'})
export class LoginComponent implements OnInit{

  public email: string;
  public password: string;
  constructor(public apiauth: ApiauthService){
    this.email="";
    this.password = "";
  }

  ngOnInit(){

  }

  login(){
    this.apiauth.login(this.email, this.password).subscribe(response => {
      console.log(response);
    })
  }
}