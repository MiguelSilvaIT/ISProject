using RestSharp;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Net;
using System.Collections.Generic;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

namespace App_A
{
    public partial class Form1 : Form
    {

        string appName = "";
        string containerName = "";
        
        string connectionString =
        System.Configuration.ConfigurationManager.ConnectionStrings["App_A.Properties.Settings.ConnString"].ConnectionString;

        MqttClient mClient = null;
        List<string> mStrApp = new List<string>();
        List<string> mStrContainer = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //check if TVApp already exists in the DB

            //SqlConnection sqlConnection = null;

            //string queryString = "SELECT * FROM application WHERE name = 'TVApp'";

            //try
            //{
            //    sqlConnection = new SqlConnection(connectionString);
            //    SqlCommand command = new SqlCommand(queryString, sqlConnection);
            //    sqlConnection.Open();
            //    SqlDataReader reader = command.ExecuteReader();

            //    if (!reader.HasRows)
            //    {
            //        ////Create TV application
            //        //var client = new RestClient(@"http://localhost:59352/");
            //        //var request = new RestRequest(@"api/somiod/", Method.Post);
            //        //request.RequestFormat = DataFormat.Xml;
            //        //request.AddBody(new ISProject.Models.Application
            //        //{
            //        //    name = "TVApp",
            //        //});

            //        //var response = client.Execute(request);
            //        //MessageBox.Show("Application Status Code: " + response.StatusCode + "\n" +
            //        //                "Content: " + response.Content);

            //        ////Create TV container
            //        //request = new RestRequest(@"api/somiod/TVApp", Method.Post);
            //        //request.RequestFormat = DataFormat.Xml;
            //        //request.AddHeader("somiod-discover", "container");
            //        //request.AddBody(new ISProject.Models.Container
            //        //{
            //        //    name = "TVApp Container"
            //        });
            //        //see request content
            //        var body = request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);

            //        Console.WriteLine("CurrentBody={0}", body.Value);




            //        response = client.Execute(request);
            //        MessageBox.Show("Container Status Code: " + response.StatusCode + "\n" +
            //                        "Content: " + response.Content);

                 
            //    }
               
            //    reader.Close();
            //}
            //catch (Exception ex)
            //{
           
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    sqlConnection.Close();
            //}

    
        }


        private void ConnectBTN_Click(object sender, EventArgs e)
        {
            mClient = new MqttClient(IPAddress.Parse(IPAddresstxt.Text));
            mClient.Connect(Guid.NewGuid().ToString());
            if (!mClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }
            MessageBox.Show("Connected");
        }

        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            MessageBox.Show("Message Received");

            // Use BeginInvoke to execute the UI update on the UI thread
            BeginInvoke(new Action(() =>
            {
                string conteudo = Encoding.UTF8.GetString(e.Message);
                char entrada = conteudo[0];
                conteudo = conteudo.Substring(1);

                switch (entrada)
                {
                    case 'E':
                        estadoTXT.Text = conteudo;
                        break;

                    case 'V':
                        volumeTXT.Text = conteudo;
                        break;

                    case 'C':
                        canalTXT.Text = conteudo;
                        break;

                    default:
                        MessageBox.Show(Encoding.UTF8.GetString(e.Message) + "on topic" + e.Topic);
                        break;
                }
            }));
        }


        private void SubBTN_Click(object sender, EventArgs e)
        {
            
            //Check if the mClient is connected
            if (mClient== null)
            {
                MessageBox.Show("Please connect to the message broker before subscribing.");
                return;
            }
            
            // Check if any item is selected in the ComboBox
            if (TopicDrpDown.SelectedItem != null)
            {
                var client = new RestClient(@"http://localhost:59352/");

                //Create  subscription
                var request = new RestRequest(@"api/somiod/TVApp/TVApp Container", Method.Post);
                request.RequestFormat = DataFormat.Xml;
                request.AddHeader("somiod-discover", "subscription");
                request.AddBody(new ISProject.Models.Subscription
                {
                    name = "TVApp Subscription",
                    endpoint = "mqtt://192.168.1.1:1883",
                    event_type = "1",
                    res_type = "subscription"
                });
                var response = client.Execute(request);
                
                if(response.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show("Subscription Status Code: " + response.StatusCode + "\n" +
                                                                      "Content: " + response.Content);
                    return;
                }

                

                mClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

                byte[] qosLevel = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };

                // Create an array of topics
                string[] topics = { TopicDrpDown.SelectedItem.ToString() };

                // Subscribe to the topic with the specified QoS levels
                mClient.Subscribe(topics, qosLevel);
                MessageBox.Show("Subscribed to: " + topics[0]);
            }
            else
            {
                // Handle the case when no item is selected
                MessageBox.Show("Please select a topic before subscribing.");
            }
        }

        private void UnsubBTN_Click(object sender, EventArgs e)
        {
            if (mClient != null)
            {
                string[] topic = { TopicDrpDown.SelectedItem.ToString() };
                mClient.Unsubscribe(topic); //Put this in a button to see notif!
                //delete from database
                var client = new RestClient(@"http://localhost:59352/");
                var request = new RestRequest(@"api/somiod/TVApp/TVApp Container/TVApp Subscription", Method.Delete);
                request.RequestFormat = DataFormat.Xml;
                request.AddHeader("somiod-discover", "subscription");
                var response = client.Execute(request);
                MessageBox.Show("Subscription Status Code: " + response.StatusCode + "\n" +
                                                                  "Content: " + response.Content);
                MessageBox.Show("Topic Unsubscribed");
                return;
            }
            MessageBox.Show("There is no connection created");
        }

        private void getTopicsBTN_Click(object sender, EventArgs e)
        {
            if (appDrpDown.SelectedItem == null)
            {
                MessageBox.Show("Please select an application");
                return;
            }


            List<string> containerNames = new List<string>();

            var client = new RestClient(@"http://localhost:59352/");

            var request = new RestRequest(@"api/somiod/{application}", Method.Get);
            request.RequestFormat = DataFormat.Xml;
            request.AddUrlSegment("application", appDrpDown.SelectedItem.ToString());
            request.AddHeader("somiod-discover", "container");
            var response = client.Execute(request);

            string xml = response.Content;

            XmlDocument doc = new XmlDocument();

            //remove first and last parameter from app.Content
            //remove first character
            doc.LoadXml(xml);

            XmlNodeList nameNodes = doc.SelectNodes("//Container/name/text()");

            foreach (XmlNode nameNode in nameNodes)
            {
                string nameValue = nameNode.Value;
                //add nameValue to appNames
                containerNames.Add(nameValue);
            }

            TopicDrpDown.DataSource = containerNames;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //delete all subscriptions from database
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string deleteSubs = "DELETE FROM subscription";

                SqlCommand command = new SqlCommand(deleteSubs, sqlConnection);
                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }

        }

        private void createAppAndContainers_Click(object sender, EventArgs e)
        {
            //Create TV application
            var client = new RestClient(@"http://localhost:59352/");
            var request = new RestRequest(@"api/somiod/", Method.Post);
            request.RequestFormat = DataFormat.Xml;
            request.AddBody(new ISProject.Models.Application
            {
                name = "TVApp",
            });

            var response = client.Execute(request);
            if(response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("Application TVApp created");

            }
            else 
            {
                MessageBox.Show("Application Status Code: " + response.StatusCode + "\n" +
                                                 "Content: " + response.Content);
                return;
            }
                
            
            //Create TV container
            request = new RestRequest(@"api/somiod/TVApp", Method.Post);
            request.RequestFormat = DataFormat.Xml;
            request.AddHeader("somiod-discover", "container");
            request.AddBody(new ISProject.Models.Container
            {
                name = "TVApp Container"
            });
            response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("Container TVApp Container created");

            }
            else
            {
                MessageBox.Show("Container Status Code: " + response.StatusCode + "\n" +
                                                                    "Content: " + response.Content);
                return;
            }
        }


        private void btn_delete_app_cont_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new RestClient(@"http://localhost:59352/");


                // Delete TV container
                var request = new RestRequest(@"api/somiod/TVApp/TVApp Container", Method.Delete);
                var response = client.Execute(request);

                // Check if the deletion was successful (status code 200 OK)
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("Container TVApp Container deleted successfully.");
                }
                else
                {
                    // Check if the deletion failed due to dependencies (status code 409 Conflict)
                    if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        MessageBox.Show("Cannot delete container. It has dependent resources.");
                    }
                    else
                    {
                        MessageBox.Show("Container deletion failed. Status Code: " + response.StatusCode + "\n" +
                                        "Content: " + response.Content);
                    }
                }

                // Delete TV application
                 request = new RestRequest(@"api/somiod/TVApp", Method.Delete);
                 response = client.Execute(request);

                // Check if the deletion was successful (status code 200 OK)
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("Application TVApp deleted successfully.");
                }
                else
                {
                    // Check if the deletion failed due to dependencies (status code 409 Conflict)
                    if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        MessageBox.Show("Cannot delete application. It has dependent resources.");
                    }
                    else
                    {
                        MessageBox.Show("Application deletion failed. Status Code: " + response.StatusCode + "\n" +
                                        "Content: " + response.Content);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button_disoverApps_Click(object sender, EventArgs e)
        {
            List<string> applicationNames = new List<string>();

            var client = new RestClient(@"http://localhost:59352/");

            var request = new RestRequest(@"api/somiod/", Method.Get);
            request.RequestFormat = DataFormat.Xml;

            request.AddHeader("somiod-discover", "application");
            var response = client.Execute(request);

            string xml = response.Content;

            XmlDocument doc = new XmlDocument();

            //remove first and last parameter from app.Content
            //remove first character
            doc.LoadXml(xml);

            XmlNodeList nameNodes = doc.SelectNodes("//Application/name/text()");

            foreach (XmlNode nameNode in nameNodes)
            {
                string nameValue = nameNode.Value;
                //add nameValue to appNames
                applicationNames.Add(nameValue);
            }

            appDrpDown.DataSource = applicationNames;
        }
    }
}
