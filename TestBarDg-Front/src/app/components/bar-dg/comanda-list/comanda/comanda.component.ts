import { Component, OnInit, Input } from '@angular/core';
import { Comanda } from 'src/app/models/comanda';
import { MessengerService } from 'src/app/services/messenger.service';

@Component({
  selector: 'app-comanda',
  templateUrl: './comanda.component.html',
  styleUrls: ['./comanda.component.css']
})
export class ComandaComponent implements OnInit {

  @Input() comanda: Comanda;
  constructor(private msg: MessengerService) { }

  ngOnInit(): void {
  }

  handleGetComanda(){
    this.msg.sendComanda(this.comanda);
  }

}
