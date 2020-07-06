import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-bar-dg',
  templateUrl: './bar-dg.component.html',
  styleUrls: ['./bar-dg.component.css']
})
export class BarDgComponent implements OnInit {

  public id;
  idComanda: number;
  constructor(private route: ActivatedRoute) {  this.id = parseInt(this.route.snapshot.paramMap.get('id')); }

  ngOnInit() {
    this.idComanda = parseInt(this.route.snapshot.paramMap.get('id'));
  }

}
