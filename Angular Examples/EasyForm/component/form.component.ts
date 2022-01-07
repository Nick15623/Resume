import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormButtonAlignment, FormCheckboxType, FormConfig, FormElementChange, FormGroupType, FormInputType, FormRow, FormValidationType } from '../models/form.models';
import ApiService from '../services/api.service';
import PopupService from '../../Popups/services/popup.service';
import FormService from '../services/form.service';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'easyform',
  templateUrl: './form.html',
  styleUrls: ['./form.scss']
})
export class FormComponent implements OnInit {
  @Output() public onSubmit: EventEmitter<any> = new EventEmitter<any>();

  @Input() public Config: FormConfig;

  public GroupType = FormGroupType;

  constructor(private formService: FormService) { }

  public ngOnInit(): void {
    if (this.Config) {
      if (this.Config.DemoMode == true) {
        this.activateDemoMode();
      }
    }
  }

  public onLoad(control: FormControl): void {
    this.formService.updateCacheForFormControl(this.Config.Name, control);
  }
  public onChange(change: FormElementChange): void {
    this.formService.updateCacheForElementChange(this.Config.Name, change);
  }
  public submit(): void {
    this.formService.touchForm(this.Config.Name);
    if (this.formService.isFormValid(this.Config.Name)) {
      if (this.Config && this.Config.OnSubmit) {
        this.Config.OnSubmit();
      }
      console.log(this.formService.getFormObjectFromCache(this.Config.Name));
    }
  }
  public clear(): void {
    this.formService.clearForm(this.Config.Name);
    if (this.Config && this.Config.OnClear) {
      this.Config.OnClear();
    }
  }
  public getRowColWidth(formRow: FormRow, index: number): string {

    if (formRow.CustomColumnWidths && index < formRow.CustomColumnWidths.length) {
      return `col-12 col-md-${formRow.CustomColumnWidths[index]}`;
    } else {
      var len = formRow.FormRowGroups.length;
      switch (len) {
        case 1:
          return "col-12";
        case 2:
          return "col-md-6 col-12";
        case 3:
          return "col-md-4 col-12";
        case 4:
          return "col-md-3 col-12";
        case 5:
          return "col-md-2 col-12";
        case 6:
        default:
          return "col-md-1 col-12";
      }
    }
  }
  public getButtonAlignment(): string {
    switch (this.Config.ButtonAlignment) {
      case FormButtonAlignment.Center:
        return "form-btn-center";
      case FormButtonAlignment.Right:
        return "form-btn-right";
      case FormButtonAlignment.Between:
        return "form-btn-between";
      case FormButtonAlignment.Left:
      default:
        return "form-btn-left";

    }
  }

  public TestFormConfig: FormConfig = {
    Name: "TestForm",
    Title: "Send us a Review",
    Description: "Use the form below to send us a review on how you think we are doing. We truly appreciate all feedback, and look forward to hearing from you!",
    FormRows: [
      {
        FormRowGroups: [
          {
            Type: FormGroupType.Input,
            Label: "First Name",
            FormElement: {
              Name: "FirstName",
              Type: FormInputType.Text,
              Placeholder: "First Name"
            },
            Validators: [
              {
                Type: FormValidationType.Required,
                Message: "This field is required.",
                Validation: Validators.required
              },
              {
                Type: FormValidationType.MaxLength,
                Message: "This field cannot be more than 6 characters.",
                Validation: Validators.maxLength(6)
              },
              {
                Type: FormValidationType.MinLength,
                Message: "This field cannot be more at least 2 characters.",
                Validation: Validators.minLength(2)
              }
            ]
          },
          {
            Type: FormGroupType.Input,
            Label: "MI",
            FormElement: {
              Name: "MiddleInitial",
              Type: FormInputType.Text,
              Placeholder: "M.I."
            }
          },
          {
            Type: FormGroupType.Input,
            Label: "Last Name",
            FormElement: {
              Name: "LastName",
              Type: FormInputType.Text,
              Placeholder: "Last Name"
            },
            Validators: [
              {
                Type: FormValidationType.Required,
                Message: "This field is required.",
                Validation: Validators.required
              }
            ]
          }
        ],
        CustomColumnWidths: [5, 2, 5]
      },
      {
        FormRowGroups: [
          {
            Type: FormGroupType.Input,
            Label: "Email",
            FormElement: {
              Name: "Email",
              Type: FormInputType.Text,
              Placeholder: "johndoe@email.com"
            },
            Validators: [
              {
                Type: FormValidationType.Required,
                Message: "This field is required.",
                Validation: Validators.required
              },
              {
                Type: FormValidationType.Email,
                Message: "This field must be a valid email.",
                Validation: Validators.email
              }
            ]
          }
        ],
        CustomColumnWidths: [8]
      },
      {
        FormRowGroups: [
          {
            Type: FormGroupType.Select,
            Label: "Review Rating",
            FormElement: {
              Name: "ReviewRating",
              Placeholder: "Select",
              SelectOptions: [
                {
                  key: "0",
                  value: "Zero Stars"
                },
                {
                  key: "1",
                  value: "One Stars"
                },
                {
                  key: "2",
                  value: "Two Stars"
                },
                {
                  key: "3",
                  value: "Three Stars"
                },
                {
                  key: "4",
                  value: "Four Stars"
                },
                {
                  key: "5",
                  value: "Five Stars"
                }
              ]
            },
            Validators: [
              {
                Type: FormValidationType.Required,
                Message: "This field is required.",
                Validation: Validators.required
              }
            ]
          },
          null,
          null,
          null
        ]
      },
      {
        FormRowGroups: [
          {
            Type: FormGroupType.Textarea,
            Label: "Comments",
            FormElement: {
              Name: "CommentBox",
              Placeholder: "Enter your comment here"
            },
            Validators: [
              {
                Type: FormValidationType.Required,
                Message: "This field is required.",
                Validation: Validators.required
              },
              {
                Type: FormValidationType.MaxLength,
                Message: "This field cannot be more than 256 characters.",
                Validation: Validators.maxLength(256)
              },
              {
                Type: FormValidationType.MinLength,
                Message: "This field cannot be less than 15 characters.",
                Validation: Validators.minLength(15)
              }
            ]
          }
        ]
      },
      {
        FormRowGroups: [
          {
            Type: FormGroupType.Input,
            Label: "Anonymous Review Only",
            FormElement: {
              Name: "ShouldBeAnonymousOnly",
              Type: FormInputType.Checkbox,
              CheckboxType: FormCheckboxType.LabelLast,
              CheckboxAsToggleSwitch: true
            }
          },
          {
            Type: FormGroupType.Input,
            Label: "Send Marketing Emails",
            FormElement: {
              Name: "ShouldSendMarketingEmails",
              Type: FormInputType.Checkbox,
              CheckboxType: FormCheckboxType.LabelLast,
              CheckboxAsToggleSwitch: true
            }
          }
        ]
      },
    ],

    ButtonAlignment: FormButtonAlignment.Right,
    ShowSubmitButton: true,
    ShowClearButton: true
  };
  private activateDemoMode() {
     this.Config = this.TestFormConfig
  }
}
