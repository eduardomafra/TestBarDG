import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ComandaItensComponent } from './comanda-itens.component';

describe('ComandaItensComponent', () => {
  let component: ComandaItensComponent;
  let fixture: ComponentFixture<ComandaItensComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ComandaItensComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ComandaItensComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
