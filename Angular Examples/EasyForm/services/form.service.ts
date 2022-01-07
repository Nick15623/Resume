import { KeyValue } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormControl, ValidatorFn } from '@angular/forms';
import { FormElementChange, FormRowGroup } from '../models/form.models';

@Injectable()
export default class FormService {
  public FormCache: Array<KeyValue<string, FormCacheValue>> = [];

  constructor() { }

  public cacheContainKey(formName: string) {
    return this.FormCache.some(x => x.key === formName);
  }
  public getFormObjectFromCache(formName: string): Object {
    if (this.cacheContainKey(formName)) {
      return this.FormCache.find(x => x.key === formName).value.FormObject;
    } else {
      return null;
    }
  }
  async updateCacheForElementChange(formName: string, change: FormElementChange) {
    if (this.cacheContainKey(formName)) {
      var keyValue = this.FormCache.find(x => x.key === formName);
      keyValue.value.FormObject[change.Name] = change.Value;
    } else {
      var keyValue = this.createNewCacheEntry(formName);
      keyValue.value[change.Name] = change.Value;
      this.FormCache.push(keyValue);
    }
  }
  async updateCacheForFormControl(formName: string, control: FormControl) {
    if (this.cacheContainKey(formName)) {
      var keyValue = this.FormCache.find(x => x.key === formName);
      keyValue.value.FormControls.push(control);
    } else {
      var keyValue = this.createNewCacheEntry(formName);
      keyValue.value.FormControls.push(control);
      this.FormCache.push(keyValue);
    }
  }

  async touchForm(formName: string) {
    if (this.cacheContainKey(formName)) {
      var keyValue = this.FormCache.find(x => x.key === formName);
      keyValue.value.FormControls.forEach(x => { x.markAllAsTouched(); });
    }
  }
  async clearForm(formName: string) {
    if (this.cacheContainKey(formName)) {
      var keyValue = this.FormCache.find(x => x.key === formName);
      keyValue.value.FormControls.forEach(x => {  x.reset(); });
    }
  }
  public isFormValid(formName: string): boolean {
    var isValid = true;
    if (this.cacheContainKey(formName)) {
      var keyValue = this.FormCache.find(x => x.key === formName);
      isValid = keyValue.value.FormControls.some(x => x.valid == false) == false;
    }
    return isValid;
  }

  public getValidators(rowGroup: FormRowGroup): Array<ValidatorFn> {
    if (rowGroup.Validators) {
      return rowGroup.Validators.map(x => x.Validation);
    } else {
      return [];
    }
  }

  private createNewCacheEntry(formName: string): KeyValue<string, FormCacheValue> {
    var cacheValue = { FormObject: new Object(), FormControls: [] } as FormCacheValue;
    return { key: formName, value: cacheValue } as KeyValue<string, FormCacheValue>;
  }
}

class FormCacheValue {
  public FormObject: Object;
  public FormControls: Array<FormControl>;
}
