using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace App_B
{
    public partial class Form1 : Form
    {
       string connectionString =
       System.Configuration.ConfigurationManager.ConnectionStrings["App_B.Properties.Settings.ConnString"].ConnectionString;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Create ON
            var client = new RestClient(@"http://localhost:59352/");
            var request = new RestRequest(@"api/somiod/TVApp/TVApp Container", Method.Post);
           
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Create OFF
            var client = new RestClient(@"http://localhost:59352/");
            var request = new RestRequest(@"api/somiod/TVApp/TVApp Container", Method.Post);

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
        }
    }
}
