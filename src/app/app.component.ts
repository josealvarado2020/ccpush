import { Component, OnInit } from '@angular/core';

import { Platform } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import {
  Plugins,
  PushNotification,
  PushNotificationToken,
  PushNotificationActionPerformed } from '@capacitor/core';
import { TokenService } from './token.service';
import { Router } from '@angular/router';

const { PushNotifications, Modals, Device, FCMPlugin } = Plugins;
import { FCM } from 'capacitor-fcm';
const fcm = new FCM();

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(
    private platform: Platform,
    private splashScreen: SplashScreen,
    private statusBar: StatusBar,
    private svc: TokenService,
    private router: Router
  ) {
    this.initializeApp();
  }

  initializeApp() {
    this.platform.ready().then(() => {
      this.statusBar.styleDefault();
      this.splashScreen.hide();
    });
  }

  async ngOnInit() {
    const info = await Device.getInfo();
    if (info.platform === 'ios' || info.platform === 'android') {
      // PushNotifications.register();
      PushNotifications.register()
      .then(() => {
        fcm
          .subscribeTo({ topic: 'marketing' })
          .then(r => console.log(`subscribed to topic`))
          .catch(err => console.log(err));
      })
      .catch(err => alert(JSON.stringify(err)));

      // fcm
      // .unsubscribeFrom({ topic: 'marketing' })
      // .then(() => alert(`unsubscribed from topic`))
      // .catch(err => console.log(err));

      PushNotifications.removeAllDeliveredNotifications();

    // On success, we should be able to receive notifications
      PushNotifications.addListener('registration', 
        (token: PushNotificationToken) => {
          // alert('Push registration success, token: ' + token.value);
          console.error('cc token: ', token);
          // send to api and save it in db
          this.svc.setToken(token.value);
        }
      );

      // fcm
      //   .getToken()
      //   .then(async r => {
      //     alert(`Token ${r.token}`);
      //     console.error('cc push notification token: ', r.token);
      //     const info = await Device.getInfo();
      //     let uuid = null;
      //     if (info) {
      //       uuid = info.uuid;
      //     }
      //     console.error('device uuid: ', uuid);
      //     this.svc.setToken(r.token);
      //   })
      //   .catch(err => console.log(err));


      // Some issue with our setup and push will not work
      PushNotifications.addListener('registrationError', 
        (error: any) => {
          alert('Error on registration: ' + JSON.stringify(error));
          console.error('cc error: ', error);
        }
      );

      // Show us the notification payload if the app is open on our device
      PushNotifications.addListener('pushNotificationReceived', 
        (notification: PushNotification) => {
          let test = Modals.alert({
            title: notification.title,
            message: notification.body
          });
        }
      );

      // Method called when tapping on a notification
      PushNotifications.addListener('pushNotificationActionPerformed', 
        (notification: PushNotificationActionPerformed) => {
          console.error('cc on tap: ', JSON.stringify(notification));
          // let test = Modals.alert({
          //   title: 'you clicked here',
          //   message: 'whatever we want to do when they click the notification' + notification.notification.data.loanId
          // });
          PushNotifications.removeAllDeliveredNotifications();
          console.log(notification.notification.data.loanId);
          this.router.navigate(['/', 'tabs', notification.notification.data.loanId]);
        }
      );

    }
  }
}
