import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotaFiscalComponent } from './components/bar-dg/nota-fiscal/nota-fiscal.component';
import { BarDgComponent } from './components/bar-dg/bar-dg.component';


const routes: Routes = [
  { path: '', redirectTo: 'comanda/1', pathMatch: 'full' },
  { path: 'comanda/:id', component: BarDgComponent},
  { path: 'comanda/nota-fiscal/:id', component: NotaFiscalComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const routingComponents = [NotaFiscalComponent]