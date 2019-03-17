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

        public ChatDetail(string RequestId)
        {
            InitializeComponent();

            chatProperty = new ChatProperty();

            chatProperty.RequestId = Convert.ToInt32(RequestId);

            GetChatDetails(RequestId);
        }

        private async void GetChatDetails(string RequestId)
        {
            HttpClient client = new HttpClient();

            List<ChatProperty> chatProperty = new List<ChatProperty>();

            int ReqId = Convert.ToInt32(RequestId);

            string strURL = "http://beam.gear.host/api/Chat?id=" + ReqId;
            var response = await client.GetStringAsync(strURL);
          
            var chat = JsonConvert.DeserializeObject<List<ChatProperty>>(response);
            chatProperty = (List<ChatProperty>)chat;

            //var browser = new WebView();
            //var htmlSource = new HtmlWebViewSource();
            //StringBuilder htmlStr = new StringBuilder("");
            //htmlStr.Append("<html><body><table>");
            //htmlStr.Append("<table width='100%' height='20%' Border='1px solid black'>");

            //htmlStr.Append("<tr>");
            //htmlStr.Append("<th> FirstName </th>");
            //htmlStr.Append("<th>LastName </th></tr>");

            //htmlStr.Append("<tr>");
            //htmlStr.Append("<td>Disha </td>");
            //htmlStr.Append("<td>Raval</td></tr>");

            //htmlStr.Append("<tr>");
            //htmlStr.Append("<td>Namrata</td>");
            //htmlStr.Append("<td>Rathod</td></tr>");

            //htmlStr.Append("<tr>");
            //htmlStr.Append("<td>Monika</td>");
            //htmlStr.Append("<td>Vaghasiya</td></tr>");

            //htmlStr.Append("</table></body></html>");
            //htmlSource.Html = htmlStr.ToString();
            //browser.Source = htmlSource;
            //Content = browser;
            

        }

        private async void OnSendChat(object sender, EventArgs e)
        {
            
          
            //chatProperty.ChatImage = "";
            chatProperty.ChatMessage = chat.Text.ToString();
            chatProperty.FromId = "1";
            chatProperty.ToId = "2";
            bool result = await UpdateChat(chatProperty);


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
            public string FromId { get; set; }
            public string ToId { get; set; }
            public int RequestId { get; set; }
           // public string ChatImage { get; set; }
            public Boolean IsRead { get; set; }
            public DateTime SentDate { get; set; }
            public DateTime ReceivedDate { get; set; }


        }
    }
}