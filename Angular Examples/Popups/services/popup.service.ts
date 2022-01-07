import { Injectable } from '@angular/core';
import { BannerComponent } from '../components/banner/banner.component';
import { ModalComponent } from '../components/modal/modal.component';
import { ToastComponent } from '../components/toast/toast.component';
import { WidgetComponent } from '../components/widget/widget.component';
import { BannerConfig } from '../models/banner.models';
import { AlertModalConfig, ComponentModalConfig, ConfirmModalConfig, ModalType } from '../models/modal.models';
import { ToastConfig } from '../models/toast.models';
import { WidgetConfig } from '../models/widget.models';
import AngularService from './angular.service';

@Injectable()
export default class PopupService {

  constructor(private service: AngularService) { }

  public Toast(Config: ToastConfig) {

    if (Config) {
      var guid = this.service.generateSimpleGUID();
      var inputs = [
        { key: "Guid", value: guid },
        { key: "Config", value: Config },
      ];

      this.service.addComponent(guid, ToastComponent, inputs);
      setTimeout(() => {
        if (Config && Config.OnClose) { Config.OnClose(); }
        this.service.removeComponent(guid, ToastComponent);
      }, Config.Timeout);
    }

  }
  public Modal(Config: AlertModalConfig | ConfirmModalConfig | ComponentModalConfig) {

    if (Config) {
      var guid = this.service.generateSimpleGUID();
      var modalType = this.getModalType(Config);
      var inputs = [
        { key: "Guid", value: guid },
        { key: "Type", value: modalType },
        { key: "Config", value: Config },
      ];

      this.service.addComponent(guid, ModalComponent, inputs);
    }

  }
  public Widget(Config: WidgetConfig) {

    if (Config) {
      var guid = this.service.generateSimpleGUID();
      var inputs = [
        { key: "Guid", value: guid },
        { key: "Config", value: Config },
      ];

      this.service.addComponent(guid, WidgetComponent, inputs);
    }

  }
  public Banner(Config: BannerConfig) {

    if (Config) {
      var guid = this.service.generateSimpleGUID();
      var inputs = [
        { key: "Guid", value: guid },
        { key: "Config", value: Config },
      ];

      this.service.addComponent(guid, BannerComponent, inputs);
    }

  }

  private getModalType(Config: AlertModalConfig | ConfirmModalConfig | ComponentModalConfig): ModalType {
    if (Config instanceof AlertModalConfig) { return ModalType.Alert; }
    else if (Config instanceof ConfirmModalConfig) { return ModalType.Confirmation; }
    else if (Config instanceof ComponentModalConfig) { return ModalType.CustomComponent; }
    else { return ModalType.Alert; }
  } 
}
