import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-nota-fiscal',
  templateUrl: './nota-fiscal.component.html',
  styleUrls: ['./nota-fiscal.component.css']
})
export class NotaFiscalComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }
  public comandaId;

  ngOnInit(): void {
    let id = parseInt(this.route.snapshot.paramMap.get('id'));
    this.comandaId = id;
  }

}
