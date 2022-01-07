import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { ConfirmModalConfig } from '../../../../models/modal.models';
import ModalService from '../../../../services/modal.service';
import { ModalBaseComponent } from '../../modal-base.component';

@Component({
  selector: 'modal-confirmation',
  templateUrl: './confirmation.html',
  styleUrls: ['./confirmation.scss']
})
export class ConfirmationModalComponent extends ModalBaseComponent implements OnInit {
  @Input() Config: ConfirmModalConfig;
  @Output() onConfirm: EventEmitter<any> = new EventEmitter<any>();

  public get themeClass() { return this.modalService.getThemeClass(this.Config); }
  public get themeBtnClass() { return this.modalService.getThemeBtnClass(this.Config); }

  constructor(private modalService: ModalService) {
    super();
  }

  public ngOnInit(): void { }

  public close() {
    this.onClose.emit(true);
  }

  public confirm() {
    this.onConfirm.emit(true);
  }

}
