import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import ApiService from './services/api.service';
import AngularService from '../Popups/services/angular.service';
import { PopupModule } from '../Popups/popup.module';
import { FormComponent } from './component/form.component';
import { FormInputComponent } from './component/innercomponents/input/input.component';
import { FormSelectComponent } from './component/innercomponents/select/select.component';
import { FormTextareaComponent } from './component/innercomponents/textarea/textarea.component';
import { FormValidationComponent } from './component/innercomponents/validation/validation.component';
import FormService from './services/form.service';

@NgModule({
  declarations: [
    FormComponent,
    FormInputComponent,
    FormSelectComponent,
    FormTextareaComponent,
    FormValidationComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    PopupModule
  ],
  providers: [
    AngularService,
    ApiService,
    FormService
  ],
  exports: [
    FormComponent
  ]
})
export class EasyFormModule { }
