import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from "rxjs";

import { Comanda } from 'src/app/models/comanda';

const url = 'http://localhost:49242/api/comandas/';
var httpOptions = {headers: new HttpHeaders({"Content-Type": "application/json"})};

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

  async fecharComanda(idComanda: number): Promise<Comanda>{
    const apiUrl = url + 'fechar/' + idComanda;
    return this.http.post<Comanda>(apiUrl, idComanda, httpOptions).toPromise();
  }

  async resetarComanda(idComanda: number): Promise<Comanda>{
    const apiUrl =  url + 'resetar/' + idComanda;
    return this.http.post<Comanda>(apiUrl, idComanda, httpOptions).toPromise();
  }

}
