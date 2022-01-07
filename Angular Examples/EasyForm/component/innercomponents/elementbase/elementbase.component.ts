import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { isNumber } from 'util';
import AngularService from '../../../../Popups/services/angular.service';
import { FormCheckboxType, FormElementBase, FormInput, FormInputType, FormRowGroup } from '../../../models/form.models';
import FormService from '../../../services/form.service';

@Component({
  selector: 'easyform-element-base',
  template: ''
})
export class FormElementBaseComponent implements OnInit {
  @Output() public onLoad: EventEmitter<any> = new EventEmitter<any>();
  @Output() public onChange: EventEmitter<any> = new EventEmitter<any>();
  @Input() public RowGroup: FormRowGroup;

  public Element: FormElementBase;
  public Control: FormControl;

  public Id = `${this.service.generateSimpleGUID().substring(0, 24)}`;

  public ShowLabelBefore: boolean = true;
  public ShowLabelAfter: boolean = false;

  private DebounceTimeout: number = 250; //ms
  public Debounce: any;

  constructor(private service: AngularService, private formService: FormService) { }

  public ngOnInit(): void {
    if (this.RowGroup && this.RowGroup.FormElement) {
      this.initControl();
      this.setLabelPostition();
    }
  }

  public changed(): void {
    this.Debounce && clearTimeout(this.Debounce);
    this.Debounce = setTimeout(() => {
      this.onChange.emit({
        Name: this.Element.Name,
        Value: this.Control.value
      });
    }, this.DebounceTimeout);
  }
  public customChanged(value: any) {
    this.Debounce && clearTimeout(this.Debounce);
    this.Debounce = setTimeout(() => {
      this.onChange.emit({
        Name: this.Element.Name,
        Value: value
      });
    }, this.DebounceTimeout);
  }

  private initialChange(): void {
    let isCheckbox = (this.Element as FormInput)?.Type == FormInputType.Checkbox ?? false;
    let isNumberBox = (this.Element as FormInput)?.Type == FormInputType.Number ?? false;

    if (isCheckbox == true) {
      this.customChanged(Boolean(this.Element.Value));
    } else if (isNumberBox == true) {
      this.customChanged(Number(this.Element.Value));
    } else {
      this.changed();
    }
  }
  private initControl() {
    this.Element = this.RowGroup.FormElement;
    this.Control = new FormControl('', this.formService.getValidators(this.RowGroup));
    this.onLoad.emit(this.Control);
    this.initialChange();
  }
  private setLabelPostition() {
    if (this.RowGroup?.Label) {
      var input = this.Element as FormInput;
      if (input?.Type == FormInputType.Checkbox && input?.CheckboxType == FormCheckboxType.LabelLast) {
        this.ShowLabelBefore = false;
        this.ShowLabelAfter = true;
      } else {
        this.ShowLabelBefore = true;
        this.ShowLabelAfter = false;
      }
    }
  }
}
