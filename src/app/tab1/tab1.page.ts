import { Component, OnInit } from '@angular/core';
import { TokenService } from '../token.service';

@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss']
})
export class Tab1Page implements OnInit {
token: string;
  constructor(private svc: TokenService) {}

  ngOnInit() {
    this.token = this.svc.getToken;
  }

  update() {
    this.token = this.svc.getToken;
  }
}
