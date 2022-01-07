import { Component } from '@angular/core';
import AngularService from '../../../../Popups/services/angular.service';
import { FormTextarea } from '../../../models/form.models';
import FormService from '../../../services/form.service';
import { FormElementBaseComponent } from '../elementbase/elementbase.component';

@Component({
  selector: 'easy-textarea',
  templateUrl: './textarea.html',
  styleUrls: ['./textarea.scss']
})
export class FormTextareaComponent extends FormElementBaseComponent {
  public get TextareaElement(): FormTextarea {
    return this.Element as FormTextarea;
  };

  constructor(service: AngularService, formService: FormService) {
    super(service, formService);
    this.Id = `Textarea_${this.Id}`;
  }
}
