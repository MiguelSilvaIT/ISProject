using RestSharp.Authenticators;
using RestSharp;
using ISProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;

namespace App_A
{
    public partial class Form1 : Form
    {

        string appName = "";
        string containerName = "";
        
        string connectionString =
        System.Configuration.ConfigurationManager.ConnectionStrings["App_A.Properties.Settings.ConnString"].ConnectionString;


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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
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
    }
}
