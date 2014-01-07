using RestBus.RabbitMQ.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClientExample
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonSend_Click(object sender, EventArgs e)
        {

            RequestOptions requestOptions = null;
 
            //Request response in JSON format            
            requestOptions = new RequestOptions();
            requestOptions.Headers.Add("Accept", "application/json");

            //Send Request
            var response = Global.HelloServiceClient.GetAsync("/hello/" + TextBoxName.Text, requestOptions).Result;

            //Get Response object
            var resObj = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType( response.Content.ReadAsStringAsync().Result, new { Result = "" });
            
            //Display result
            PanelResponse.Visible = true;
            LabelResponse.Text = resObj.Result;

        }
    }
}