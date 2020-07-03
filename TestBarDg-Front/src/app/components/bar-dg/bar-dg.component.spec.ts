import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BarDgComponent } from './bar-dg.component';

describe('BarDgComponent', () => {
  let component: BarDgComponent;
  let fixture: ComponentFixture<BarDgComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BarDgComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BarDgComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
