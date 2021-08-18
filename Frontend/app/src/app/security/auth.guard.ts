/*import { Injectable } from "@angular/core";
import { Router, CanActivate, ActivatedRouteSnapshot } from "@angular/router";


@Injectable({providedIn: 'root'})
export class AuthGuard implements CanActivate{
  constructor(private router: Router){
  }
  canActivate(route: ActivatedRouteSnapshot){
    this.router.navigate(['/login']);
    return false
  }
  
}*/

import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { ApiauthService } from './apiauth.service';
@Injectable()
export class AuthGuard implements CanActivate {
  constructor(public router: Router, private apiauthService: ApiauthService) {}
  canActivate(): boolean {
    const usuario = this.apiauthService.usuarioData;
    if(usuario !== null){
      return true;
    }
      this.router.navigate(['login']);
      return false;
  }
}