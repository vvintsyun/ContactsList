import {Component} from '@angular/core';
import {ContactComponent, MY_FORMATS} from '../edit-contact/contact.component';
import {ContactsDataService} from '../services/data.service';
import {ActivatedRoute, Router} from '@angular/router';
import {DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE} from '@angular/material';
import {MomentDateAdapter} from '@angular/material-moment-adapter';

@Component({
  templateUrl: '../edit-contact/contact.component.html',
  styleUrls: ['../edit-contact/contacts.component.sass'],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE]
    },
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS }
  ]
})
export class AddContactComponent extends ContactComponent {
  isEditForm = false;

  onSubmit() {
    const data = this.contactForm.value;
    data.contactInfos.forEach(x => x.type = +x.type);
    this.dataService.createContact(data)
      .subscribe(_ => this.router.navigate(['../'], {relativeTo: this.route}));
  }
}
