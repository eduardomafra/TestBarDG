import { Component, OnInit } from '@angular/core';

import { ComandaItensService } from 'src/app/services/comanda-itens.service';
import { ComandaItens } from 'src/app/models/comanda-itens';

@Component({
  selector: 'app-comanda-itens',
  templateUrl: './comanda-itens.component.html',
  styleUrls: ['./comanda-itens.component.css']
})
export class ComandaItensComponent implements OnInit {

  comandaItemList: ComandaItens[] = []

  constructor(private comandaItensService: ComandaItensService) { }

  ngOnInit() {
    this.comandaItensService.getComandaItens().subscribe((products) =>{
      this.comandaItemList = products
    })
  }

}
