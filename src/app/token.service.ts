import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private token: string;

  constructor() { }

  get getToken() {
      return this.token;
  }

  setToken(option: string) {
      this.token = option;
  }


}
