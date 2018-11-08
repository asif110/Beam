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
    public partial class InboxDetails : ContentPage
    {
        //string ReqPK;
        [JsonProperty]
        public string ReqPK { get; set; }

        ReuestProperty rp;

        string UPk;
        string strReqType;

        public InboxDetails(string PK, string UserPK,string reqType)
        {
            rp = new ReuestProperty();
            rp.PK = Convert.ToInt32(PK);

            ReqPK = PK;
            strReqType = reqType;
            InitializeComponent();
            GetRequestDocument(PK);
            UPk = UserPK;
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    BackButtonPressed();
        //    return true;
        //}

        public async Task BackButtonPressed()
        {
            var action = await DisplayAlert("Notification", "Are you sure to accept?", "No", "Yes");
            if (action)
            {
                //  Navigate to first page
            }
        }

        private async void GetRequestDocument(string PK)
        {
            HttpClient client = new HttpClient();

            List<ReuestProperty> requestPropertyient = new List<ReuestProperty>();

            int id = Convert.ToInt32(PK);

            string strURL = "http://beam.gear.host/api/Request?id=" + id;
            var response = await client.GetStringAsync(strURL);
            // var response = await client.GetStringAsync("http://beam.gear.host/api/Request?id=1");

            //var request = JsonConvert.DeserializeObject<ReuestProperty>(response);
            var request = JsonConvert.DeserializeObject<List<ReuestProperty>>(response);
            requestPropertyient = (List<ReuestProperty>)request;

            lblItemDescription.Text = "Description:" + requestPropertyient[0].ReqDescritption + " " + requestPropertyient[0].ItemDescription;

            DateTimeUtc.Text = "Send by: "+ requestPropertyient[0].DateTimeUtc.ToString();
            FlexibilityDays.Text = "Flexibility:" + " " + requestPropertyient[0].FlexibilityDays.ToString();
            lblName.Text = requestPropertyient[0].UserName.ToString();

            lblFromCity.Text = "Document Origin: "+ requestPropertyient[0].FromCitystr.ToString();

            lblToCity.Text = "Document destination: " + requestPropertyient[0].ToCitystr.ToString();
            // ReqSubject.Text = requestPropertyient[0].ReqSubject.ToString();
            if(requestPropertyient[0].IsUrgent)
            {
                lblSituation.Text = "Situation: " + "Urgent";
            }
            else
            {
                lblSituation.Text = "Situation: " + "Not Urgent";
            }
           

            //MainListView.ItemsSource = request;


        }

        async void OnAccept(object sender, EventArgs e)
        {
           
                
            rp.IsStatusChange = true;

            
            rp.Status = 1;

            var action = await DisplayAlert("Notification", "Are you sure to accept?", "No", "Yes");
            if (action)
            {
                //  Navigate to first page
                UpdateStatus(rp);
                if (strReqType == "received")
                {
                    await Navigation.PushAsync(new Inbox(UPk));
                }
                else
                {
                    await Navigation.PushAsync(new SendBox(UPk));
                }
                
            }
           

        }

        async void OnDecline(object sender, EventArgs e)
        {

           
            rp.IsStatusChange = true;
            rp.Status = 2;

            var action = await DisplayAlert("Notification", "Are you sure to Decline?", "No", "Yes");
            if (action)
            {

                UpdateStatus(rp);
                if (strReqType == "received")
                {
                    await Navigation.PushAsync(new Inbox(UPk));
                }
                else
                {
                    await Navigation.PushAsync(new SendBox(UPk));
                }
                //await Navigation.PushAsync(new Inbox(UPk));

            }
        }

        private async void UpdateStatus(ReuestProperty rp)
        {
            HttpClient httpClient = new HttpClient();

            Uri uri = new Uri("http://beam.gear.host/api/Request");

            string json = JsonConvert.SerializeObject(rp, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                //if (res == "0")
                //{
                //    return false;
                //}
                //else //success
                //{
                //    App.UserEmail = email;
                //    App.UserPK = Convert.ToInt32(res);
                //}
            } 
            //// need to change //
          

            


        }

//        {
//            try
//            {
//                User user = new User()
//                {
//                    Email = email,
//                    Password = password
//                };
//        string json = JsonConvert.SerializeObject(user, Formatting.Indented);
//        var content = new StringContent(json, Encoding.UTF8, "application/json");
//        var response = await httpClient.PostAsync(uri, content);
//                if (response.IsSuccessStatusCode)
//                {
//                    string res = await response.Content.ReadAsStringAsync();
//                    if (res == "0")
//                    {
//                        return false;
//                    }
//                    else //success
//                    {
//                        App.UserEmail = email;
//                        App.UserPK = Convert.ToInt32(res);
//                    }
//}
//            }
//            catch(Exception ex)
//            {
//                return false;
//            }
//            return true;
//        }

    }
}