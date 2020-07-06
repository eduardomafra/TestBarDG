import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Item } from 'src/app/models/item';
import { MessengerService } from 'src/app/services/messenger.service';
import { Comanda } from 'src/app/models/comanda';
import { ComandaService } from 'src/app/services/comanda.service';
import { ComandaItens } from 'src/app/models/comanda-itens';
import { getAllLifecycleHooks } from '@angular/compiler/src/lifecycle_reflector';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit, OnDestroy {

  @Input() item: Item
  @Input() idComanda: number;
  
  comanda: Comanda;
  bloqueiaSuco: boolean;
  subscription: Subscription;

  constructor(private msg: MessengerService, private comandaService: ComandaService) { }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngOnInit() {
    console.log(this.idComanda)
    this.subscription = this.comandaService.getComandaById(this.idComanda).subscribe((comanda) =>{
      this.comanda = comanda;
    })

    this.subscription = this.msg.getComanda().subscribe((comanda: Comanda) => {
      this.comanda = comanda;

    })

    this.subscription = this.msg.getComandaItens().subscribe((bloqueiaSuco: boolean) => {
      this.bloqueiaSuco = bloqueiaSuco;
    })

  }

  handleAddToComanda(){
    this.subscription;
    this.msg.sendItem(this.item);
  }

}
