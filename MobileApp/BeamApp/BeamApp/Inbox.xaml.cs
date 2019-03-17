using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace BeamApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Inbox : ContentPage
    {
        string Upk = "";
        public Inbox(string UserPK)
        {
            InitializeComponent();
            // RequestContent();
            GetRequest(UserPK);
            Upk = UserPK;
        }

        private async void GetRequest(string UserPK)
        {
            
            HttpClient client = new HttpClient();
            List<ReuestProperty> reqList = new List<ReuestProperty>();
            //var response = await client.GetStringAsync("http://beam.gear.host/api/Request?id=0&resUserFK=" + UserPK.ToString());
                       var response = await client.GetStringAsync("http://beam.gear.host/api/Request?id=0&resUserFK=" + UserPK.ToString()+ "&reqType=received");

            var request = JsonConvert.DeserializeObject<List<ReuestProperty>>(response);
            reqList = request;
            MainListView.ItemsSource = request;
            //lblName.Text = reqList[0].UserName;


            //RequestController requestController = new RequestController();
            //   var fnRequest = await requestController.Request();

            //MainListView.ItemsSource = request;

        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selection = e.SelectedItem as ReuestProperty;

                await Navigation.PushAsync(new InboxDetails(selection.PK.ToString(), Upk, "received"));
                //await Navigation.PushAsync(new RequestDocument());


            }
        }

    }
    public class ReuestProperty
    {

        ////to get data
        public int UserFK { get; set; }

        //data retrieved
        public int PK { get; set; }
        public int RequestTypeFK { get; set; }
        public int FromCityFK { get; set; }
        public int ToCityFK { get; set; }
        public DateTime DateTimeUtc { get; set; }
        public Boolean IsUrgent { get; set; }
        public int FlexibilityDays { get; set; }
        public string Subject { get; set; }
        public string ItemDescription { get; set; }
        public string Image { get; set; }
        public int Options { get; set; }
        public Boolean ShareOnFacebook { get; set; }
        public int AccompanyInfoFK { get; set; }
        public int PackageInfoFK { get; set; }
        public Boolean IsForwardingAllowed { get; set; }
        public int Status { get; set; }
        public int WillingToPay { get; set; }
        public string StatusDescription { get; set; }
        public string ReqDescritption { get; set; }
        public string ReqSubject { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Boolean IsStatusChange { get; set; }
        public string FromCitystr { get; set; }
        public string ToCitystr { get; set; }

    }
}