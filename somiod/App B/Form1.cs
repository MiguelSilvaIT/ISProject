﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;

namespace App_B
{
    public partial class Form1 : Form
    {
        string connectionString =
            System.Configuration.ConfigurationManager.ConnectionStrings["App_B.Properties.Settings.connStr"].ConnectionString;

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
            if (TopicDrpDown.SelectedItem != null)
            {

                //Create ON            
                var client = new RestClient(@"http://localhost:59352/");
                var selectedTopic = TopicDrpDown.SelectedItem.ToString();
                //var request = new RestRequest(@"api/somiod/TVApp/TVApp Container", Method.Post);
                var request = new RestRequest(@"api/somiod/TVApp/" + selectedTopic, Method.Post);//Para ficar generico de acord com o container selecionado

                request.RequestFormat = DataFormat.Xml;
                request.AddBody(new ISProject.Models.Data
                {
                    res_type = "data",
                    content = "On",
                    name = "On",

                });
                var response = client.Execute(request);
                MessageBox.Show("Data Status Code: " + response.StatusCode + "\n" +
                                               "Content: " + response.Content);
                try
                {
                    mClient.Publish(TopicDrpDown.SelectedItem.ToString(), Encoding.UTF8.GetBytes("E ON"));
                    MessageBox.Show("Message Sent");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error publishing message: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                // Handle the case when no item is selected
                MessageBox.Show("Please select a topic before publishing.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Check if any item is selected in the ComboBox
            if (TopicDrpDown.SelectedItem != null)
            {

                //Create OFF
                var client = new RestClient(@"http://localhost:59352/");
                var selectedTopic = TopicDrpDown.SelectedItem.ToString();
                //var request = new RestRequest(@"api/somiod/TVApp/TVApp Container", Method.Post);
                var request = new RestRequest(@"api/somiod/TVApp/" + selectedTopic, Method.Post);//Para ficar generico de acord com o container selecionado


                request.RequestFormat = DataFormat.Xml;
                request.AddBody(new ISProject.Models.Data
                {
                    res_type = "data",
                    content = "Off",
                    name = "Off",

                });
                var response = client.Execute(request);
                MessageBox.Show("Data Status Code: " + response.StatusCode + "\n" +
                                               "Content: " + response.Content);

                try
                {
                    mClient.Publish(TopicDrpDown.SelectedItem.ToString(), Encoding.UTF8.GetBytes("E OFF"));
                    MessageBox.Show("Message Sent");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error publishing message: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

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
            try
            {
                string val = "V " + numericUpDown1.Value.ToString();
                mClient.Publish(TopicDrpDown.SelectedItem.ToString(), Encoding.UTF8.GetBytes(val));
                MessageBox.Show("Message Sent");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error publishing message: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Canal
        private void conCanal_Click(object sender, EventArgs e)
        {
            try
            {
                string val = "C " + numericUpDown2.Value.ToString();
                mClient.Publish(TopicDrpDown.SelectedItem.ToString(), Encoding.UTF8.GetBytes(val));
                MessageBox.Show("Message Sent");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error publishing message: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void TopicDrpDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
