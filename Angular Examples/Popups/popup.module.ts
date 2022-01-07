import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import AngularService from './services/angular.service';
import PopupService from './services/popup.service';
import ModalService from './services/modal.service';

import { ModalComponent } from './components/modal/modal.component';
import { AlertModalComponent } from './components/modal/innercomponents/alert/alert.component';
import { ConfirmationModalComponent } from './components/modal/innercomponents/confirmation/confirmation.component';
import { CustomComponentModalComponent } from './components/modal/innercomponents/customcomponent/customcomponent.component';
import { ToastComponent } from './components/toast/toast.component';
import { WidgetComponent } from './components/widget/widget.component';
import { BannerComponent } from './components/banner/banner.component';

@NgModule({
  declarations: [
    ToastComponent,
    ModalComponent,
    AlertModalComponent,
    ConfirmationModalComponent,
    CustomComponentModalComponent,
    WidgetComponent,
    BannerComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    AngularService,
    PopupService,
    ModalService
  ],
  exports: [
    ModalComponent
  ]
})
export class PopupModule { }
