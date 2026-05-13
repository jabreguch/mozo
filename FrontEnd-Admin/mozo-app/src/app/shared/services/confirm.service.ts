import { Injectable } from '@angular/core';
import Swal, { SweetAlertIcon } from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class ConfirmService {

  confirm(
    title: string,
    text: string,
    confirmText = 'Aceptar',
    cancelText = 'Cancelar',
    icon: SweetAlertIcon = 'question'
  ): Promise<boolean> {

    return Swal.fire({
      title,
      text,
      icon,
      showCancelButton: true,
      confirmButtonText: confirmText,
      cancelButtonText: cancelText,
      reverseButtons: true
    }).then(result => result.isConfirmed);
  }

  success(title: string, text = ''): void {
    Swal.fire({
      title,
      text,
      icon: 'success',
      timer: 1800,
      showConfirmButton: false
    });
  }

  error(title: string, text = ''): void {
    Swal.fire({
      title,
      text,
      icon: 'error'
    });
  }

}
