import {
  ComponentFactoryResolver,
  Injectable,
  ViewContainerRef,
  ComponentRef
} from '@angular/core';
import {ContactInfoComponent} from '../contact-infos/contact-info/contact-info.component';
import {AbstractControl, FormArray, FormControl, FormGroup, ValidationErrors, Validators} from '@angular/forms';
import {ContactInfoType} from '../contact-info';

@Injectable()
export class ComponentCreatorService {
  private rootViewContainer: ViewContainerRef;
  formArray: FormArray;
  private index: number = 0;
  componentsReferences = Array<ComponentRef<ContactInfoComponent>>();

  constructor(private factoryResolver: ComponentFactoryResolver) {
  }

  setRootViewContainerRef(viewContainerRef) {
    this.rootViewContainer = viewContainerRef;
  }

  getContactInfoForm(value) {
    return new FormGroup({
      value: new FormControl(value.value),
      type: new FormControl(value.type)
    });
  }

  initFormComponents() {
    for (let i = 0; i < this.formArray.controls.length; i++) {
      const componentFactory = this.factoryResolver.resolveComponentFactory(ContactInfoComponent);
      const infoComponentRef = this.rootViewContainer.createComponent(componentFactory);

      const infoComponent = infoComponentRef.instance;
      infoComponent.containerIndex = this.index++;
      infoComponent.contactInfoForm = this.formArray.controls[i] as FormGroup;

      this.componentsReferences.push(infoComponentRef);
    }
  }

  addDynamicComponent() {
    if (this.rootViewContainer) {
      const componentFactory = this.factoryResolver.resolveComponentFactory(ContactInfoComponent);
      const infoComponentRef = this.rootViewContainer.createComponent(componentFactory);

      const infoComponent = infoComponentRef.instance;
      infoComponent.containerIndex = this.index++;
      infoComponent.contactInfoForm = this.getContactInfoForm({value: '', type: ''});
      this.componentsReferences.push(infoComponentRef);

      this.formArray.push(infoComponent.contactInfoForm);
    }
  }

  removeDynamicComponent(key: number) {
    if (this.rootViewContainer.length < 1) {
      return;
    }

    const componentRef = this.componentsReferences.find(x => x.instance.containerIndex === key);
    const containerIndex = this.rootViewContainer.indexOf(componentRef as any);

    this.rootViewContainer.remove(containerIndex);
    this.componentsReferences = this.componentsReferences.filter(x => x.instance.containerIndex !== key);

    const groupFormIndex = this.formArray.controls.indexOf(componentRef.instance.contactInfoForm);
    this.formArray.removeAt(groupFormIndex);
  }
}
