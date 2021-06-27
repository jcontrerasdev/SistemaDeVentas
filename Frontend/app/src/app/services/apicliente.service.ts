import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cliente } from '../models/cliente';
import { Response } from '../models/response';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ApiclienteService {

  url: string = 'https://localhost:44349/api/cliente';
  constructor(
    private _http: HttpClient
  ) { }

  getClientes(): Observable<Response>{
    return this._http.get<Response>(this.url);
  }

  add(cliente:Cliente):Observable<Response>{
    return this._http.post<Response>(this.url, cliente, httpOptions);
  }
}

