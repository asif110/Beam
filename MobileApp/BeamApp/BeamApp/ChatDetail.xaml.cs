using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeamApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatDetail : ContentPage
    {
        ChatProperty chatProperty;
        StringBuilder strDB = new StringBuilder();
        public ChatDetail(string RequestId, string FromId, string ToId)
        {
            InitializeComponent();

            chatProperty = new ChatProperty();
            
            chatProperty.RequestId = Convert.ToInt32(RequestId);
            chatProperty.FromId = Convert.ToInt32(FromId);
            chatProperty.ToId = Convert.ToInt32(ToId);
            refreshChatUi();
            int intervalInSeconds = 5;

            Device.StartTimer(TimeSpan.FromSeconds(intervalInSeconds), () =>
            { 
                
                Device.BeginInvokeOnMainThread(() => refreshChatUi());
                return true;
            });

            //GetChatDetails(RequestId,FromId,ToId);

        }
        

        private  void refreshChatUi()
        {
             GetChatDetails(chatProperty.RequestId.ToString(), chatProperty.FromId.ToString(), chatProperty.ToId.ToString());
        }
        
        private async void GetChatDetails(string RequestId, string FromId, string ToId)
        {
            HttpClient client = new HttpClient();

            List<ChatProperty> cht = new List<ChatProperty>();

            int ReqId = Convert.ToInt32(RequestId);
            int FId = Convert.ToInt32(FromId);
            int TId = Convert.ToInt32(ToId);

            string strURL = "http://beam.gear.host/api/Chat?id=" + ReqId  + "&FromId=" + FId + "&ToId=" + TId;
            var response = await client.GetStringAsync(strURL);
          
            var chat = JsonConvert.DeserializeObject<List<ChatProperty>>(response);
            cht = (List<ChatProperty>)chat;

            //var browser = new WebView();
            //browser.VerticalOptions = LayoutOptions.FillAndExpand;
            //browser.WidthRequest = 1000;
            //browser.HeightRequest = 1000;
            
            wb.VerticalOptions = LayoutOptions.FillAndExpand;
            wb.HorizontalOptions = LayoutOptions.FillAndExpand;

            wb.WidthRequest = 500;
            wb.HeightRequest = 500;

            var htmlSource = new HtmlWebViewSource();
            StringBuilder htmlStr = new StringBuilder("");
            htmlStr.Append("<html><body>");
            htmlStr.Append("<table width='100%' Border='0px'>");

          

            foreach (ChatProperty chtpr  in cht)
            {
                if (chtpr.FromId != chatProperty.FromId)
                {
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td align='left'>");
                    htmlStr.Append("<font color='blue'>");

                    htmlStr.Append(chtpr.ChatMessage.ToString());
                    htmlStr.Append("</font>");
                    htmlStr.Append("</td></tr>");

                }
                else
                {
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td align='right'>");
                    htmlStr.Append("<font color='red'>");

                    htmlStr.Append(chtpr.ChatMessage.ToString());
                    htmlStr.Append("</font>");
                    htmlStr.Append("</td></tr>");
                }

            }

            string result = htmlStr.ToString();

            strDB.Append(result);
            htmlStr.Append("</table></body></html>");
            htmlSource.Html = htmlStr.ToString();
           // browser.Source = htmlSource; 
            wb.Source = htmlSource;
            //stklyt.Children.Add(browser);
            

            //Content = browser;
            // cnt.Content = browser;
            //cnt.Content = wb;
            //stklyt.Children.Add(browser);
            cnt.Content =  stklyt;

        }

        public void RefillChat(StringBuilder str)
        {
            wb.VerticalOptions = LayoutOptions.FillAndExpand;
            wb.HorizontalOptions = LayoutOptions.FillAndExpand;

            wb.WidthRequest = 500;
            wb.HeightRequest = 500;

            var htmlSource = new HtmlWebViewSource();
            StringBuilder htmlStr = new StringBuilder("");
            string result = strDB.ToString();
            string strParam = str.ToString();
            string strNew = "";

            htmlStr.Append(result);
            htmlStr.Append(strParam);

            strNew = htmlStr.ToString();

            strDB.Clear();

            strDB.Append(strNew);

            htmlStr.Append("</table></body></html>");
            htmlSource.Html = htmlStr.ToString();
            wb.Source = htmlSource;
          
            cnt.Content = stklyt;
        }
        private async void OnSendChat(object sender, EventArgs e)
        {
            
          
            //chatProperty.ChatImage = "";
            chatProperty.ChatMessage = chat.Text.ToString();
           // chatProperty.FromId = "1";
            //chatProperty.ToId = "2";
            bool result = await UpdateChat(chatProperty);

            StringBuilder updateMessage = new StringBuilder(""); ;

            updateMessage.Append("<tr>");
            updateMessage.Append("<td align='right'>");
            updateMessage.Append("<font color='red'>");

            updateMessage.Append(chat.Text.ToString());
            updateMessage.Append("</font>");
            updateMessage.Append("</td></tr>");

            //RefillChat(updateMessage);

            chat.Text = "";

        }

        public async Task<bool> UpdateChat(ChatProperty chp)
        {
            Boolean chatstatus = false;
            HttpClient httpClient = new HttpClient();

            Uri uri = new Uri("http://beam.gear.host/api/Chat");

            string json = JsonConvert.SerializeObject(chp, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();

                if (res == "0")
                {
                    chatstatus = false;
                }
                else //success
                {
                    chatstatus= true;
                }
            }

            return chatstatus;
        }
        public class ChatProperty
        {

            ////to get data
            public int UserFK { get; set; }

            //data retrieved
            public int PK { get; set; }
            public string ChatMessage { get; set; }
            public int FromId { get; set; }
            public int ToId { get; set; }
            public int RequestId { get; set; }
           // public string ChatImage { get; set; }
            public Boolean IsRead { get; set; }
            public DateTime SentDate { get; set; }
            public DateTime ReceivedDate { get; set; }


        }
    }
}