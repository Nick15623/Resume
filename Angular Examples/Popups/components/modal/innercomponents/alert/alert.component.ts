import { Component, OnInit, Input } from '@angular/core';
import { AlertModalConfig } from '../../../../models/modal.models';
import ModalService from '../../../../services/modal.service';
import { ModalBaseComponent } from '../../modal-base.component';

@Component({
  selector: 'modal-alert',
  templateUrl: './alert.html',
  styleUrls: ['./alert.scss']
})
export class AlertModalComponent extends ModalBaseComponent implements OnInit {
  @Input() Config: AlertModalConfig;

  public get themeClass() { return this.modalService.getThemeClass(this.Config); }
  public get themeBtnClass() { return this.modalService.getThemeBtnClass(this.Config); }

  constructor(private modalService: ModalService) {
    super();
  }

  public ngOnInit(): void { }

  public close() {
    this.onClose.emit(true);
  }

}
