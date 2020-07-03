import { Component, OnInit } from '@angular/core';

import { ComandaService } from 'src/app/services/comanda.service';
import { Comanda } from 'src/app/models/comanda';

@Component({
  selector: 'app-comandas',
  templateUrl: './comandas.component.html',
  styleUrls: ['./comandas.component.css']
})
export class ComandasComponent implements OnInit {

  comandaList: Comanda[] = []

  constructor(private comandaService: ComandaService) { }

  ngOnInit() {
    this.comandaService.getComandas().subscribe((comandas) =>{
      this.comandaList = comandas
    })
  }

}
