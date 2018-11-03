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
	public partial class Request : ContentPage
	{
        public enum RequestType
        {
            REQUEST_TYPE_INVALID = 0,
            REQUEST_TYPE_DOCUMENT = 1,
            REQUEST_TYPE_PACKAGE,
            REQUEST_TYPE_ACCOMPANY,
            REQUEST_CARPOOL
        }

        List<CityController.CountryName> m_countryList;
        CityController m_cityController = new CityController();
        int m_iCityFrom = 0;
        int m_iCityTo = 0;
        RequestType m_requestType = RequestType.REQUEST_TYPE_INVALID;
       

        public Request (RequestType requestType)
		{
			InitializeComponent ();
            m_countryList = CityController.GetCountries().ToList();
            countryFrom.ItemsSource = m_countryList.ToList();
            countryTo.ItemsSource = m_countryList.ToList();
            Title = "Request";

            switch (requestType)
            {
                case RequestType.REQUEST_TYPE_DOCUMENT:
                    ShowDocument();
                    break;
                case RequestType.REQUEST_TYPE_PACKAGE:
                    ShowPackage();
                    break;
                case RequestType.REQUEST_TYPE_ACCOMPANY:
                    ShowAccompany();
                    break;
                case RequestType.REQUEST_CARPOOL:
                    ShowCarpool();
                    break;
            }
            m_requestType = requestType;
        }

        private void ShowDocument()
        {
            flightInformationAccompany.IsVisible = false;
            package.IsVisible = false;
        }
        private void ShowPackage()
        {
            flightInformationAccompany.IsVisible = false;
            photo.IsVisible = false;
        }

        private void ShowAccompany()
        {
            package.IsVisible = false;
            goViral.IsVisible = false;
            urgentPanel.IsVisible = false;
            flexibility.IsVisible = false;
            sendByText.Text = "Departure Date";
            photo.IsVisible = false;
        }

        private void ShowCarpool()
        {
            flightInformationAccompany.IsVisible = false;
            package.IsVisible = false;
            urgentPanel.IsVisible = false;
            sendBy.IsVisible = false;
            flightInformationAccompany.IsVisible = false;
            flexibility.IsVisible = false;
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

        async void OnCreateRequest(object sender, EventArgs e)
        {
            RequestController requestController = new RequestController();
            int options = 0;
            if (firstDegree.IsToggled) options = 1;
            if (secondDegree.IsToggled) options = 2;
            if (viral.IsToggled) options = 3;

            int flexibilityDays = 0;
            int.TryParse(flexibility.Text, out flexibilityDays);

            bool result = await requestController.CreateRequest(m_requestType, m_iCityFrom, m_iCityTo, sendByDate.Date, urgent.IsToggled,
                                                                flexibilityDays, subject.Text, itemDescription.Text, "TBI", options,
                                                                facebook.IsToggled);

            if (result)
            {
                await DisplayAlert("Be a sender", "Your request is created ", "OK");
                await Navigation.PopAsync();

            }
            else
            {
                await DisplayAlert("Be a sender", "Error occured, please try again later", "OK");
            }

        }
    }
}