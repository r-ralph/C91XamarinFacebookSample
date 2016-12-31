using System;
namespace C91XamarinFacebookSample
{
	public enum Result { Success, Cancel, Error, Unknown };
	public interface IFacebookService
	{
		void Login();
		event Action<Result, string> LoginFinished;
	}
}
