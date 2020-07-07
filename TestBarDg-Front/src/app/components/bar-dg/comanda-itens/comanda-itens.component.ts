import { Component, OnInit, Input, OnDestroy } from '@angular/core';

import { ComandaItensService } from 'src/app/services/comanda-itens.service';
import { ComandaItens } from 'src/app/models/comanda-itens';
import { MessengerService } from 'src/app/services/messenger.service';
import { Item } from 'src/app/models/item';
import { Comanda } from 'src/app/models/comanda';
import { ComandaService } from 'src/app/services/comanda.service';
import { Router } from '@angular/router';
import { from, Subscription } from 'rxjs';

@Component({
  selector: 'app-comanda-itens',
  templateUrl: './comanda-itens.component.html',
  styleUrls: ['./comanda-itens.component.css']
})
export class ComandaItensComponent implements OnInit, OnDestroy {

  @Input() idComanda: number;
  comandaItemList: ComandaItens[] = []
  newComandaItem: ComandaItens;
  comanda: Comanda;
  subscription: Subscription;

  comandaTotal = 0;
  bloqueiaSucoItem: boolean;

  constructor(private comandaItensService: ComandaItensService, private msg: MessengerService, private comandaService: ComandaService, private router: Router) {   }
  
  ngOnDestroy(): void {
    this.subscription.unsubscribe();  
  }

  async ngOnInit() {
    var self = this;

    this.subscription = this.msg.getItem().subscribe(async (item: Item) => {
      // console.log(item)
      var verificaItem = false;
      this.handleSendComandaItens();
      this.comandaItemList.forEach(async function (itens) {
        if(itens.idItem == item.id){  
          verificaItem = true;    
          await self.addItem(itens);
          await self.getComandaItensByComanda(self.idComanda);          
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
      await this.insertComandaItens(this.comandaItemList[this.comandaItemList.length-1]);
      await this.getComandaItensByComanda(this.idComanda);      
      }
       
      this.getTotal();      
    })

    this.subscription = this.msg.getComanda().subscribe((comanda: Comanda) => {
      this.idComanda = comanda.id;
      this.getComandaItensByComanda(comanda.id);

    })
    
    await this.getComandaItensByComanda(this.idComanda); 
    this.handleSendComandaItens();
    this.comandaService.getComandaById(this.idComanda).subscribe((comanda) =>{
      this.comanda = comanda;
    })
    // console.log(this.idComanda)
    this.msg.getComanda().subscribe((comanda: Comanda) => {
      this.comanda = comanda;

    })
  }
  
  comandaItensIsEmpty(): boolean{
    if(Object.keys(this.comandaItemList).length === 0){
      return true;
    }

    return false;

  }

  getTotal(){
    this.comandaTotal = 0;
    var self = this;
    this.comandaItemList.forEach(function (item) {
      self.comandaTotal += (item.quantidade * item.valorUnitario)
    })
  }


  async resetarComanda(comanda: Comanda){
    await this.comandaService.resetarComanda(comanda.id);
    window.location.reload();
  }

   async fecharComanda(comanda: Comanda){
    await this.comandaService.fecharComanda(comanda.id); 
    this.router.navigate(['comanda/nota-fiscal', comanda.id]); 
  }

  async insertComandaItens(comandaItem: ComandaItens){
    await this.comandaItensService.insertComandaItens(comandaItem);
  }

  async updateComandaItens(idComandaItem: number, comandaItem: ComandaItens){
    await this.comandaItensService.updateComandaItens(idComandaItem, comandaItem);
  }

  async getComandaItensByComanda(idComanda: number){
    this.comandaItensService.getComandaItensByComanda(idComanda).subscribe((itens) =>{
      this.comandaItemList = [];
      this.comandaItemList = itens;
      this.getTotal();
      this.handleSendComandaItens();
      // await this.handleSendComandaItens()
    })
  }

  async deleteComandaItensById(id: number){
    await this.comandaItensService.deleteComandaItensById(id);
  }

  async removeItem(item: ComandaItens){
    if (item.quantidade > 1){
      item.quantidade -= 1;
      item.valorTotal = item.quantidade * item.valorUnitario;
      await this.updateComandaItens(item.id, item);
    } else {
        if(item.id != null){
          await this.deleteComandaItensById(item.id);
        }        
        // const index: number = this.comandaItemList.indexOf(item);
        // if(index !== -1){
        //   this.comandaItemList.splice(index, 1);
        // }       
    }
    this.getComandaItensByComanda(this.idComanda); 
    this.getTotal();
  }

  async addItem(item: ComandaItens){
    if(!this.bloqueiaSuco(item)){
      item.quantidade += 1;
      item.valorTotal = item.quantidade * item.valorUnitario;
      await this.updateComandaItens(item.id, item);
      this.getTotal();
    }   
  }

  bloqueiaSuco(item: ComandaItens): boolean{
    if(item.nomeItem=="Suco" && item.quantidade >= 3){
      return true
    } 
    return false;
  }

  handleSendComandaItens(){
    var self = this;
    this.bloqueiaSucoItem = false;
    this.comandaItemList.forEach(function (itens) {
     if(itens.idItem == 3 && itens.quantidade >= 3 && itens.idComanda == self.idComanda){
       self.bloqueiaSucoItem = true;
     }
    });
    this.msg.sendComandaItens(this.bloqueiaSucoItem);
  }

  async onSelect(comanda){

    if(!comanda.isClosed){
      await this.fecharComanda(comanda);
    } else {
      this.router.navigate(['comanda/nota-fiscal', comanda.id]);
    }
    
  }

}
