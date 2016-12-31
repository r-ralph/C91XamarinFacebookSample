using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace C91XamarinFacebookSample.Droid
{
	[Activity(Label = "C91XamarinFacebookSample.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		internal FacebookClient client;

		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);
			client = new FacebookClient(this);
			client.Init();

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
		}

		protected override void OnActivityResult(int requestCode, Android.App.Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			client.OnActivityResult(requestCode, (int)resultCode, data);
		}
	}
}
