void Main()
{
	Init();
	
	var token = GetToken();
	Console.WriteLine(token);
	var response = SendSingleMessage();
//	var y = SendTopic();
//	var x = SendMultipleDevices();
//	Console.WriteLine("Successfully sent message: " + x.Result.SuccessCount);
	Console.WriteLine("done");
}

async Task<string> SendTopic()
{	
	var message = new Message()
	{
		Topic = "marketing",
		Data = new Dictionary<string, string>()
		{
			{"loanId", "tab2"}
		},
		Notification = new FirebaseAdmin.Messaging.Notification()
		{
			Title = "Marketing campaign",
			Body = "get on board with this new feature"
		},
		Android = new AndroidConfig()
		{
			TimeToLive = TimeSpan.FromHours(1),
			Notification = new AndroidNotification()
			{
				Tag = "loan",
				Color = "#F5A623",
				Icon = "ic_notification"
			}
		},
		Apns = new ApnsConfig()
		{
			Aps = new Aps()
			{
				Badge = 1,
				Sound = "default",
				Alert = new ApsAlert()
				{
					Subtitle = "only today!"
				}
			}
		}
	};
	return await FirebaseMessaging.DefaultInstance.SendAsync(message);
}

async Task<BatchResponse> SendMultipleDevices()
{
	var iosToken = "dj1pS0zT2ww:APA91bFr8NS88VsL6B7LXR_IbNdN3dzc9Buh4Pc-T7vRqT2cMDNnoUHLMmW9_qEZL7wTWFjmdMp3a6juKmGr4PSGou0K8Q02z4OVbCUAlKGaUlTrHmYC24Zk7lQmhO2XJE7tgHtpYOkA";
	var androidToken = "dej-Z1V-EUc:APA91bFfXPHVKkd8e68hFtzE3J7R5VXgjbpfwEZ8HbziD3LKB4HXdVlZ9Kja35gedLz1i4P4IPuKe7EbJcYG7bw8b0_rUN3Vc4NEXrAb72iMKrdCxbaQLNOUDx3HeVQh0z2aymmKqHSu";
	var tokens = new List<string>() {
		iosToken, androidToken
	};
	var message = new MulticastMessage()
	{
		Tokens = tokens,
		Data = new Dictionary<string, string>()
		{
			{"loanId", "tab2"}
		},
		Notification = new FirebaseAdmin.Messaging.Notification()
		{
			Title = "Loan approved",
			Body = "your money is on the way"
		},
		Android = new AndroidConfig()
		{
			TimeToLive = TimeSpan.FromHours(1),
			Notification = new AndroidNotification()
			{
				Tag = "loan",
				Color = "#F5A623",
				Icon = "ic_notification"
			}
		},
		Apns = new ApnsConfig()
		{
			Aps = new Aps()
			{
				Badge = 1,
				Sound = "default",
				Alert = new ApsAlert()
				{
					Subtitle = "$300 for loan"
				}
			}
		}
	};
	var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
	if (response.FailureCount > 0)
	{
		var failedTokens = new List<string>();
		for (var i = 0; i < response.Responses.Count; i++)
		{
			if (!response.Responses[i].IsSuccess)
			{
				// The order of responses corresponds to the order of the registration tokens.
				failedTokens.Add(tokens[i]);
			}
		}

		Console.WriteLine($"List of tokens that caused failures: {failedTokens}");
	}
	return response;
}

void Init()
{
	try
	{
		FirebaseApp.Create(new AppOptions()
		{
			Credential = GoogleCredential.GetApplicationDefault(),
		});
	}
	catch (Exception e)
	{
		var app = FirebaseAuth.DefaultInstance;
	}
}

async Task<string> SendSingleMessage()
{
	var iosToken = "dj1pS0zT2ww:APA91bFr8NS88VsL6B7LXR_IbNdN3dzc9Buh4Pc-T7vRqT2cMDNnoUHLMmW9_qEZL7wTWFjmdMp3a6juKmGr4PSGou0K8Q02z4OVbCUAlKGaUlTrHmYC24Zk7lQmhO2XJE7tgHtpYOkA";
	var androidToken = "dej-Z1V-EUc:APA91bFfXPHVKkd8e68hFtzE3J7R5VXgjbpfwEZ8HbziD3LKB4HXdVlZ9Kja35gedLz1i4P4IPuKe7EbJcYG7bw8b0_rUN3Vc4NEXrAb72iMKrdCxbaQLNOUDx3HeVQh0z2aymmKqHSu";
	var message = new Message() {
		Token = androidToken,
		Data = new Dictionary<string, string>()
		{
			{"loanId", "tab2"}
		},
		Notification = new FirebaseAdmin.Messaging.Notification()
		{
			Title = "Loan approved",
			Body = "your money is on the way"
		},
		Android = new AndroidConfig()
		{
			TimeToLive = TimeSpan.FromHours(1),
			Notification = new AndroidNotification(){
				Tag = "loan",
				Color = "#F5A623",
				Icon = "ic_notification"
			}
		},
		Apns = new ApnsConfig()
		{
			Aps = new Aps()
			{
				Badge = 1,
				Sound = "default",
				Alert = new ApsAlert() {
					Subtitle = "$300 for loan"
				}
			}
		}
	};
	return await FirebaseMessaging.DefaultInstance.SendAsync(message);
}

async Task<string> GetToken()
{
	GoogleCredential credential;
	using (var stream = new System.IO.FileStream("C:\\Users\\jose.alvarado\\Downloads\\pktest.json",
		System.IO.FileMode.Open, System.IO.FileAccess.Read))
	{
		credential = GoogleCredential.FromStream(stream).CreateScoped(
			new string[] {
				"https://www.googleapis.com/auth/firebase",
				"https://www.googleapis.com/auth/firebase.messaging" }
			);
	}

	ITokenAccess c = credential as ITokenAccess;
	return await c.GetAccessTokenForRequestAsync();
}
