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
        List<CityController.CountryName> m_countryList;
        CityController m_cityController = new CityController();
        int m_iCityFrom = 0;
        int m_iCityTo = 0;

        public Request ()
		{
			InitializeComponent ();
            m_countryList = CityController.GetCountries().ToList();
            countryFrom.ItemsSource = m_countryList.ToList();
            countryTo.ItemsSource = m_countryList.ToList();

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
    }
}