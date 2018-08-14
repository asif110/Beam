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
	public partial class Itinerary : ContentPage
	{
        List<CityController.CountryName> m_countryList;
        CityController m_cityController = new CityController();
        ItineraryController m_itineraryController = new ItineraryController();
        bool m_bIsDocument = false;
        bool m_bIsPackage = false;
        bool m_bIsCarpool = false;
        int m_iCityFrom = 0;
        int m_iCityTo = 0;

        public Itinerary (bool isDocument, bool isPackage, bool isCarpool)
		{
			InitializeComponent ();
            m_countryList = CityController.GetCountries().ToList();
            countryFrom.ItemsSource = m_countryList.ToList();
            countryTo.ItemsSource = m_countryList.ToList();

            m_bIsDocument = isDocument;
            m_bIsPackage = isPackage;
            m_bIsCarpool = isCarpool;
        }

        async void OnCountryFromSelected(object sender, EventArgs e)
        {
            string countryCode = Convert.ToString(m_countryList[countryFrom.SelectedIndex].CountryId);
            cityFrom.ItemsSource = await m_cityController.GetCities(countryCode);
        }

        async void OnCityFromSelected(object sender, EventArgs e)
        {
            CityController.City city = (CityController.City)cityFrom.SelectedItem;
            if (city != null)
            {
                m_iCityFrom = city.PK;
            }

        }
        async void OnCountryToSelected(object sender, EventArgs e)
        {
            string countryCode = Convert.ToString(m_countryList[countryTo.SelectedIndex].CountryId);
            cityTo.ItemsSource = await m_cityController.GetCities(countryCode);
        }
        async void OnCityToSelected(object sender, EventArgs e)
        {
            CityController.City city = (CityController.City)cityTo.SelectedItem;
            if (city != null)
            {
                m_iCityTo = city.PK;
            }
        }

        async void OnCreateItinerary(object sender, EventArgs e)
        {
            DateTime departureDateTime = departureDate.Date + departureTime.Time;
            DateTime returnDateTime = returnDate.Date + returnTime.Time;

            bool result = await m_itineraryController.CreateItinerary(App.UserEmail, m_iCityFrom, m_iCityTo, departureDateTime, 
                returnDateTime, m_bIsDocument, m_bIsPackage, m_bIsCarpool, modeOfTravel.SelectedIndex,details.Text);

            if (result)
            {
                await DisplayAlert("Be a taker", "Your itinerary is updated ", "OK");
                await Navigation.PopAsync();

            }
            else
            {
                await DisplayAlert("Be a taker", "Error occured, please try again later", "OK");
            }
        }
    }
}