import { Component, OnInit, Input } from '@angular/core';
import { ToastConfig, ToastThemeType } from '../../models/toast.models';

@Component({
  selector: 'toast',
  templateUrl: './toast.html',
  styleUrls: ['./toast.scss']
})
export class ToastComponent implements OnInit {

  @Input() Guid: string;
  @Input() Config: ToastConfig;

  public ShowIntro: boolean = true;
  public ShowOutro: boolean = false;

  constructor() { }

  public ngOnInit(): void {
    if (this.Config) {
      if (this.Config.OnLoad) {
        this.Config.OnLoad();
      }

      setTimeout(() => {
        this.ShowIntro = false;
        this.ShowOutro = true;
      }, this.Config.Timeout - 400);
    }


  }

  public getThemeClass(): string {
    switch (this.Config.Theme) {
      case ToastThemeType.Success:
        return 'toast-success';
      case ToastThemeType.Warning:
        return 'toast-warning';
      case ToastThemeType.Danger:
        return 'toast-danger';
      case ToastThemeType.Primary:
        return 'toast-primary';
      case ToastThemeType.Secondary:
        return 'toast-secondary';
      case ToastThemeType.Info:
        return 'toast-info';
      case ToastThemeType.Default:
      default:
        return '';
    }
  }

}
