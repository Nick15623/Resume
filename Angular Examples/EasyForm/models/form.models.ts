
import { KeyValue } from "@angular/common";
import { ValidatorFn } from "@angular/forms";
import ApiService from "../services/api.service";

export class FormConfig {
  public Name: string;
  public Title?: string;
  public Description?: string;

  public FormRows: Array<FormRow>;

  public ButtonAlignment?: FormButtonAlignment;
  public ShowSubmitButton?: boolean = false;
  public SubmitButtonText?: string;
  public OnSubmit?: Function;
  public ShowClearButton?: boolean = false;
  public ClearButtonText?: string;
  public OnClear?: Function;

  public DemoMode?: boolean = false;
}

export enum FormButtonAlignment {
  Left,
  Center,
  Right,
  Between
}

export class FormRow {
  public FormRowGroups: Array<FormRowGroup>;
  public CustomColumnWidths?: Array<number>;
}

export class FormRowGroup {

  constructor() {
    this.Validators = [];
  }

  public Type: FormGroupType;
  public FormElement: FormInput | FormSelect | FormTextarea; 
  public Validators?: Array<FormValidation>;
  public Label?: string;
}

export enum FormGroupType {
  Input,
  Select,
  Textarea
}

export class FormElementBase {
  public Name: string;
  public Value?: string;
  public Placeholder?: string;
  public Title?: string;

  public Disabled?: boolean;
  public Readonly?: boolean;
}

export class FormInput extends FormElementBase {
  public Type: FormInputType;
  public CheckboxType?: FormCheckboxType;
  public CheckboxAsToggleSwitch?: boolean;
}

export enum FormInputType {
  Checkbox,
  Email,
  Hidden,
  Number,
  Passsword,
  Telephone,
  Text
}

export enum FormCheckboxType {
  LabelFirst,
  LabelLast
}

export class FormSelect extends FormElementBase {
  public SelectOptions: Array<KeyValue<string, string>>;
}

export class FormTextarea extends FormElementBase {
}

export class FormValidation {
  public Type: FormValidationType;
  public Message: string;
  public Validation: ValidatorFn;
}

export enum FormValidationType {
  Required,
  Email,
  Pattern,
  MinLength,
  MaxLength,
  Custom
}

// Endpoints

export class Endpoint {
  public Type: EndpointType;
  public Url: string;
  public QueryString?: Array<QueryString>;
  public Before?: Function; // Return true to continue. False to prevent.
  public After?: Function;
  public Error?: Function;

  // OVERRIDE FUNCTION
  // GETS: Gives the entire row's object (null if create)
  // RETURNS: Gives an object back (null if delete) 
  public Override?: Function;

}

export class EndpointHelper {
  public static async ExecuteAsync(service: ApiService, endpoint: Endpoint, model: any = null): Promise<any> {
    return await service.handleRequest(endpoint, model);
  }
}

export enum EndpointType {
  GET,
  POST,
  PUT,
  DELETE
}

export class QueryString { // Param=PropertyValue
  public Param: string; 
  public PropertyName: string;
  public CustomValue?: Function; // Gives function the property for modifying it before url creation
}

export class FormElementChange {
  public Name: string;
  public Value: any;
}
