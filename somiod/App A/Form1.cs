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

            SqlConnection sqlConnection = null;

            string queryString = "SELECT * FROM application WHERE name = 'TVApp'";

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
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
                    MessageBox.Show("Application Status Code: " + response.StatusCode + "\n" +
                                    "Content: " + response.Content);

                    //Create TV container
                    request = new RestRequest(@"api/somiod/TVApp", Method.Post);
                    request.RequestFormat = DataFormat.Xml;
                    request.AddHeader("somiod-discover", "container");
                    request.AddBody(new ISProject.Models.Container
                    {
                        name = "TVApp Container"
                    });
                    //see request content
                    var body = request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);

                    Console.WriteLine("CurrentBody={0}", body.Value);




                    response = client.Execute(request);
                    MessageBox.Show("Container Status Code: " + response.StatusCode + "\n" +
                                    "Content: " + response.Content);

                    //Create TV subscription
                    request = new RestRequest(@"api/somiod/TVApp/TVApp Container", Method.Post);
                    request.RequestFormat = DataFormat.Xml;
                    request.AddHeader("somiod-discover", "subscription");
                    request.AddBody(new ISProject.Models.Subscription
                    {
                        name = "TVApp Subscription",
                        endpoint = "mqtt://192.168.1.1:1883",
                        event_type = "1"
                    });
                    response = client.Execute(request);
                    MessageBox.Show("Subscription Status Code: " + response.StatusCode + "\n" +
                                                   "Content: " + response.Content);
                }
               
                reader.Close();
            }
            catch (Exception ex)
            {
           
                MessageBox.Show(ex.InnerException.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

            //Applications
            try
            {
                using (sqlConnection = new SqlConnection(connectionString))
                {
                    string getApps = "SELECT name FROM application";

                    SqlCommand command = new SqlCommand(getApps, sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        mStrApp.Add((string)reader["name"]);

                    }
                    appDrpDown.DataSource = mStrApp;
                    reader.Close();
                    sqlConnection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void submit_application_Click(object sender, EventArgs e)
        {
            //Create TV application
            var client = new RestClient(@"http://localhost:59352/");
            var request = new RestRequest(@"api/somiod/", Method.Post);
            request.RequestFormat = DataFormat.Xml;
            request.AddBody(new ISProject.Models.Application
            { 
                name = inputAppName.Text 
            });

            var response = client.Execute(request);
            MessageBox.Show("Status Code: " + response.StatusCode + "\n" + 
                            "Content: " + response.Content);
        }

        private void submit_container_Click(object sender, EventArgs e)
        {
            
            
            //Create TV container
            var client = new RestClient(@"http://localhost:59352/");
            var request = new RestRequest(@"api/somiod/", Method.Post);
            request.RequestFormat = DataFormat.Xml;
            request.AddBody(new ISProject.Models.Application
            {
                name = inputAppName.Text
            });

            var response = client.Execute(request);
            MessageBox.Show("Status Code: " + response.StatusCode + "\n" +
                            "Content: " + response.Content);
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
            // Check if any item is selected in the ComboBox
            if (TopicDrpDown.SelectedItem != null)
            {
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
                return;
            }
            MessageBox.Show("There is no connection created");
        }

        private void getTopicsBTN_Click(object sender, EventArgs e)
        {
            string appName = appDrpDown.SelectedItem.ToString();
            mStrContainer.Clear();
            TopicDrpDown.DataSource = null;
            TopicDrpDown.Items.Clear();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string getContainers = "SELECT name FROM container WHERE parent = (SELECT id FROM application WHERE name = @name)";

                SqlCommand command = new SqlCommand(getContainers, sqlConnection);
                command.Parameters.AddWithValue("@name", appName);

                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    mStrContainer.Add((string)reader["name"]);
                }
                TopicDrpDown.DataSource = mStrContainer;
                reader.Close();
                sqlConnection.Close();
            }
        }
    }
}
