using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace BeamApp
{
	public partial class App : Application
	{
        public static string UserEmail = ""; //not a good idea, will need to find alternatives
        public static int UserPK = 0;
		public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage( new MainPage());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
