using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using uPLibrary.Networking.M2Mqtt;

namespace App_B
{
    public partial class Form1 : Form
    {
        string connectionString =
            System.Configuration.ConfigurationManager.ConnectionStrings["App_B.Properties.Settings.ConnString"].ConnectionString;

        MqttClient mClient = null;
        List<string> mStrApp = new List<string>();
        List<string> mStrContainer = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Check if any item is selected in the ComboBox
            if (ContainerDrpDown.SelectedItem != null)
            {

                //Create ON            
                var client = new RestClient(@"http://localhost:59352/");
                var selectedTopic = ContainerDrpDown.SelectedItem.ToString();
                //var request = new RestRequest(@"api/somiod/TVApp/TVApp Container", Method.Post);
                var request = new RestRequest(@"api/somiod/TVApp/" + selectedTopic, Method.Post);//Para ficar generico de acord com o container selecionado

                request.RequestFormat = DataFormat.Xml;
                request.AddBody(new ISProject.Models.Data
                {
                    res_type = "data",
                    content = "E On",
                    name = "On",

                });
                var response = client.Execute(request);
                //DEBUG MessageBox.Show("Data Status Code: " + response.StatusCode + "\n" +
                                               //"Content: " + response.Content);
                //try
                //{
                //    mClient.Publish(TopicDrpDown.SelectedItem.ToString(), Encoding.UTF8.GetBytes("E ON"));
                //    MessageBox.Show("Message Sent");
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show($"Error publishing message: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}

            }
            else
            {
                // Handle the case when no item is selected
                MessageBox.Show("Please select a topic before publishing.");
                return;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Check if any item is selected in the ComboBox
            if (ContainerDrpDown.SelectedItem != null)
            {

                //Create OFF
                var client = new RestClient(@"http://localhost:59352/");
                var selectedTopic = ContainerDrpDown.SelectedItem.ToString();
                //var request = new RestRequest(@"api/somiod/TVApp/TVApp Container", Method.Post);
                var request = new RestRequest(@"api/somiod/TVApp/" + selectedTopic, Method.Post);//Para ficar generico de acord com o container selecionado


                request.RequestFormat = DataFormat.Xml;
                request.AddBody(new ISProject.Models.Data
                {
                    res_type = "data",
                    content = "E Off",
                    name = "Off",

                });
                var response = client.Execute(request);
                //DEBUG MessageBox.Show("Data Status Code: " + response.StatusCode + "\n" +
                                               //"Content: " + response.Content);

            }
            else
            {
                // Handle the case when no item is selected
                MessageBox.Show("Please select a topic before publishing.");
            }
        }

        //Volume
        private void conVolume_Click(object sender, EventArgs e)
        {

            //Create Volume
            var selectedVolume = numericUpDown1.Value;
            var selectedApp = appDrpDown.SelectedItem.ToString();
            var selectedContainer = ContainerDrpDown.SelectedItem.ToString();

            var client = new RestClient(@"http://localhost:59352/");

            var request = new RestRequest(@"api/somiod/" +selectedApp +"/" + selectedContainer, Method.Post);

            request.RequestFormat = DataFormat.Xml;
            request.AddBody(new ISProject.Models.Data
            {
                res_type = "data",
                content = "V" + selectedVolume.ToString(),
                name = "Volume of: " + selectedContainer,

            });
            var response = client.Execute(request);
            //DEBUG MessageBox.Show("Data Status Code: " + response.StatusCode + "\n" +
                                           //"Content: " + response.Content);
            try
            {
                //string val = "V " + selectedVolume;
                //mClient.Publish(TopicDrpDown.SelectedItem.ToString(), Encoding.UTF8.GetBytes(val));
                //MessageBox.Show("Message Sent");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error publishing message: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Canal
        private void conCanal_Click(object sender, EventArgs e)
        {
            //Create Canal
            var selectedCanal = numericUpDown2.Value;
            var selectedApp = appDrpDown.SelectedItem.ToString();
            var selectedContainer = ContainerDrpDown.SelectedItem.ToString();

            var client = new RestClient(@"http://localhost:59352/");

            var request = new RestRequest(@"api/somiod/" + selectedApp + "/" + selectedContainer, Method.Post);

            request.RequestFormat = DataFormat.Xml;
            request.AddBody(new ISProject.Models.Data
            {
                res_type = "data",
                content = "C" + selectedCanal.ToString(),
                name = "Canal of: " + selectedContainer,

            });
            var response = client.Execute(request);
            MessageBox.Show("Data Status Code: " + response.StatusCode + "\n" +
                                           "Content: " + response.Content);
            //try
            //{
            //    string val = "C " + numericUpDown2.Value.ToString();
            //    mClient.Publish(TopicDrpDown.SelectedItem.ToString(), Encoding.UTF8.GetBytes(val));
            //    MessageBox.Show("Message Sent");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Error publishing message: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            SqlConnection sqlConnection = null;

            string queryString = "SELECT * FROM application WHERE name = 'Comando'";

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    //Create Comando application
                    var client = new RestClient(@"http://localhost:59352/");
                    var request = new RestRequest(@"api/somiod/", Method.Post);
                    request.RequestFormat = DataFormat.Xml;
                    request.AddBody(new ISProject.Models.Application
                    {
                        name = "Comando",
                    });

                    var response = client.Execute(request);
                    MessageBox.Show("Application Status Code: " + response.StatusCode + "\n" +
                                    "Content: " + response.Content);      
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

            ////Applications
            //try
            //{
            //    using (sqlConnection = new SqlConnection(connectionString))
            //    {
            //        string getApps = "SELECT name FROM application";

            //        SqlCommand command = new SqlCommand(getApps, sqlConnection);
            //        sqlConnection.Open();
            //        SqlDataReader reader = command.ExecuteReader();

            //        while (reader.Read())
            //        {
            //            mStrApp.Add((string)reader["name"]);

            //        }
            //        appDrpDown.DataSource = mStrApp;
            //        reader.Close();
            //        sqlConnection.Close();
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }

        private void connBTN_Click(object sender, EventArgs e)
        {
            mClient = new MqttClient(IPAddress.Parse(ipAddressTXT.Text));
            mClient.Connect(Guid.NewGuid().ToString());
            if (!mClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }
            MessageBox.Show("Connected");
        }

        private void getTopicsBTN_Click(object sender, EventArgs e)
        {
            
            if(appDrpDown.SelectedItem ==null)
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

            ContainerDrpDown.DataSource = containerNames;

        }

        private void TopicDrpDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void app_discover_Click(object sender, EventArgs e)
        {

            List<string> applicationNames = new List<string>();

            var client = new RestClient(@"http://localhost:59352/");

            var request = new RestRequest(@"api/somiod/" , Method.Get);
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btn_discover_data_Click(object sender, EventArgs e)
        {

            comboBoxSubscription.DataSource = null;
            comboBoxSubscription.Items.Clear();

            if (ContainerDrpDown.SelectedItem == null)
            {
                MessageBox.Show("Please select a container");
                return;
            }
            
            List<string> dataNames = new List<string>();
            var client = new RestClient(@"http://localhost:59352/");
            var request = new RestRequest(@"api/somiod/" + appDrpDown.SelectedItem.ToString() + "/" + ContainerDrpDown.SelectedItem.ToString(), Method.Get);
            request.RequestFormat = DataFormat.Xml;
            request.AddHeader("somiod-discover", "data");
            var response = client.Execute(request);
            if(response.StatusCode == HttpStatusCode.NotFound)
            {
                MessageBox.Show("No data found");
                return;
            }

            string xml = response.Content;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeList nameNodes = doc.SelectNodes("//Data/name/text()");
            foreach (XmlNode nameNode in nameNodes)
            {
                string nameValue = nameNode.Value;
                //add nameValue to appNames
                dataNames.Add(nameValue);
            }

            comboBoxData.DataSource = dataNames;
        }

        private void btn_delete_data_Click(object sender, EventArgs e)
        {
            
            if(comboBoxData.SelectedItem == null)
            {
                MessageBox.Show("Please select a data");
                return;
            }

            try
            {
                var client = new RestClient(@"http://localhost:59352/");


                
                var request = new RestRequest(@"api/somiod/"+ appDrpDown.SelectedItem.ToString() + "/" + ContainerDrpDown.SelectedItem.ToString() + "/data/" + comboBoxData.SelectedItem.ToString(), Method.Delete);
                var response = client.Execute(request);

                // Check if the deletion was successful (status code 200 OK)
                if (response.StatusCode == HttpStatusCode.OK)
                {
                   
                    MessageBox.Show("Data "+ comboBoxData.SelectedItem.ToString() + " deleted successfully.");
                    btn_discover_data_Click(sender, e);
                }
                else
                {
                   
                    MessageBox.Show("Container deletion failed. Status Code: " + response.StatusCode + "\n" +
                                    "Content: " + response.Content);
                    
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void button_discoverSubscriptions_Click(object sender, EventArgs e)
        {

            comboBoxSubscription.DataSource = null;
            comboBoxSubscription.Items.Clear();
            if (ContainerDrpDown.SelectedItem == null)
            {
                MessageBox.Show("Please select a container");
                return;
            }

            List<string> subscriptionNames = new List<string>();
            var client = new RestClient(@"http://localhost:59352/");
            var request = new RestRequest(@"api/somiod/" + appDrpDown.SelectedItem.ToString() + "/" + ContainerDrpDown.SelectedItem.ToString(), Method.Get);
            request.RequestFormat = DataFormat.Xml;
            request.AddHeader("somiod-discover", "subscription");
            var response = client.Execute(request);

            string xml = response.Content;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeList nameNodes = doc.SelectNodes("//Subscription/name/text()");
            foreach (XmlNode nameNode in nameNodes)
            {
                string nameValue = nameNode.Value;
                //add nameValue to appNames
                subscriptionNames.Add(nameValue);
            }

            comboBoxSubscription.DataSource = subscriptionNames;
        }

        private void btn_delete_subscription_Click(object sender, EventArgs e)
        {
            if (comboBoxSubscription.SelectedItem == null)
            {
                MessageBox.Show("Please select a subscription");
                return;
            }

            try
            {
                var client = new RestClient(@"http://localhost:59352/");



                var request = new RestRequest(@"api/somiod/" + appDrpDown.SelectedItem.ToString() + "/" + ContainerDrpDown.SelectedItem.ToString() + "/subscription/" + comboBoxSubscription.SelectedItem.ToString(), Method.Delete);
                var response = client.Execute(request);

                // Check if the deletion was successful (status code 200 OK)
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    MessageBox.Show("Subscription " + comboBoxSubscription.SelectedItem.ToString() + " deleted successfully.");
                    button_discoverSubscriptions_Click(sender, e);
                }
                else
                {

                    MessageBox.Show("Container deletion failed. Status Code: " + response.StatusCode + "\n" +
                                    "Content: " + response.Content);

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
