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
        public string Uri = "/api/values/"; //Substitute "/hello/" for the ServiceStack example

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
            var response = Global.HelloServiceClient.GetAsync(Uri + TextBoxName.Text, requestOptions).Result;

            //Display result
            PanelResponse.Visible = true;
            LabelResponse.Text = response.Content.ReadAsStringAsync().Result;

        }
    }
}