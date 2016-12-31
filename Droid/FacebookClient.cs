using System;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;

namespace C91XamarinFacebookSample.Droid
{
	public class FacebookClient
	{
		MainActivity activity;
		ICallbackManager callbackManager;
		public FacebookClient(MainActivity activity)
		{
			this.activity = activity;
		}
		public void Init()
		{
			FacebookSdk.SdkInitialize(activity.ApplicationContext);
			FacebookSdk.ApplicationId = "xxxxxxxxxxxxxxxxxxxxxxxxxx";
			callbackManager = CallbackManagerFactory.Create();
			var loginCallback = new FacebookCallback<LoginResult>
			{
				HandleSuccess = loginResult =>
				{
					var token = loginResult.AccessToken;
					var intent = new Intent(FacebookLoginReceiver.ActionKey);
					intent.PutExtra(FacebookLoginReceiver.ExtraResult, (int)Result.Success);
					intent.PutExtra(FacebookLoginReceiver.ExtraToken, token.Token);
					activity.SendBroadcast(intent);
				},
				HandleCancel = () =>
				{
					var intent = new Intent(FacebookLoginReceiver.ActionKey);
					intent.PutExtra(FacebookLoginReceiver.ExtraResult, (int)Result.Cancel);
					activity.SendBroadcast(intent);
				},
				HandleError = loginError =>
				{
					var intent = new Intent(FacebookLoginReceiver.ActionKey);
					intent.PutExtra(FacebookLoginReceiver.ExtraResult, (int)Result.Error);
					activity.SendBroadcast(intent);
				}
			};
			LoginManager.Instance.RegisterCallback(callbackManager, loginCallback);
		}

		public void OnActivityResult(int requestCode, int resultCode, Intent data)
		{
			callbackManager.OnActivityResult(requestCode, resultCode, data);
		}

		// API
		internal void Login()
		{
			LoginManager.Instance.LogInWithReadPermissions(activity, new List<string> { "email" });
		}
	}

	class FacebookCallback<TResult> : Java.Lang.Object, IFacebookCallback where TResult : Java.Lang.Object
	{
		public Action HandleCancel { get; set; }
		public Action<FacebookException> HandleError { get; set; }
		public Action<TResult> HandleSuccess { get; set; }
		public void OnCancel()
		{
			var c = HandleCancel;
			if (c != null) c();
		}
		public void OnError(FacebookException error)
		{
			var c = HandleError;
			if (c != null) c(error);
		}
		public void OnSuccess(Java.Lang.Object result)
		{
			var c = HandleSuccess;
			if (c != null) c(result.JavaCast<TResult>());
		}
	}
}
