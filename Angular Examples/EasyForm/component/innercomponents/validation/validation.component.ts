import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import {  FormValidation, FormValidationType } from '../../../models/form.models';

@Component({
  selector: 'easy-validation',
  templateUrl: './validation.html',
  styleUrls: ['./validation.scss']
})
export class FormValidationComponent implements OnInit {

  @Input() public Validators: Array<FormValidation>;
  @Input() public Control: FormControl;

  constructor() { }

  public ngOnInit(): void {}

  public getValidationMessage(type: string) {
    var message = "";
    switch (type) {
      case "required":
        var validation = this.Validators.find(x => x.Type == FormValidationType.Required);
        message = validation?.Message ? validation.Message : "This field is required.";
        break;
      case "email":
        var validation = this.Validators.find(x => x.Type == FormValidationType.Email);
        message = validation?.Message ? validation.Message : "This field must be a valid Email.";
        break;
      case "pattern":
        var validation = this.Validators.find(x => x.Type == FormValidationType.Pattern);
        message = validation?.Message ? validation.Message : "This field is an invalid pattern.";
        break;
      case "minlength":
        var validation = this.Validators.find(x => x.Type == FormValidationType.MinLength);
        message = validation?.Message ? validation.Message : "This field must be longer.";
        break;
      case "maxlength":
        var validation = this.Validators.find(x => x.Type == FormValidationType.MaxLength);
        message = validation?.Message ? validation.Message : "This field must be shorter.";
        break;
    }

    return message;
  }

}
