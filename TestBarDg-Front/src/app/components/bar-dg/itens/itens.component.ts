import { Component, OnInit } from '@angular/core';

import { ItemService } from 'src/app/services/item.service';
import { Item } from 'src/app/models/item';

@Component({
  selector: 'app-itens',
  templateUrl: './itens.component.html',
  styleUrls: ['./itens.component.css']
})
export class ItensComponent implements OnInit {

  itemList: Item[] = []

  constructor(private itemService: ItemService) { }

  ngOnInit() {
    this.itemService.getItens().subscribe((products) =>{
      this.itemList = products
    })
  }

}
