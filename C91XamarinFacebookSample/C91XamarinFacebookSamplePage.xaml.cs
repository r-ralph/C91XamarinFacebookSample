using Xamarin.Forms;

namespace C91XamarinFacebookSample
{
	public partial class C91XamarinFacebookSamplePage : ContentPage
	{
		public C91XamarinFacebookSamplePage()
		{
			InitializeComponent();
			loginBtn.Clicked += (sender, e) =>
			{
				var service = DependencyService.Get<IFacebookService>();
				service.LoginFinished += (result, token) =>
				{
					// do someting
				};

				service.Login();
			};
		}
	}
}
