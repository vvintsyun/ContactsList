import { Component } from '@angular/core';
import {ContactsDataService} from './services/data.service';

@Component({
  selector: 'app-root',
  providers: [
    ContactsDataService
  ],
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
}
