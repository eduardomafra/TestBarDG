import { Component, OnInit } from '@angular/core';

import { ComandaItensService } from 'src/app/services/comanda-itens.service';
import { ComandaItens } from 'src/app/models/comanda-itens';
import { MessengerService } from 'src/app/services/messenger.service';
import { Item } from 'src/app/models/item';

@Component({
  selector: 'app-comanda-itens',
  templateUrl: './comanda-itens.component.html',
  styleUrls: ['./comanda-itens.component.css']
})
export class ComandaItensComponent implements OnInit {

  comandaItemList: ComandaItens[] = []

  constructor(private comandaItensService: ComandaItensService, private msg: MessengerService) { }

  ngOnInit() {

    this.msg.getMsg().subscribe((item: Item) => {
      

      var verificaItem = false;

      this.comandaItemList.forEach(function (itens) {
        if(itens.idItem == item.id){
          verificaItem = true;
          itens.quantidade += 1;
          itens.valorTotal = itens.quantidade * itens.valorUnitario;
        }  
      });
      
      if(!verificaItem){
        this.comandaItemList.push({
          id: null,
          idItem: item.id,
          idComanda: 1,
          quantidade: 1,
          ativo: false,
          valorTotal: item.preco,
          valorUnitario: item.preco
      })
      }

      console.log(this.comandaItemList);

    })

    this.comandaItensService.getComandaItensByComanda(1).subscribe((itens) =>{
      this.comandaItemList = itens
    })
    
  }

  deleteComandaItensById(id: number){
    this.comandaItensService.deleteComandaItensById(id).subscribe();
  }

  removeItem(item: ComandaItens){
    if (item.quantidade > 1){
      item.quantidade -= 1;
      item.valorTotal = item.quantidade * item.valorUnitario;
    } else {
        if(item.id != null){
          this.deleteComandaItensById(item.id);
        }        
        const index: number = this.comandaItemList.indexOf(item);
        if(index !== -1){
          this.comandaItemList.splice(index, 1);
        }       
    }
  }

}
