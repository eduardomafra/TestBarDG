import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";

import { ComandaItens } from 'src/app/models/comanda-itens';

const url = 'http://localhost:49242/api/comanda_itens';

@Injectable({
  providedIn: 'root'
})
export class ComandaItensService {

  constructor(private http: HttpClient) { }

  getComandaItens(): Observable<ComandaItens[]>{
    return this.http.get<ComandaItens[]>(url);
  }

  getComandaItensByComanda(idComanda: number): Observable<ComandaItens[]>{
    const apiUrl = url + '/comanda/' + idComanda;
    return this.http.get<ComandaItens[]>(apiUrl);
  }

  deleteComandaItensById(id: number): Observable<number>{
    const apiUrl = url +  '/' + id;
    return this.http.delete<number>(apiUrl);
  }


}
