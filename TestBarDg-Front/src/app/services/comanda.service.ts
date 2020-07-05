import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";

import { Comanda } from 'src/app/models/comanda';

const url = 'http://localhost:49242/api/comandas/';

@Injectable({
  providedIn: 'root'
})
export class ComandaService {

  constructor(private http: HttpClient) { }

  getComandas(): Observable<Comanda[]>{
    return this.http.get<Comanda[]>(url);
  }

  getComandaById(idComanda: number): Observable<Comanda>{
    const apiUrl = url + idComanda;
    return this.http.get<Comanda>(apiUrl);
  }
}
