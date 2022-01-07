import { KeyValue } from '@angular/common';
import { Component } from '@angular/compiler/src/core';
import { ApplicationRef, ComponentFactory, ComponentRef, ElementRef, EmbeddedViewRef, Injector, Type, ViewChild, ViewContainerRef } from '@angular/core';
import { ComponentFactoryResolver, Injectable } from '@angular/core';
import { Key } from 'protractor';
import { ModalComponent } from '../components/modal/modal.component';

@Injectable()
export default class AngularService {

  private componentRefs: Array<KeyValue<string, ComponentRef<unknown>>> = [];

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private appRef: ApplicationRef,
    private injector: Injector
  ) { }

  public addComponent(name: string, component: any, params: Array<KeyValue<string, any>> = [], containerOverride: ElementRef = null): void {
    if (name && component) {
      const componentRef = this.addComponentRef(name, component, params);
      if (componentRef) {
        this.appRef.attachView(componentRef.hostView);
        const embeddedViewRef = componentRef.hostView as EmbeddedViewRef<any>;
        const htmlElem = embeddedViewRef.rootNodes[0] as HTMLElement;

        if (containerOverride) {
          containerOverride.nativeElement.appendChild(htmlElem);
        } else {
          document.body.appendChild(htmlElem);
        }
      }
    }
  }
  public removeComponent(name: string, component: any): void {
    if (name && component) {
      const componentRef = this.removeComponentRef(name, component);
      if (componentRef) {
        this.appRef.detachView(componentRef.hostView);
        componentRef.destroy();
      }
    }
  }
  public generateSimpleGUID(): string {
    var now = performance.now();
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, (c) => {
      var r = (now + Math.random()) * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }

  private addComponentRef(name: string, component: any, inputs: Array<KeyValue<string, any>> = []): ComponentRef<unknown> {
    try {
      const componentFactory = this.componentFactoryResolver.resolveComponentFactory(component);
      const componentRef = componentFactory.create(this.injector);

      if (inputs && inputs.length > 0) {
        componentFactory.inputs.forEach(x => {
          var input = inputs.find(p => p.key === x.propName);
          if (input) { componentRef.instance[input.key] = input.value; }
        });
      }

      this.componentRefs.push({ key: this.getKey(name, component), value: componentRef });
      return componentRef;
    } catch (e) {
      console.log(e);
      return null;
    }
  }
  private removeComponentRef(name: string, component: any): ComponentRef<unknown> {
    try {
      var componentRefIndex = this.componentRefs.findIndex(x => x.key === this.getKey(name, component));
      if (componentRefIndex >= 0) {
        var keyValuePair = this.componentRefs.splice(componentRefIndex, 1)[0];
        return keyValuePair.value;
      } else {
        return null;
      }
    } catch (e) {
      console.log(e);
      return null;
    }
  }
  private getKey(name: string, component: any): string {
    let componentName = component.name;
    return `${componentName ? componentName : 'Unknown'}_${name}`;
  }
}





// Inspired by - https://medium.com/hackernoon/angular-pro-tip-how-to-dynamically-create-components-in-body-ba200cc289e6
