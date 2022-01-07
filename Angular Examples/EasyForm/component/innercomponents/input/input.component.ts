import { Component, OnInit } from '@angular/core';
import AngularService from '../../../../Popups/services/angular.service';
import { FormCheckboxType, FormInput, FormInputType } from '../../../models/form.models';
import FormService from '../../../services/form.service';
import { FormElementBaseComponent } from '../elementbase/elementbase.component';

@Component({
  selector: 'easy-input',
  templateUrl: './input.html',
  styleUrls: ['./input.scss']
})
export class FormInputComponent extends FormElementBaseComponent {

  public InputType = FormInputType;
  public CheckboxType = FormCheckboxType;

  public get InputElement(): FormInput {
    return this.Element as FormInput;
  };

  constructor(service: AngularService, formService: FormService) {
    super(service, formService);
    this.Id = `Input_${this.Id}`;
  }

  public click(element: HTMLInputElement): void {
    if (this.InputElement.Type == FormInputType.Checkbox) {
      let isChecked = element.checked;
      this.InputElement.Value = isChecked ? "true" : null;
      this.customChanged(isChecked);
    }
  }

  public doChange(): void {
    if (this.InputElement.Type != FormInputType.Checkbox) {
      this.changed();
    }
  }

  public getType(): string {
    switch (this.InputElement.Type) {
      case FormInputType.Checkbox:
        return "checkbox";
      case FormInputType.Email:
        return "email";
      case FormInputType.Hidden:
        return "hidden";
      case FormInputType.Number:
        return "number";
      case FormInputType.Passsword:
        return "password";
      case FormInputType.Telephone:
        return "tel";
      case FormInputType.Text:
        return "text";
    }
  }
  public getInputClass(): string {
    switch (this.InputElement.Type) {
      case FormInputType.Checkbox:
        return "form-check-input checkbox-input";
      case FormInputType.Number:
      case FormInputType.Email:
      case FormInputType.Hidden:
      case FormInputType.Passsword:
      case FormInputType.Telephone:
      case FormInputType.Text:
        return "form-control";
    }
  }
  public getLabelClass(): string {
    switch (this.InputElement.Type) {
      case FormInputType.Checkbox:
        return "form-check-label";
      case FormInputType.Number:
      case FormInputType.Email:
      case FormInputType.Hidden:
      case FormInputType.Passsword:
      case FormInputType.Telephone:
      case FormInputType.Text:
        return "form-label";
    }
  }

  

}
