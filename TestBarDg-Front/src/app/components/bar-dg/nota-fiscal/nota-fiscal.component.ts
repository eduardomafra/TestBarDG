import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ComandaService } from 'src/app/services/comanda.service';
import { Comanda } from 'src/app/models/comanda';
import { ComandaItens } from 'src/app/models/comanda-itens';
import { ComandaItensService } from 'src/app/services/comanda-itens.service';
import { DescontoService } from 'src/app/services/desconto.service';
import { Desconto } from 'src/app/models/desconto';

@Component({
  selector: 'app-nota-fiscal',
  templateUrl: './nota-fiscal.component.html',
  styleUrls: ['./nota-fiscal.component.css']
})
export class NotaFiscalComponent implements OnInit {

  constructor(private route: ActivatedRoute, private comandaService: ComandaService, private comandaItensService: ComandaItensService, private descontoService: DescontoService) { }
  public comandaId;
  comanda: Comanda;
  comandaItemList: ComandaItens[] = [];
  descontoList: Desconto[] = [];
  totalParcial: number=0;
  desconto: number=0;

  ngOnInit(): void {
    let id = parseInt(this.route.snapshot.paramMap.get('id'));
    this.comandaId = id;

    this.comandaService.getComandaById(this.comandaId).subscribe((comanda) =>{
      this.comanda = comanda
    })

    this.comandaItensService.getComandaItensByComanda(this.comandaId).subscribe((comandaItens) =>{
      this.comandaItemList = comandaItens;
      this.comandaItemList.forEach(itens => {
        this.totalParcial += itens.valorTotal;
      });
    })

    this.descontoService.getDescontosByComandaId(this.comandaId).subscribe((descontos) =>{
      this.descontoList = descontos;
      this.descontoList.forEach(desconto => {
        this.desconto += desconto.valorDesconto;
      });
    })

  }

}
