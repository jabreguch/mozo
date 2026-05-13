import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { ModalControlModel } from '@shared/models/controls/modal-control.model';

@Injectable({
  providedIn: 'root'
})
export class ModalService {

  private modalSubject = new Subject<ModalControlModel | null>();
  modalState$ = this.modalSubject.asObservable();

  open<T = any>(
    config: ModalControlModel<T>
  ): void {
    this.modalSubject.next(config);
  }

  close() {
    this.modalSubject.next(null);
  }

}
