import { TestBed } from '@angular/core/testing';

import { ComandaItensService } from './comanda-itens.service';

describe('ComandaItensService', () => {
  let service: ComandaItensService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ComandaItensService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
