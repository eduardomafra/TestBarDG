import { Component, OnInit, Input } from '@angular/core';
import { Comanda } from 'src/app/models/comanda';
import { ComandaService } from 'src/app/services/comanda.service';

@Component({
  selector: 'app-comanda-list',
  templateUrl: './comanda-list.component.html',
  styleUrls: ['./comanda-list.component.css']
})
export class ComandaListComponent implements OnInit {
  
  @Input() comanda: Comanda;
  comandaList: Comanda[] = []
  
  constructor(private comandaService: ComandaService) { }

  ngOnInit() {
    this.comandaService.getComandas().subscribe((comandas) =>{
      this.comandaList = comandas
    })
  }

}
