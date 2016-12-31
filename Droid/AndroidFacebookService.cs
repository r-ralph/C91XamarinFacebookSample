using System;
using Android.Content;
using C91XamarinFacebookSample.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidFacebookService))]
namespace C91XamarinFacebookSample.Droid
{
	public class AndroidFacebookService : IFacebookService
	{
		public event Action<Result, string> LoginFinished;

		public void Login()
		{
			var context = Forms.Context;
			if (!(context is MainActivity)) return;
			var activity = context as MainActivity;
			// Register BroadcastReceiver
			var receiver = new FacebookLoginReceiver();
			receiver.LoginFinished += (result, userId) =>
			{
				context.UnregisterReceiver(receiver);
				if (LoginFinished != null)
				{
					LoginFinished(result, userId);
				}
			};
			context.RegisterReceiver(receiver, new IntentFilter(FacebookLoginReceiver.
			ActionKey));
			// Start
			activity.client.Login();
		}
	}
}
