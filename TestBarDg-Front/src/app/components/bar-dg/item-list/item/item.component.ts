import { Component, OnInit, Input } from '@angular/core';
import { Item } from 'src/app/models/item';
import { MessengerService } from 'src/app/services/messenger.service';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit {

  @Input() item: Item

  constructor(private msg: MessengerService) { }

  ngOnInit(): void {
  }

  handleAddToComanda(){
    // console.log(this.item);
    this.msg.sendMsg(this.item);
  }

}
