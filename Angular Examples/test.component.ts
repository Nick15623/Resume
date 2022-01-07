import { Component, Input, OnInit } from '@angular/core';
import PopupService from '../../Popups/services/popup.service';
import { ToastConfig } from '../../Popups/models/toast.models';
import { FormConfig } from '../../EasyForm/models/form.models';


@Component({
  selector: 'how-to',
  templateUrl: './test.html',
  styleUrls: ['./test.scss']
})
export class HowToComponent implements OnInit {

  public DemoFormConfig: FormConfig = {
    Name: null,
    FormRows: null,
    DemoMode: true
  };

  constructor(private popupService: PopupService) { }

  public ngOnInit(): void {

    this.popupService.Toast(new ToastConfig({
      Message: "Example Text"
    }));

  }
}


