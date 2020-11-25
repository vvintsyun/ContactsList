import {AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {ContactsDataService} from '../services/data.service';
import {Contact} from '../contact';
import {ActivatedRoute, Router} from '@angular/router';
import {filter, debounceTime, distinctUntilChanged, tap, switchMap} from 'rxjs/operators';
import {fromEvent} from 'rxjs';
import {NotificationService} from '../services/error-notification.service';
import * as moment from 'moment';

@Component({
  selector: 'app-contacts-list',
  templateUrl: './contacts-list.component.html',
  styleUrls: ['./contacts-list.component.sass']
})
export class ContactsListComponent implements OnInit {

  constructor(private dataService: ContactsDataService,
              private router: Router,
              private notification: NotificationService,
              private route: ActivatedRoute) {
  }

  @ViewChild('search', {static: true}) search: ElementRef;
  contacts: Contact[];
  displayedColumns: string[] = ['firstName', 'lastName', 'middleName', 'birthDate', 'organizationName', 'organizationPost', 'actions'];

  ngOnInit(): void {
    this.getContacts();

    fromEvent(this.search.nativeElement,'keyup')
      .pipe(
        filter(Boolean),
        debounceTime(400),
        distinctUntilChanged(),
        switchMap((event: KeyboardEvent) => {
          return this.dataService.getContacts(this.search.nativeElement.value);
        })
      )
      .subscribe(
        (x: any[]) => this.contacts = this.mapContacts(x),
        (_ => {
          this.notification.showError('Error on getting contacts was occured.');
          this.contacts = [];
        })
      );
  }

  private getContacts() {
    this.dataService.getContacts()
      .subscribe(
        (x: any[]) => this.contacts = this.mapContacts(x),
        (_ => {
          this.notification.showError('Error on getting contacts was occured.');
          this.contacts = [];
        })
      );
  }

  addContact() {
    this.router.navigate(['add'], {relativeTo: this.route});
  }

  editContact(id) {
    this.router.navigate([`${id}/edit`], {relativeTo: this.route});
  }

  deleteContact(event, id) {
    event.stopPropagation();
    this.dataService.deleteContact(id)
      .subscribe(_ => this.getContacts(),
        _ => this.notification.showError('Error on deleting contact was occured.'));
  }

  private formatDate(date: Date) {
    return date
      ? moment(date).format('DD MMM YYYY')
      : '';
  }

  private mapContacts(contacts) {
    return contacts.map(
      c => new Contact(c.id, c.firstName, c.lastName, c.middleName, this.formatDate(c.birthDate),
        c.organizationName, c.organizationPost, c.contactInfos));
  }
}
