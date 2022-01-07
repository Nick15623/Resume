import { Component, OnInit, Input, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { BannerButton, BannerConfig, BannerContentAlignmentType, BannerLocation, BannerThemeType } from '../../models/banner.models';
import AngularService from '../../services/angular.service';

@Component({
  selector: 'Banner',
  templateUrl: './Banner.html',
  styleUrls: ['./Banner.scss']
})
export class BannerComponent implements OnInit, AfterViewInit {

  @Input() Guid: string;
  @Input() Config: BannerConfig;

  @ViewChild('CustomComponent') CustomComponent: ElementRef;
  private Id: string = `CustomComponent_${this.service.generateSimpleGUID()}`;

  public ShowIntro: boolean = true;
  public ShowOutro: boolean = false;

  public AlignmentType = BannerContentAlignmentType;

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

  public onClick(btn: BannerButton) {
    if (btn) {
      if (btn.OnClick) {
        btn.OnClick();
      }
      if (btn.CloseAfterClick) {
        this.onClose();
      }
    }
  }
  public onClose() {
    if (this.Config && this.Config.OnClose) {
      this.Config.OnClose();
    }
    this.ShowIntro = false;
    this.ShowOutro = true;
    setTimeout(() => { this.service.removeComponent(this.Guid, BannerComponent); }, 240);
  }

  public getLocationClass(): string {
    switch (this.Config.Location) {
      case BannerLocation.Top:
        return 'banner-top';
      case BannerLocation.Bottom:
        return 'banner-bottom';
      default:
        return '';
    }
  }
  public getThemeClass(): string {
    switch (this.Config.Theme) {
      case BannerThemeType.Success:
        return 'banner-success';
      case BannerThemeType.Warning:
        return 'banner-warning';
      case BannerThemeType.Danger:
        return 'banner-danger';
      case BannerThemeType.Primary:
        return 'banner-primary';
      case BannerThemeType.Secondary:
        return 'banner-secondary';
      case BannerThemeType.Info:
        return 'banner-info';
      case BannerThemeType.Default:
      default:
        return 'banner-fuzzy';
    }
  }
  public getAlignmentClass(): string {
    switch (this.Config.ContentAlignment) {
      case BannerContentAlignmentType.Left:
        return 'banner-content-left';
      case BannerContentAlignmentType.Center:
        return 'banner-content-center';
      case BannerContentAlignmentType.Right:
        return 'banner-content-right';
      case BannerContentAlignmentType.SpaceBetween:
        return 'banner-content-between';
      default:
        return 'banner-content-left';
    }
  }
  public getButtonThemeClass(btn: BannerButton): string {
    switch (btn.Theme) {
      case BannerThemeType.Success:
        return 'btn-success';
      case BannerThemeType.Warning:
        return 'btn-warning';
      case BannerThemeType.Danger:
        return 'btn-danger';
      case BannerThemeType.Primary:
        return 'btn-primary';
      case BannerThemeType.Secondary:
        return 'btn-secondary';
      case BannerThemeType.Info:
        return 'btn-info';
      case BannerThemeType.Default:
      default:
        return 'btn-light';
    }
  }

}
