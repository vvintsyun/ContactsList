import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Contact } from '../contact';

@Injectable()
export class ContactsDataService {

  private url = "/api/contacts";

  constructor(private http: HttpClient) {
  }

  getContacts(filter = '') {
    return this.http.get(this.url, {params: {filterString: filter}});
  }

  getContact(id: number) {
    return this.http.get(this.url + '/' + id);
  }

  createContact(contact: Contact) {
    return this.http.post(this.url, contact);
  }

  updateContact(contact: Contact) {
    return this.http.put(this.url + '/' + contact.id, contact);
  }

  deleteContact(id: number) {
    return this.http.delete(this.url + '/' + id);
  }
}
