﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeamApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            this.loginPanel.IsEnabled = false;
            this.spinner.IsRunning = true;
            string email = this.email.Text;
            string password = this.password.Text;
           
            LoginController loginController = new LoginController();
            bool result = await loginController.Login(email,password);
            if (!result)
            {
                await DisplayAlert("Login failed", "Incorrect username or password", "OK");
                this.loginPanel.IsEnabled = true;
                this.spinner.IsRunning = false;
                //test
            }
            else
            {
                string UserPK = App.UserPK.ToString();
                await Navigation.PushAsync(new PreMainMenu(UserPK));
                Navigation.RemovePage(this);
            }
        }

        async void OnRegister(object sender, EventArgs e)
        {
            //App.Current.MainPage = new Register();
            await Navigation.PushAsync(new Register());
        }
    }
}
