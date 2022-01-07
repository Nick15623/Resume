
export class WidgetConfig {
  constructor(obj: WidgetConfig) {
    // Defaults
    this.Location = WidgetLocation.BottomRight;
    this.MaximizeTheme = WidgetThemeType.Default;
    this.MinimizeTheme = WidgetThemeType.Primary;
    this.Size = WidgetSize.Small;
    if (obj) { WidgetMapper.MapWidget(obj, this); }
  }

  public Component: any;
  public Location?: WidgetLocation; 
  public MaximizeTheme?: WidgetThemeType;
  public MinimizeTheme?: WidgetThemeType;
  public Size?: WidgetSize;
  public Icon?: string;

  public OnMaximize?: Function;
  public OnMinimize?: Function;
  public OnLoad?: Function;
  public OnClose?: Function;
}

export enum WidgetLocation {
  TopLeft,
  TopRight,
  BottomLeft,
  BottomRight
}

export enum WidgetThemeType {
  Default,
  Primary,
  Secondary,
  Info,
  Success,
  Warning,
  Danger
}

export enum WidgetSize {
  ExtraSmall,
  Small,
  Medium,
  Large,
  ExtraLarge,
  NearlyFullscreen
}

class WidgetMapper {

  public static MapWidget(from: WidgetConfig, to: WidgetConfig) {
    if (from.Component != undefined) { to.Component = from.Component; }
    if (from.Location != undefined) { to.Location = from.Location; }
    if (from.MinimizeTheme != undefined) { to.MinimizeTheme = from.MinimizeTheme; }
    if (from.MaximizeTheme != undefined) { to.MaximizeTheme = from.MaximizeTheme; }
    if (from.Size != undefined) { to.Size = from.Size; }
    if (from.Icon != undefined) { to.Icon = from.Icon; }
    if (from.OnMaximize != undefined) { to.OnMaximize = from.OnMaximize; }
    if (from.OnMinimize != undefined) { to.OnMinimize = from.OnMinimize; }
    if (from.OnLoad != undefined) { to.OnLoad = from.OnLoad; }
    if (from.OnClose != undefined) { to.OnClose = from.OnClose; }
  }
}

