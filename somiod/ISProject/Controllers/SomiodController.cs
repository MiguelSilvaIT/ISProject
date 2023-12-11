using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Xml;
using ISProject.Models;


namespace ISProject.Controllers
{
    [RoutePrefix("api/somiod")]
    public class SomiodController : ApiController
    {



        string connectionString =
        System.Configuration.ConfigurationManager.ConnectionStrings["ISProject.Properties.Settings.ConnStr"].ConnectionString;

        #region HardCoded

        List <Application> applications = new List<Application>
        {
            new Application { id = 1, name = "Application 1", creation_dt = DateTime.Now },
            new Application { id = 2, name = "Application 2", creation_dt = DateTime.Now }
        };

        List<Container> containers = new List<Container>
        {
            new Container { id = 1, name = "Container 1", creation_dt = DateTime.Now, parent = 1 }
        };

        List<Data> datas = new List<Data>
        {
            new Data { id = 1, name = "Data 1", creation_dt = DateTime.Now, parent = 1 }
        };

        List<Subscription> subscriptions = new List<Subscription>
        {
            new Subscription { id = 1, name = "Subscription 1", creation_dt = DateTime.Now, parent = 1, event_type = "event_type", endpoint = "endpoint" }
        };

        #endregion hardcoded

        #region Applications

        [HttpGet]
        [Route("")]
        public IEnumerable<Application> GetAllApplications()
        {
            List<Application> applications = new List<Application>();
            SqlConnection conn = null;
            string queryString =
            "SELECT * FROM application ORDER BY id;";

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Application a = new Application
                    {
                        id = (int)reader["id"],
                        name = (string)reader["name"],
                        creation_dt = (DateTime)reader["creation_dt"]

                    };
                    applications.Add(a);

                }
                reader.Close();
            }

            catch (Exception)
            {

                throw;
            }


            return applications;
        }

        [Route("{name}")]
        public IHttpActionResult GetApplication(String name)
        {
            Application application = new Application();
            SqlConnection conn = null;
            string queryString =
            "SELECT * FROM application WHERE name = @name;";

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@name", name);

                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    Application a = new Application
                    {
                        id = (int)reader["id"],
                        name = (string)reader["name"],
                        creation_dt = (DateTime)reader["creation_dt"]

                    };
                    application = a;

                }
                reader.Close();

                return Ok(application);

            }

            catch (Exception)
            {

                throw;
            }


        }

        //Post Application
        [HttpPost]
        [Route("")]
        public IHttpActionResult PostApplication()
        {
            
            SqlConnection sqlConnection = null;
            string queryString = "INSERT INTO application (name, creation_dt) VALUES (@name, @creation_dt);";


            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            string xml = bodyStream.ReadToEnd();



            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);


            XmlNode applicationName = doc.SelectSingleNode("/Application/name");


            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                command.Parameters.AddWithValue("@name", applicationName.InnerText);
                command.Parameters.AddWithValue("@creation_dt", DateTime.Now);
                SqlDataReader reader = command.ExecuteReader();

                return Ok();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return BadRequest(ex.Message);
            }

        }

        //Delete Application
        [HttpDelete]
        [Route("{name}")]
        public IHttpActionResult DeleteApplication(String name)
        {
            SqlConnection sqlConnection = null;
            string queryString = "DELETE FROM application WHERE name = @name;";


            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                command.Parameters.AddWithValue("@name", name);
                SqlDataReader reader = command.ExecuteReader();

                return Ok();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        //Update Application
        [HttpPut]
        [Route("{name}")]
        public IHttpActionResult UpdateApplication(String name)
        {
            SqlConnection sqlConnection = null;
            string queryString = "UPDATE application SET name = @newName WHERE name = @name; ";


            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            string xml = bodyStream.ReadToEnd();



            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);


            XmlNode applicationName = doc.SelectSingleNode("/Application/name");


            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@newName", applicationName.InnerText);
                SqlDataReader reader = command.ExecuteReader();

                return Ok();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        #endregion Applications







    }
}
