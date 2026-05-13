import { FormBuilder, ValidatorFn, Validators, FormGroup } from '@angular/forms';

export interface FieldConfig {
  name: string;
  value?: any;
  validators?: ValidatorFn[];
}

export const V = {
  username: [Validators.required, Validators.minLength(3)],
  password: [Validators.required, Validators.minLength(4)],
  email: [Validators.required, Validators.email],

  required: [Validators.required],

  phone: [Validators.pattern(/^[0-9]{9}$/)],

  onlyNumbers: [Validators.pattern(/^[0-9]+$/)],

  integer: [Validators.pattern(/^-?[0-9]+$/)],

  positiveInteger: [Validators.pattern(/^[0-9]+$/)],

  decimal: [Validators.pattern(/^-?\d+(\.\d+)?$/)],

  positiveDecimal: [Validators.pattern(/^\d+(\.\d+)?$/)],

  maxLength: (length: number) => [Validators.maxLength(length)],
  minLength: (length: number) => [Validators.minLength(length)],
  max: (value: number) => [Validators.max(value)],
  min: (value: number) => [Validators.min(value)],

};

export function buildForm(
  fb: FormBuilder,
  fields: FieldConfig[]
): FormGroup {
  const controls: Record<string, any> = {};

  for (const field of fields) {
    controls[field.name] = [
      field.value ?? '',
      field.validators ?? [],
    ];
  }

  return fb.group(controls);
}

export function getError(
  form: FormGroup,
  fieldName: string
): string {
  const control = form.get(fieldName);

  if (!control || !control.touched || !control.errors) {
    return '';
  }

  if (control.errors['required']) {
    return 'Campo obligatorio';
  }

  if (control.errors['minlength']) {
    return `Mínimo ${control.errors['minlength'].requiredLength} caracteres`;
  }

    if (control.errors['maxlength']) {
    return `Máximo ${control.errors['maxlength'].requiredLength} caracteres`;
  }

  if (control.errors['email']) {
    return 'Correo inválido';
  }

  if (control.errors['pattern']) {
    return 'Formato inválido';
  }

  return 'Valor inválido';
}
