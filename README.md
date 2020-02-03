# ccpush
spike for push notifications using ionic

This spike uses firebase message centre and ionic:
console: https://firebase.google.com/docs/cloud-messaging/send-message

## Request
to test send a POST request to: https://fcm.googleapis.com/v1/projects/mobile-app-test-254800/messages:send
### body
{
   "message":{
      "token":"dYLgaP9A3iY:APA91bFHZUlUqiSnfwK_u5qOP4SzECQvLd1stqZpCpnkSlIeEa8fawUiGdVUmJaK1mW9DhAISwJzmKQxF4flrTJiEKLZ-xQV6bbOGNHNtDDyfBDs8aHBQRyevmtj1cgH7XejR4NKlBMf",
      "notification":{
      	"title":"Your loan has been approved!",
        "body":"Congratulations! your money is already in your bank account"
      },
     "android":{
       "ttl":"3600s",
       "notification":{
         "tag":"loan",
         "color":"#F5A623",
         "default_sound": true,
         "icon": "ic_notification"
       }
     },
     "apns":{
       "payload":{
         "aps":{
           "badge":1,
           "alert": {
           	"subtitle": "$100 for other"
           },
           "sound": "default"
         }
       }
     },
     "data": {
     	"loanId" : "tab2"
     }
   }
}

make sure to add an authorization header with bearer token
