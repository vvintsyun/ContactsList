import {AfterViewInit, Component, EventEmitter, Input, OnInit, SimpleChanges, ViewChild, ViewContainerRef} from '@angular/core';
import {ComponentCreatorService} from '../services/component-creator.service';
import {FormArray, FormGroup} from '@angular/forms';

@Component({
  selector: 'app-contact-infos',
  templateUrl: './contact-infos.component.html',
  styleUrls: ['./contact-infos.component.sass', '../edit-contact/contacts.component.sass']
})
export class ContactInfosComponent implements AfterViewInit {
  @Input() parentFormGroup: FormGroup;

  private formArray: FormArray;
  @ViewChild('dynamic', {read: ViewContainerRef, static: true}) viewContainerRef: ViewContainerRef;
  panelOpenState = false;
  constructor(private service: ComponentCreatorService) {
  }

  ngAfterViewInit(): void {
    this.service.setRootViewContainerRef(this.viewContainerRef);
    this.service.formArray = this.parentFormGroup.controls['contactInfos'] as FormArray;

    this.formArray = this.parentFormGroup.controls['contactInfos'] as FormArray;
    this.service.initFormComponents();
  }


  onAddContactClick() {
    this.service.addDynamicComponent();
  }
}
