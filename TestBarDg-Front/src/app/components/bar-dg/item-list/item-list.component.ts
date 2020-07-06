import { Component, OnInit, Input } from '@angular/core';

import { ItemService } from 'src/app/services/item.service';
import { Item } from 'src/app/models/item';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.css']
})
export class ItemListComponent implements OnInit {

  @Input() idComanda: number;
  itemList: Item[] = []

  constructor(private itemService: ItemService) { }

  ngOnInit() {
    console.log(this.idComanda)
    this.itemService.getItens().subscribe((products) =>{
      this.itemList = products
    })
    // console.log(this.idComanda)
  }

}
