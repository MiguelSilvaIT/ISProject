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

        string baseURI = @"http://localhost:59352/api/somiod";
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //Create TV application
            var client = new RestClient(@"http://localhost:59352/");
            //var request = new RestRequest(@"api/somiod/", Method.Post);
            //request.RequestFormat = DataFormat.Xml;
            //request.AddBody(new ISProject.Models.Application
            //{ 
            //    name = "Television Teste" 
            //});

            //var response = client.Execute(request);
            //MessageBox.Show("Status Code: " + response.StatusCode + "\n" + 
            //                "Content: " + response.Content);



            //Create TV container

            //get application id
            var request = new RestRequest(@"api/somiod/{AppName}", Method.Get);
            request.AddUrlSegment("AppName", "Television Teste");
            request.AddHeader("somiod-discover" , "application");
            //show request endpoint
            //DEBUG - MessageBox.Show("Request Endpoint: " + client.BuildUri(request).ToString());
            var response = client.Execute(request);
            var app = response.Content;
            MessageBox.Show("Status Code: " + response.StatusCode + "\n" +
                                           "Content: " + app);

            //receber o id da aplicação por xml
            XmlDocument doc = new XmlDocument();
            
            //remove first and last parameter from app.Content
            //remove first character
            doc.Load(app);
            XmlNodeList elemList = doc.GetElementsByTagName("id");
            string id = elemList[0].InnerXml;
            MessageBox.Show("Application ID: " + id);



           

            //request = new RestRequest(@"api/somiod/", Method.Post);
            //request.RequestFormat = DataFormat.Xml;
            //request.AddBody(new ISProject.Models.Container
            //{
            //    name = "Television Teste"
            //});

            //response = client.Execute(request);
            //MessageBox.Show("Status Code: " + response.StatusCode + "\n" +
            //                "Content: " + response.Content);
        }
    }
}
