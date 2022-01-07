import { Component } from '@angular/core';
import AngularService from '../../../../Popups/services/angular.service';
import { FormSelect } from '../../../models/form.models';
import FormService from '../../../services/form.service';
import { FormElementBaseComponent } from '../elementbase/elementbase.component';

@Component({
  selector: 'easy-select',
  templateUrl: './select.html',
  styleUrls: ['./select.scss']
})
export class FormSelectComponent extends FormElementBaseComponent {
  public get SelectElement(): FormSelect {
    return this.Element as FormSelect;
  };

  constructor(service: AngularService, formService: FormService) {
    super(service, formService);
    this.Id = `Select_${this.Id}`;
  }
}
