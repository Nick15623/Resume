
export enum ModalType {
  Alert,
  Confirmation,
  CustomComponent
}

export class ModalConfigBase {
  constructor() {
    // Defaults
    this.Size = ModalSize.Medium;
    this.ShowHeader = true;
    this.ShowFooter = true;
    this.CloseOnOuterClick = false;
    this.FuzzyBackground = false;
  }

  public Title?: string;
  public Theme?: ModalThemeType;
  public Icon?: string;
  public Size?: ModalSize;

  public ShowHeader?: boolean;
  public ShowFooter?: boolean;

  public CloseOnOuterClick?: boolean;
  public FuzzyBackground?: boolean;

  public OnLoad?: Function;
  public OnClose?: Function;
}

export enum ModalThemeType {
  Default,
  Primary,
  Secondary,
  Info,
  Success,
  Warning,
  Danger
}

export enum ModalSize {
  ExtraSmall,
  Small,
  Medium,
  Large,
  ExtraLarge,
  Fullscreen
}

export class AlertModalConfig extends ModalConfigBase {
  constructor(obj: AlertModalConfig) {
    super();
    if (obj) { ModalMapper.MapAlertModal(obj, this); }
  }
  public Message?: string;
  public CustomMessage?: Function;
  public ButtonText?: string;
}

export class ConfirmModalConfig extends ModalConfigBase {
  constructor(obj: ConfirmModalConfig) {
    super();
    if (obj) { ModalMapper.MapConfirmModal(obj, this); }
  }
  public Message?: string;
  public CustomMessage?: Function;
  public CloseButtonText?: string;
  public ConfirmButtonText?: string;

  public OnConfirm?: Function;
}

export class ComponentModalConfig extends ModalConfigBase {
  constructor(obj: ComponentModalConfig) {
    super();
    if (obj) { ModalMapper.MapComponentModal(obj, this); }
  }
  public Component: any;
  public ButtonText?: string;
}

class ModalMapper {

  public static MapAlertModal(from: AlertModalConfig, to: AlertModalConfig) {
    if (from.Message != undefined) { to.Message = from.Message; }
    if (from.ButtonText != undefined) { to.ButtonText = from.ButtonText; }
    if (from.CustomMessage != undefined) { to.CustomMessage = from.CustomMessage; }
    this.MapBaseModal(from, to);
  }
  public static MapConfirmModal(from: ConfirmModalConfig, to: ConfirmModalConfig) {
    if (from.Message != undefined) { to.Message = from.Message; }
    if (from.CloseButtonText != undefined) { to.CloseButtonText = from.CloseButtonText; }
    if (from.ConfirmButtonText != undefined) { to.ConfirmButtonText = from.ConfirmButtonText; }
    if (from.CustomMessage != undefined) { to.CustomMessage = from.CustomMessage; }
    if (from.OnConfirm != undefined) { to.OnConfirm = from.OnConfirm; }
    this.MapBaseModal(from, to);
  }
  public static MapComponentModal(from: ComponentModalConfig, to: ComponentModalConfig) {
    if (from.Component != undefined) { to.Component = from.Component; }
    if (from.ButtonText != undefined) { to.ButtonText = from.ButtonText; }
    this.MapBaseModal(from, to);
  }

  public static MapBaseModal(from: ModalConfigBase, to: ModalConfigBase) {
    if (from.Title != undefined) { to.Title = from.Title; }
    if (from.Theme != undefined) { to.Theme = from.Theme; }
    if (from.Icon != undefined) { to.Icon = from.Icon; }
    if (from.Size != undefined) { to.Size = from.Size; }
    if (from.ShowHeader != undefined) { to.ShowHeader = from.ShowHeader; }
    if (from.ShowFooter != undefined) { to.ShowFooter = from.ShowFooter; }
    if (from.CloseOnOuterClick != undefined) { to.CloseOnOuterClick = from.CloseOnOuterClick; }
    if (from.FuzzyBackground != undefined) { to.FuzzyBackground = from.FuzzyBackground; }
    if (from.OnLoad != undefined) { to.OnLoad = from.OnLoad; }
    if (from.OnClose != undefined) { to.OnClose = from.OnClose; }
  }
}

