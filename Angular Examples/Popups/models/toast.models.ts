

export class ToastConfig {
  constructor(obj: ToastConfig) {
    // Defaults
    this.Timeout = 3000;

    if (obj) { ToastMapper.MapToast(obj, this); }
  }

  public Message: string;
  public Theme?: ToastThemeType;
  public Icon?: string;
  public Timeout?: number;

  public OnLoad?: Function;
  public OnClose?: Function;
}

export enum ToastThemeType {
  Default,
  Primary,
  Secondary,
  Info,
  Success,
  Warning,
  Danger
}

class ToastMapper {

  public static MapToast(from: ToastConfig, to: ToastConfig) {
    if (from.Message != undefined) { to.Message = from.Message; }
    if (from.Theme != undefined) { to.Theme = from.Theme; }
    if (from.Icon != undefined) { to.Icon = from.Icon; }
    if (from.Timeout != undefined) { to.Timeout = from.Timeout; }
    if (from.OnLoad != undefined) { to.OnLoad = from.OnLoad; }
    if (from.OnClose != undefined) { to.OnClose = from.OnClose; }
  }
}

