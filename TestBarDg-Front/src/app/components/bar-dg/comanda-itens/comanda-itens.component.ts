import { Component, OnInit } from '@angular/core';

import { ComandaItensService } from 'src/app/services/comanda-itens.service';
import { ComandaItens } from 'src/app/models/comanda-itens';
import { MessengerService } from 'src/app/services/messenger.service';
import { Item } from 'src/app/models/item';
import { Comanda } from 'src/app/models/comanda';
import { ComandaService } from 'src/app/services/comanda.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-comanda-itens',
  templateUrl: './comanda-itens.component.html',
  styleUrls: ['./comanda-itens.component.css']
})
export class ComandaItensComponent implements OnInit {

  comandaItemList: ComandaItens[] = []
  comanda: Comanda;

  comandaTotal = 0;

  idComanda = 1;

  constructor(private comandaItensService: ComandaItensService, private msg: MessengerService, private comandaService: ComandaService, private router: Router) {   }

  ngOnInit() {

    var self = this;

    this.msg.getItem().subscribe((item: Item) => {
      console.log(item)
      var verificaItem = false;

      this.comandaItemList.forEach(function (itens) {
        if(itens.idItem == item.id){
          verificaItem = true;
          self.addItem(itens);
        }  
      });
      
      if(!verificaItem){
        this.comandaItemList.push({
          id: null,
          idItem: item.id,
          nomeItem: item.descricao,
          idComanda: this.idComanda,
          quantidade: 1,
          ativo: false,
          valorTotal: item.preco,
          valorUnitario: item.preco
      })
      }
      this.getTotal();

    })

    this.msg.getComanda().subscribe((comanda: Comanda) => {
      this.saveItensComanda();
      this.idComanda = comanda.id;
      this.getComandaItensByComanda(comanda.id);

    })
    
    this.getComandaItensByComanda(this.idComanda); 

    this.comandaService.getComandaById(1).subscribe((comanda) =>{
      this.comanda = comanda;
    })

    this.msg.getComanda().subscribe((comanda: Comanda) => {
      this.comanda = comanda;

    })
  }
  

  getTotal(){
    this.comandaTotal = 0;
    var self = this;
    this.comandaItemList.forEach(function (item) {
      self.comandaTotal += (item.quantidade * item.valorUnitario)
    })
  }

  fecharComanda(){
    
  }

  insertComandaItens(comandaItem: ComandaItens){
    this.comandaItensService.insertComandaItens(comandaItem).subscribe();
  }

  updateComandaItens(idComandaItem: number, comandaItem: ComandaItens){
    this.comandaItensService.updateComandaItens(idComandaItem, comandaItem).subscribe();
  }

  saveItensComanda(){
    var self = this;
    this.comandaItemList.forEach(function (comandaItens){
      if(comandaItens.id != null){
        self.updateComandaItens(comandaItens.id, comandaItens);
      } else {
        self.insertComandaItens(comandaItens);
      }
    });
  }

  getComandaItensByComanda(idComanda: number){
    this.comandaItensService.getComandaItensByComanda(idComanda).subscribe((itens) =>{
      this.comandaItemList = itens;
      this.getTotal();
      this.handleSendComandaItens()
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
    this.getTotal();
  }

  addItem(item: ComandaItens){
    item.quantidade += 1;
    item.valorTotal = item.quantidade * item.valorUnitario;
    this.getTotal();
  }

  handleSendComandaItens(){
    this.msg.sendComandaItens(this.comandaItemList);
  }

  onSelect(comanda){
    this.router.navigate(['/nota-fiscal', comanda.id]);
  }

}
