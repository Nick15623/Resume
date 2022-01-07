import { Component, OnInit, Input, ViewChild, ViewContainerRef, AfterViewInit, ElementRef } from '@angular/core';
import { ComponentModalConfig } from '../../../../models/modal.models';
import AngularService from '../../../../services/angular.service';
import ModalService from '../../../../services/modal.service';
import { ModalBaseComponent } from '../../modal-base.component';

@Component({
  selector: 'modal-custom-component',
  templateUrl: './customcomponent.html',
  styleUrls: ['./customcomponent.scss']
})
export class CustomComponentModalComponent extends ModalBaseComponent implements OnInit, AfterViewInit {
  @Input() Config: ComponentModalConfig;

  @ViewChild('CustomComponent') CustomComponent: ElementRef;

  public get themeClass() { return this.modalService.getThemeClass(this.Config); }
  public get themeBtnClass() { return this.modalService.getThemeBtnClass(this.Config); }

  private Id: string = `CustomComponent_${this.service.generateSimpleGUID()}`;

  constructor(private modalService: ModalService, private service: AngularService) {
    super();
  }

  public ngOnInit(): void { }

  public ngAfterViewInit(): void {
    if (this.Config?.Component && this.CustomComponent) {
      this.service.addComponent(this.Id, this.Config.Component, [], this.CustomComponent)
    }
  }

  public close() {
    if (this.Config?.Component) {
      this.service.removeComponent(this.Id, this.Config.Component);
    }
    this.onClose.emit(true);
  }

}
