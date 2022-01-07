import { Injectable } from "@angular/core";
import { ModalConfigBase, ModalSize, ModalThemeType } from "../models/modal.models";

@Injectable()
export default class ModalService {
  constructor() { }

  public getSizeClass(config: ModalConfigBase): string {
    switch (config.Size) {
      case ModalSize.ExtraSmall:
        return 'modal-xsm';
      case ModalSize.Small:
        return 'modal-sm';
      case ModalSize.Medium:
        return 'modal-md';
      case ModalSize.Large:
        return 'modal-lg';
      case ModalSize.ExtraLarge:
        return 'modal-xlg';
      case ModalSize.Fullscreen:
        return 'modal-fullscreen';
      default:
        return 'modal-md';
    }
  }
  public getThemeClass(config: ModalConfigBase): string {
    switch (config.Theme) {
      case ModalThemeType.Success:
        return 'modal-success';
      case ModalThemeType.Warning:
        return 'modal-warning';
      case ModalThemeType.Danger:
        return 'modal-danger';
      case ModalThemeType.Primary:
        return 'modal-primary';
      case ModalThemeType.Secondary:
        return 'modal-secondary';
      case ModalThemeType.Info:
        return 'modal-info';
      case ModalThemeType.Default:
      default:
        return '';
    }
  }
  public getThemeBtnClass(config: ModalConfigBase): string {
    switch (config.Theme) {
      case ModalThemeType.Success:
        return 'btn-success';
      case ModalThemeType.Warning:
        return 'btn-warning';
      case ModalThemeType.Danger:
        return 'btn-danger';
      case ModalThemeType.Primary:
        return 'btn-primary';
      case ModalThemeType.Secondary:
        return 'btn-secondary';
      case ModalThemeType.Info:
        return 'btn-info';
      case ModalThemeType.Default:
      default:
        return 'btn-light';
    }
  }
}
