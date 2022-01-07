import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'modal',
  templateUrl: './modal.html',
  styleUrls: ['./modal.scss']
})
export class ModalBaseComponent {
  @Output() onClose: EventEmitter<any> = new EventEmitter<any>(); 
  constructor() { }
}
