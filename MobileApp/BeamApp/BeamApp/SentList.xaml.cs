using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;

namespace BeamApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SentList : ContentPage
    {
        string RequestId = "";
        string UId = "";

        public SentList(string ReqId, string UserId)
        {
            InitializeComponent();
            GetRequest(ReqId);
            RequestId = ReqId;
            UId = UserId;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selection = e.SelectedItem as ReuestReceiverProperty;

                await Navigation.PushAsync(new InboxDetails(selection.PK.ToString(), UId, "sent", selection.ReceivedUserFK));
                //await Navigation.PushAsync(new RequestDocument());


            }
        }

        private async void GetRequest(string RequestId)
        {
            

            HttpClient client = new HttpClient();
            List<ReuestReceiverProperty> reqList = new List<ReuestReceiverProperty>();
            //var response = await client.GetStringAsync("http://beam.gear.host/api/Request?id=0&resUserFK=" + UserPK.ToString());
            var response = await client.GetStringAsync("http://beam.gear.host/api/Request?id=" + RequestId + "&resUserFK=0&reqType=''");

            var request = JsonConvert.DeserializeObject<List<ReuestReceiverProperty>>(response);
            reqList = request;
            
            MN.ItemsSource = request;

        }



    }
    public class ReuestReceiverProperty
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
        public Boolean IsStatusChange { get; set; }
        public string FromCitystr { get; set; }
        public string ToCitystr { get; set; }
        public string ReceivedUserFK { get; set; }
        public string reqCreatedUserFK { get; set; }


    }
}