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
	public partial class PreMainMenu : ContentPage
	{
		public PreMainMenu ()
		{
			InitializeComponent ();
            
		}

        private void OnTaker(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Taker());
        }

        private void OnSender(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainMenu());
        }
    }
}