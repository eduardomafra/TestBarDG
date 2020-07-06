import { Component, OnInit, Input } from '@angular/core';
import { Comanda } from 'src/app/models/comanda';
import { MessengerService } from 'src/app/services/messenger.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-comanda',
  templateUrl: './comanda.component.html',
  styleUrls: ['./comanda.component.css']
})
export class ComandaComponent implements OnInit {

  @Input() comanda: Comanda;
  constructor(private msg: MessengerService, private router: Router) { }

  ngOnInit(): void {
  }

  handleSendComanda(){
    this.msg.sendComanda(this.comanda);
    this.router.navigate(['comanda/', this.comanda.id]);
  }

}
