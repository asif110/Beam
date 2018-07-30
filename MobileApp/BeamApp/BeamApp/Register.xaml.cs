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
        RegisterationController registrationController = new RegisterationController();
        List<RegisterationController.CountryName> countryList;
        int cityFK;
        int cityTravelTo1FK;
        int cityTravelTo2FK;
        public Register()
        {
            InitializeComponent();
            
            countryList = RegisterationController.GetCountries().ToList();
            //countryList.Sort();

            //this.country1.ItemsSource = countries;
            this.country1.ItemsSource = countryList.ToList();
            this.countryTravel1.ItemsSource = countryList.ToList();
            this.countryTravel2.ItemsSource = countryList.ToList();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        async void OnRegister(object sender, EventArgs e)
        {
            bool result = await registrationController.RegisterUser(fName.Text, lName.Text, email.Text, password.Text, phone.Text, cityFK, cityTravelTo1FK, cityTravelTo2FK);
            if (result)
            {
                await DisplayAlert("User Registration","User registerd successfully", "OK");
                await Navigation.PopAsync();
                
            }
            else
            {
                await DisplayAlert("User Registration", "User did not register successfully", "OK");
            }
        }

        private void EnableForm(bool isEnabled)
        {
            this.registrationPanel.IsEnabled = isEnabled;
            if (isEnabled)
            {
                Navigation.PopAsync();
            }
            else
            {
                Navigation.PushModalAsync(new Spinner());
            }
        }

        async void OnCountrySelected(object sender, EventArgs e)
        {
            //EnableForm(false);
            string countryCode =  Convert.ToString(countryList[country1.SelectedIndex].CountryId);
            city1.ItemsSource  = await registrationController.GetCities(countryCode);
            //EnableForm(true);
        }

        async void OnCountryTravel1Selected(object sender, EventArgs e)
        {
            string countryCode = Convert.ToString(countryList[countryTravel1.SelectedIndex].CountryId);
            cityTravel1.ItemsSource = await registrationController.GetCities(countryCode);
        }
        async void OnCountryTravel2Selected(object sender, EventArgs e)
        {
            string countryCode = Convert.ToString(countryList[countryTravel2.SelectedIndex].CountryId);
            cityTravel2.ItemsSource = await registrationController.GetCities(countryCode);
        }

        void OnCitySelected(object sender, EventArgs e)
        {
            RegisterationController.City city = (RegisterationController.City)city1.SelectedItem;
            if (city != null)
            { 
                cityFK = city.PK;
            }

        }
        void OnCityTravelTo1Selected(object sender, EventArgs e)
        {
            RegisterationController.City city = (RegisterationController.City)cityTravel1.SelectedItem;
            if (city != null)
            {
                cityTravelTo1FK = city.PK;
            }

        }
        void OnCityTravelTo2Selected(object sender, EventArgs e)
        {
            RegisterationController.City city = (RegisterationController.City)cityTravel2.SelectedItem;
            if (city != null)
            {
                cityTravelTo2FK = city.PK;
            }

        }
    }
}
