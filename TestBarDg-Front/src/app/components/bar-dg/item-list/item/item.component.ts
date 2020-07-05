import { Component, OnInit, Input } from '@angular/core';
import { Item } from 'src/app/models/item';
import { MessengerService } from 'src/app/services/messenger.service';
import { Comanda } from 'src/app/models/comanda';
import { ComandaService } from 'src/app/services/comanda.service';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit {

  @Input() item: Item
  comanda: Comanda;

  constructor(private msg: MessengerService, private comandaService: ComandaService) { }

  ngOnInit() {

    this.comandaService.getComandaById(1).subscribe((comanda) =>{
      this.comanda = comanda;
    })

    this.msg.getComanda().subscribe((comanda: Comanda) => {
      this.comanda = comanda;

    })
  }
  

  handleAddToComanda(){
    // console.log(this.item);
    this.msg.sendItem(this.item);
  }

}
