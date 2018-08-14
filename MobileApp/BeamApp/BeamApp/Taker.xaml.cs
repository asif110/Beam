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
	public partial class Taker : ContentPage
	{
		public Taker ()
		{
			InitializeComponent ();
		}

        private void OnNext(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Itinerary(document.IsToggled,package.IsToggled,carpool.IsToggled));
            Navigation.RemovePage(this);
        }
    }
}