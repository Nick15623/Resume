import { Component, OnInit, Input, ElementRef, ViewChild, TemplateRef, Query, AfterViewInit } from '@angular/core';
import { WidgetConfig, WidgetLocation, WidgetSize, WidgetThemeType } from '../../models/widget.models';
import AngularService from '../../services/angular.service';
import PopupService from '../../services/popup.service';

@Component({
  selector: 'widget',
  templateUrl: './widget.html',
  styleUrls: ['./widget.scss']
})
export class WidgetComponent implements OnInit, AfterViewInit {

  @Input() Guid: string;
  @Input() Config: WidgetConfig;

  @ViewChild('CustomComponent') CustomComponent: ElementRef;
  private Id: string = `CustomComponent_${this.service.generateSimpleGUID()}`;

  public ShowIntro: boolean = true;
  public ShowOutro: boolean = false;
  public IsOpen: boolean = false;

  public ShowMax: boolean = true;
  public ShowMin: boolean = false;

  constructor(private service: AngularService) { }

  public ngOnInit(): void {
    if (this.Config) {
      if (this.Config.OnLoad) {
        this.Config.OnLoad();
      }
    }
  }

  public ngAfterViewInit(): void {
    if (this.Config?.Component && this.CustomComponent) {
      this.service.addComponent(this.Id, this.Config.Component, [], this.CustomComponent)
    }
  }

  public onMaximize() {
    if (this.IsOpen == false) {
      if (this.Config && this.Config.OnMaximize) {
        this.Config.OnMaximize();
      }

      this.ShowMin = false;
      this.ShowMax = true;
      this.IsOpen = true;
    }
  }
  public onMinimize() {
    if (this.IsOpen == true) {
      if (this.Config && this.Config.OnMinimize) {
        this.Config.OnMinimize();
      }

      this.ShowMax = false;
      this.ShowMin = true;
      setTimeout(() => { this.IsOpen = false; }, 400);
    }
  }
  public onClose() {
    if (this.Config?.Component) {
      this.service.removeComponent(this.Id, this.Config.Component);
    }
    if (this.Config && this.Config.OnClose) {
      this.Config.OnClose();
    }

    this.ShowIntro = false;
    this.ShowOutro = true;
    setTimeout(() => { this.service.removeComponent(this.Guid, WidgetComponent); }, 240);
  }

  public getLocationClass(): string {
    switch (this.Config.Location) {
      case WidgetLocation.TopLeft:
        return 'widget-top-left';
      case WidgetLocation.TopRight:
        return 'widget-top-right';
      case WidgetLocation.BottomLeft:
        return 'widget-bottom-left';
      case WidgetLocation.BottomRight:
        return 'widget-bottom-right';
    }
  }
  public getSizeClass(): string {
    switch (this.Config.Size) {
      case WidgetSize.ExtraSmall:
        return 'widget-xsm';
      case WidgetSize.Small:
        return 'widget-sm';
      case WidgetSize.Medium:
        return 'widget-md';
      case WidgetSize.Large:
        return 'widget-lg';
      case WidgetSize.ExtraLarge:
        return 'widget-xlg';
      case WidgetSize.NearlyFullscreen:
        return 'widget-fullscreen';
      default:
        return 'widget-md';
    }
  }
  public getMaxThemeClass(): string {
    return this.getThemeClass(this.Config.MaximizeTheme);
  }
  public getMinThemeClass(): string {
    return this.getThemeClass(this.Config.MinimizeTheme);
  }

  private getThemeClass(type: WidgetThemeType): string {
    switch (type) {
      case WidgetThemeType.Success:
        return 'widget-success';
      case WidgetThemeType.Warning:
        return 'widget-warning';
      case WidgetThemeType.Danger:
        return 'widget-danger';
      case WidgetThemeType.Primary:
        return 'widget-primary';
      case WidgetThemeType.Secondary:
        return 'widget-secondary';
      case WidgetThemeType.Info:
        return 'widget-info';
      case WidgetThemeType.Default:
      default:
        return '';
    }
  }
}
