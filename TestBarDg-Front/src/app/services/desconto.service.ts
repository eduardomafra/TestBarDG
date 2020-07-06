import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Desconto } from '../models/desconto';

const url = 'http://localhost:49242/api/descontos/';

@Injectable({
  providedIn: 'root'
})
export class DescontoService {

  constructor(private http: HttpClient) { }

  getDescontos(): Observable<Desconto[]>{
    return this.http.get<Desconto[]>(url);
  }

  getDescontosByComandaId(idComanda: number): Observable<Desconto[]>{
    const apiUrl = url + 'comanda/' + idComanda;
    return this.http.get<Desconto[]>(apiUrl);
  }

}
