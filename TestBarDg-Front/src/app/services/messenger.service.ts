import { Injectable } from '@angular/core';
import { Subject } from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class MessengerService {

  itemSubject = new Subject();
  comandaSubject = new Subject();
  comandaItensSubject = new Subject();

  constructor() { }

  sendItem(item){
    this.itemSubject.next(item)
    // console.log(item)
  }

  getItem(){
    return this.itemSubject.asObservable()
  }

  sendComanda(comanda){
    this.comandaSubject.next(comanda)
    // console.log(comanda)
  }

  getComanda(){
    return this.comandaSubject.asObservable()
  }

  sendComandaItens(comandaItens){
    this.comandaItensSubject.next(comandaItens)
  }

  getComandaItens(){
    return this.comandaItensSubject.asObservable()
  }

}
