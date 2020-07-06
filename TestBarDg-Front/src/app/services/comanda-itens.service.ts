import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from "rxjs";

import { ComandaItens } from 'src/app/models/comanda-itens';

const url = 'http://localhost:49242/api/comanda_itens';
var httpOptions = {headers: new HttpHeaders({"Content-Type": "application/json"})};

@Injectable({
  providedIn: 'root'
})
export class ComandaItensService {

  constructor(private http: HttpClient) { }

  getComandaItens(): Observable<ComandaItens[]>{
    return this.http.get<ComandaItens[]>(url);
  }

  getComandaItensByComanda(idComanda: number): Observable<ComandaItens[]> {
    const apiUrl = url + '/comanda/' + idComanda;
    return this.http.get<ComandaItens[]>(apiUrl);
    
  }

  async deleteComandaItensById(id: number): Promise<number>{
    const apiUrl = url +  '/' + id;
    return this.http.delete<number>(apiUrl).toPromise();
  }

  async insertComandaItens(comandaItem: ComandaItens): Promise<ComandaItens>{
    return this.http.post<ComandaItens>(url, comandaItem).toPromise();
  }

  async updateComandaItens(id: number, comandaItem: ComandaItens): Promise<ComandaItens>{
    const apiUrl = url + '/' + id;
    return this.http.put<ComandaItens>(apiUrl, comandaItem, httpOptions).toPromise();
  }


}
