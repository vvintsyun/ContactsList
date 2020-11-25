import {Component, OnInit} from '@angular/core';
import {ContactsDataService} from '../services/data.service';
import {Contact} from '../contact';
import {FormArray, FormBuilder, FormControl, FormGroup} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE} from '@angular/material';
import {MomentDateAdapter} from '@angular/material-moment-adapter';
import {ComponentCreatorService} from '../services/component-creator.service';
import {NotificationService} from '../services/error-notification.service';


export const MY_FORMATS = {
  parse: {
    dateInput: "LL"
  },
  display: {
    dateInput: "DD MMM YYYY",
    monthYearLabel: "YYYY",
    dateA11yLabel: "LL",
    monthYearA11yLabel: "YYYY"
  }
};

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contacts.component.sass'],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE]
    },
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS }
  ]
})
export class ContactComponent implements OnInit {

  constructor(protected dataService: ContactsDataService,
              protected route: ActivatedRoute,
              protected router: Router,
              private notification: NotificationService,
              private fb: FormBuilder,
              private componentCreatorService: ComponentCreatorService) {
  }

  contactForm = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    middleName: new FormControl(''),
    birthDate: new FormControl(null),
    organizationName: new FormControl(''),
    organizationPost: new FormControl(''),
    contactInfos: new FormArray([])
  });
  contactId: number;
  isLoaded: boolean = false;
  isEditForm: boolean = true;

  getErrorMessage(formControl) {
    if (formControl.hasError('required')) {
      return 'You must enter a value';
    }

    return formControl.hasError('pattern') ? 'You must enter a valid value' : '';
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.contactId = +params.get('id');
      if (this.contactId) {
        this.getContact(this.contactId);
      }
    });
  }

  private getContact(contactId: number) {
    this.dataService.getContact(contactId)
      .subscribe((contact: Contact) => this.assignForm(contact),
        _ => this.notification.showError('Error on getting contact was occured.'));
  }

  private assignForm(contact: Contact) {
    const formContactInfos = contact.contactInfos.map(x =>
      this.componentCreatorService.getContactInfoForm({value: x.value, type: x.type.toString()}));

    this.contactForm.setValue({
      firstName: contact.firstName,
      lastName: contact.lastName,
      middleName: contact.middleName,
      birthDate: contact.birthDate,
      organizationName: contact.organizationName,
      organizationPost: contact.organizationPost,
      contactInfos: []
    });
    this.contactForm.setControl('contactInfos', this.fb.array(formContactInfos || []));
    this.isLoaded = true;
  }

  onSubmit() {
    const data = this.contactForm.value;
    data.id = this.contactId;
    data.contactInfos.forEach(x => x.type = +x.type);
    this.dataService.updateContact(data)
      .subscribe(_ => this.router.navigate(['../..'], {relativeTo: this.route}),
        _ => this.notification.showError('Error on updating contact was occured.'));
  }
}
