import { Component, OnInit, Input } from '@angular/core';
import { AlertModalConfig, ComponentModalConfig, ConfirmModalConfig, ModalType } from '../../models/modal.models';
import AngularService from '../../services/angular.service';
import ModalService from '../../services/modal.service';

@Component({
  selector: 'modal',
  templateUrl: './modal.html',
  styleUrls: ['./modal.scss']
})
export class ModalComponent implements OnInit {

  @Input() Guid: string;
  @Input() Type: ModalType;
  @Input() Config: AlertModalConfig | ConfirmModalConfig | ComponentModalConfig;

  public ModalTypeEnum = ModalType;
  public ShowIntro: boolean = true;
  public ShowOutro: boolean = false;

  public get sizeClass() { return this.modalService.getSizeClass(this.Config); }

  constructor(private service: AngularService, private modalService: ModalService) { }

  public ngOnInit(): void {
    if (this.Config && this.Config.OnLoad) {
      this.Config.OnLoad();
    }
  }

  public close() {
    this.ShowIntro = false;
    this.ShowOutro = true;
    setTimeout(() => {
      if (this.Config && this.Config.OnClose) { this.Config.OnClose(); }
      this.service.removeComponent(this.Guid, ModalComponent);
    }, 250); // 250ms is outro animation length
  }

  public confirm(response: boolean) { // Only applies to Confirm Modals
    var config = this.Config as ConfirmModalConfig;
    if (config) {
      if (config.OnConfirm) {
        var customResponse = config.OnConfirm();
        this.handleConfirmResponse(customResponse);
      } else {
        this.handleConfirmResponse(response);
      }
    }
  }

  private handleConfirmResponse(response: boolean) {
    if (response == true) {
      this.service.removeComponent(this.Guid, ModalComponent);
      if (this.Config && this.Config.OnClose) {
        this.Config.OnClose();
      }
    } else {
      // TBD
    }
  }
}
