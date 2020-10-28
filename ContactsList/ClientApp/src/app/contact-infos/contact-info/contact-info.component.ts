import {Component, OnInit, Pipe, PipeTransform} from '@angular/core';
import {AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators} from '@angular/forms';
import {ComponentCreatorService} from '../../services/component-creator.service';
import {ContactInfoType} from '../../contact-info';

@Component({
  selector: 'app-contact-info',
  templateUrl: './contact-info.component.html',
  styleUrls: ['./contact-info.component.sass', '../../edit-contact/contacts.component.sass']
})
export class ContactInfoComponent implements OnInit {
  constructor(private service: ComponentCreatorService) {
  }

  types = ContactInfoType;
  containerIndex: number;
  contactInfoForm: FormGroup;

  valueTypeValidator() {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!this.contactInfoForm) return null;

      const selectedType = this.contactInfoForm.get('type');
      if (selectedType && selectedType.value) {
        switch (+selectedType.value) {
          case ContactInfoType.Email: {
            return Validators.email(control);
          }
          case ContactInfoType.Telephone: {
            const phoneRegExp = new RegExp('^[+]{0,1}[0-9]{1,15}$');
            const isInvalid = !phoneRegExp.test(control.value);

            return isInvalid
              ? {invalidValue: true}
              : null;
          }
          case ContactInfoType.Skype:
          case ContactInfoType.Other: {
            return null;
          }
          default:
            throw new Error('Not implemented type');
        }
      }
      return null;
    };
  }

  getErrorMessage(formControl) {
    if (formControl.hasError('required')) {
      return 'You must enter a value';
    }

    return formControl.hasError('email') || formControl.hasError('invalidValue') ? 'You must enter a valid value' : '';
  }

  ngOnInit(): void {
    const typeControl = this.contactInfoForm.get('type');
    const valueControl = this.contactInfoForm.get('value');
    if (typeControl && valueControl) {
      typeControl.valueChanges.subscribe(_ => {
        valueControl.updateValueAndValidity();
      });

      valueControl.setValidators([
        Validators.required,
        this.valueTypeValidator()
      ]);
      typeControl.setValidators([
        Validators.required
      ]);
    }

  }

  deleteInfo() {
    this.service.removeDynamicComponent(this.containerIndex);
  }
}

@Pipe({name: 'enumToArray'})
export class EnumToArrayPipe implements PipeTransform {
  transform(value): Object {
    return Object.keys(value).filter(e => !isNaN(+e)).map(o => {
      return {index: +o, name: value[o]};
    });
  }
}
