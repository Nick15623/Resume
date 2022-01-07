

export class BannerConfig {
  constructor(obj: BannerConfig) {
    // Defaults
    this.Location = BannerLocation.Top;
    this.ContentAlignment = BannerContentAlignmentType.Left;
    this.Theme = BannerThemeType.Default;
    this.Buttons = [];

    if (obj) { BannerMapper.MapBanner(obj, this); }
  }

  public Location?: BannerLocation;
  public ContentAlignment?: BannerContentAlignmentType;
  public Theme?: BannerThemeType;
  public Component?: any;
  public Message?: string;
  public Buttons?: Array<BannerButton>;

  public OnLoad?: Function;
  public OnClose?: Function;
}

export class BannerButton {
  public Theme?: BannerThemeType;
  public ButtonText?: string;
  public OnClick?: Function;
  public CloseAfterClick?: boolean;
}

export enum BannerLocation {
  Top,
  Bottom
}
export enum BannerContentAlignmentType {
  Left,
  Center,
  Right,
  SpaceBetween
}
export enum BannerThemeType {
  Default,
  Primary,
  Secondary,
  Info,
  Success,
  Warning,
  Danger
}

class BannerMapper {

  public static MapBanner(from: BannerConfig, to: BannerConfig) {
    if (from.Location != undefined) { to.Location = from.Location; }
    if (from.ContentAlignment != undefined) { to.ContentAlignment = from.ContentAlignment; }
    if (from.Theme != undefined) { to.Theme = from.Theme; }
    if (from.Component != undefined) { to.Component = from.Component; }
    if (from.Message != undefined) { to.Message = from.Message; }
    if (from.Buttons != undefined) { to.Buttons = from.Buttons; }
    if (from.OnLoad != undefined) { to.OnLoad = from.OnLoad; }
    if (from.OnClose != undefined) { to.OnClose = from.OnClose; }
  }
}

