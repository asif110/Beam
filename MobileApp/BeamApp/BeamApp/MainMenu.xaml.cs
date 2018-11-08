using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeamApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainMenu : ContentPage
	{
		public MainMenu ()
		{
			InitializeComponent ();
		}

        private void OnDocument(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Request(Request.RequestType.REQUEST_TYPE_DOCUMENT));
        }

        private void OnPackage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Request(Request.RequestType.REQUEST_TYPE_PACKAGE));
        }
        private void OnAccompany(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Request(Request.RequestType.REQUEST_TYPE_ACCOMPANY));
        }

        private void OnCarpool(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Request(Request.RequestType.REQUEST_CARPOOL));
        }
    }
}