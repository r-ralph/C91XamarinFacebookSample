﻿using System;
using Android.Content;

namespace C91XamarinFacebookSample.Droid
{
	[BroadcastReceiver]
	public class FacebookLoginReceiver : BroadcastReceiver
	{
		public const string ActionKey = "FacebookLoginReceiver.Action";
		public const string ExtraResult = "FacebookLoginReceiver.Result";
		public const string ExtraToken = "FacebookLoginReceiver.Token";
		public event Action<Result, string> LoginFinished;
		public override void OnReceive(Context context, Intent intent)
		{
			var result = Result.Unknown;
			var token = string.Empty;
			if (intent.Action != ActionKey) return;
			if (intent.Extras != null && intent.Extras.ContainsKey(ExtraResult))
			{
				result = (Result)Enum.ToObject(typeof(Result), intent.Extras.
			GetInt(ExtraResult));
			}
			if (intent.Extras != null && intent.Extras.ContainsKey(ExtraToken))
			{
				token = intent.Extras.GetString(ExtraToken);
			}
			if (LoginFinished != null)
			{
				LoginFinished(result, token);
			}
		}
	}
}
