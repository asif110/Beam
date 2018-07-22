using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections;

namespace BeamApp
{
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
            this.country1.SelectedIndex = 0;
            this.city1.SelectedIndex = 0;

            RegisterationController registrationController = new RegisterationController();
            ArrayList countries =  registrationController.GetAllCountires();
            this.country1.ItemsSource = countries;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        async void OnRegister(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Register());
        }
    }
}
