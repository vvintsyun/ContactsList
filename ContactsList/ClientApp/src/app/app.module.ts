import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ContactsListComponent } from './contacts-list/contacts-list.component';
import {ContactComponent} from './edit-contact/contact.component';
import {
  MAT_FORM_FIELD_DEFAULT_OPTIONS,
  MatButtonModule,
  MatExpansionModule,
  MatIconModule,
  MatInputModule,
  MatNativeDateModule,
  MatSelectModule, MatSnackBarModule,
  MatTableModule
} from '@angular/material';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {ContactInfosComponent} from './contact-infos/contact-infos.component';
import {ContactInfoComponent, EnumToArrayPipe} from './contact-infos/contact-info/contact-info.component';
import {ContactsDataService} from './services/data.service';
import {ComponentCreatorService} from './services/component-creator.service';
import {AddContactComponent} from './add-contact/add-contact.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ContactsListComponent,
    ContactComponent,
    AddContactComponent,
    ContactInfoComponent,
    ContactInfosComponent,
    EnumToArrayPipe
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', redirectTo: 'contacts', pathMatch: 'full'},
      {path: 'contacts', component: ContactsListComponent},
      {path: 'contacts/:id/edit', component: ContactComponent},
      {path: 'contacts/add', component: AddContactComponent},
    ]),
    ReactiveFormsModule,
    MatInputModule,
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatExpansionModule,
    MatIconModule,
    MatTableModule,
    MatButtonModule,
    MatSnackBarModule
  ],
  entryComponents: [
    ContactInfoComponent
  ],
  providers: [
    ContactsDataService,
    ComponentCreatorService,
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'fill' } },
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
