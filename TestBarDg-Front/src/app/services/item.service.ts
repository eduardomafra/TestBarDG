import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";

import { Item } from 'src/app/models/item';
import { environment } from '../../environments/environment';

const url = environment.url  + 'api/itens/';

// var httpOptions = {headers: new HttpHeaders({"Access-Control-Allow-Headers": "Origin, X-Requested-With, Content-Type, Accept"})};

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  constructor(private http: HttpClient) { }

  getItens(): Observable<Item[]>{
    return this.http.get<Item[]>(url);
  }

}
