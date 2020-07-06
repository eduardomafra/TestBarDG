import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { NavComponent } from './components/shared/nav/nav.component';
import { BarDgComponent } from './components/bar-dg/bar-dg.component';
import { ComandaItensComponent } from './components/bar-dg/comanda-itens/comanda-itens.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import {MatListModule} from '@angular/material/list';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import { ItemListComponent } from './components/bar-dg/item-list/item-list.component';
import { ItemComponent } from './components/bar-dg/item-list/item/item.component';
import {MatIconModule} from '@angular/material/icon';
import { ComandaListComponent } from './components/bar-dg/comanda-list/comanda-list.component';
import { ComandaComponent } from './components/bar-dg/comanda-list/comanda/comanda.component';
import { NotaFiscalComponent } from './components/bar-dg/nota-fiscal/nota-fiscal.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    NavComponent,
    BarDgComponent,
    ComandaItensComponent,
    ItemListComponent,
    ItemComponent,
    ComandaListComponent,
    ComandaComponent,
    NotaFiscalComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NoopAnimationsModule,
    MatListModule,
    HttpClientModule,
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    MatIconModule,
    RouterModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
