using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using ISProject.Models;


namespace ISProject.Controllers
{
    [RoutePrefix("api/somiod")]
    public class SomiodController : ApiController
    {



        string connectionString =
        System.Configuration.ConfigurationManager.ConnectionStrings["ISProject.Properties.Settings.ConnStr"].ConnectionString;

        
        //Pq os caminhos para obter os detalhes da App1 e Os containers da App são os msms
        [HttpGet]
        [Route("{name}")]
        public IHttpActionResult discoverAppOrContainers(String name)
        {
            //get header of request
            var headers = Request.Headers;
            //check if header contains "somiod-discover"
            if (headers.Contains("somiod-discover"))
            {
                //get value of header
                string somiodDiscover = headers.GetValues("somiod-discover").First();
                //check if value is "container"
                if (somiodDiscover == "container")
                {
                    //return container
                    return GetAllContainers(name);
                }
                else
                {
                    //return application
                    return GetApplication(name);
                }
            }
            else
            {
                return BadRequest("Header somiod-discover not found");
            }
        }

        [HttpGet]
        [Route("{appName}/{containerName}")]
        public IHttpActionResult discoverContainerOrSubscriptions(String appName, String containerName)
        {
            //get header of request
            var headers = Request.Headers;
            //check if header contains "somiod-discover"
            if (headers.Contains("somiod-discover"))
            {
                //get value of header
                string somiodDiscover = headers.GetValues("somiod-discover").First();
                //check if value is "subscription"
                if (somiodDiscover == "subscription")
                {
                    //return subscription
                    return GetAllSubscriptions(appName, containerName);
                }
                else
                {
                    //return container
                    return GetContainer(appName, containerName);
                }
            }
            else
            {
                return BadRequest("Header somiod-discover not found");
            }
        }


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



        #region Containers


        public IHttpActionResult GetAllContainers(String appName)
        {
            List<Container> containers = new List<Container>();
            SqlConnection conn = null;
            string queryString =
            "SELECT * FROM container WHERE parent = (SELECT id FROM application WHERE name = @appName) ORDER BY id;";

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@appName", appName);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Container a = new Container
                    {
                        id = (int)reader["id"],
                        name = (string)reader["name"],
                        creation_dt = (DateTime)reader["creation_dt"],
                        parent = (int)reader["parent"]

                    };
                    containers.Add(a);

                }
                reader.Close();
            }

            catch (Exception)
            {

                throw;
            }


            return Ok(containers);
        }

   
        public IHttpActionResult GetContainer(String appName, String containerName)
        {
            Application application = new Application();
            SqlConnection conn = null;
            string queryString =
            "SELECT * FROM container WHERE parent = (SELECT id FROM application WHERE name = @appName) AND name = @containerName " +
            "ORDER BY id;";
    

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@appName", appName);
                command.Parameters.AddWithValue("@containerName", containerName);

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

        //Post Container
        [HttpPost]
        [Route("{appName}")]
        public IHttpActionResult PostContainer(String appName)
        {

            SqlConnection sqlConnection = null;
            string queryString = "INSERT INTO container (name, creation_dt, parent) VALUES (@name, @creation_dt, (SELECT id FROM application WHERE name = @appName));";

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
                command.Parameters.AddWithValue("@appName", appName);
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

        //Update Application
        [HttpPut]
        [Route("{appName}/{name}")]
        public IHttpActionResult UpdateContainer(String name)
        {
            SqlConnection sqlConnection = null;
            string queryString  = "UPDATE container SET name = @newName WHERE name = @name; ";


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

        #endregion


        #region Subscriptions
        

        public IHttpActionResult GetAllSubscriptions(String appName, String containerName)
        {
            List<Subscription> subscriptions = new List<Subscription>();
            SqlConnection conn = null;
            string queryString =
            "SELECT * FROM subscription WHERE parent = (SELECT id FROM container WHERE name = @containerName AND parent = (SELECT id FROM application WHERE name = @appName)) ORDER BY id;";

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@appName", appName);
                command.Parameters.AddWithValue("@containerName", containerName);
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    Subscription a = new Subscription
                    {
                        id = (int)reader["id"],
                        name = (string)reader["name"],
                        creation_dt = (DateTime)reader["creation_dt"],
                        parent = (int)reader["parent"],
                        event_type = (string)reader["event"],  
                        endpoint = (string)reader["endpoint"]  


                    };
                    subscriptions.Add(a);

                }
                reader.Close();
            }

            catch (Exception)
            {

                throw;
            }


            return Ok(subscriptions);
        }

        [HttpGet]
        [Route("{appName}/{containerName}/{subscriptionName}")]
        public IHttpActionResult GetSubscription(String appName, String containerName, String subscriptionName)
        {
            Subscription subscription = new Subscription();
            SqlConnection conn = null;

            string queryString = "SELECT * FROM subscription WHERE parent = (SELECT id FROM container WHERE name = @containerName " +
                "AND parent = (SELECT id FROM application WHERE name = @appName)) AND name = @subscriptionName ORDER BY id;";

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@appName", appName);
                command.Parameters.AddWithValue("@containerName", containerName);
                command.Parameters.AddWithValue("@subscriptionName", subscriptionName);

                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    Subscription a = new Subscription
                    {
                        id = (int)reader["id"],
                        name = (string)reader["name"],
                        creation_dt = (DateTime)reader["creation_dt"],
                        parent = (int)reader["parent"],
                        event_type = (string)reader["event"],
                        endpoint = (string)reader["endpoint"]

                    };
                    subscription = a;

                }
                reader.Close();

                return Ok(subscription);

            }

            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        [Route("{appName}/{containerName}")]
        public IHttpActionResult PostSubscription(String appName, String containerName)
        {
            Subscription subscription = new Subscription();
            SqlConnection sqlConnection = null;

            string queryString = "INSERT INTO subscription (name, creation_dt, parent, event, endpoint) VALUES (@name, @creation_dt, " +
                "(SELECT id FROM container WHERE name = @containerName AND parent = (SELECT id FROM application WHERE name = @appName)), @event, @endpoint);";

            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);

            string xml = bodyStream.ReadToEnd();

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(xml);

            XmlNode subscriptionName = doc.SelectSingleNode("/Subscription/name");
            XmlNode subscriptionEvent = doc.SelectSingleNode("/Subscription/event_type");
            XmlNode subscriptionEndpoint = doc.SelectSingleNode("/Subscription/endpoint");

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand command = new SqlCommand(queryString, sqlConnection);

                command.Parameters.AddWithValue("@appName", appName);
                command.Parameters.AddWithValue("@containerName", containerName);

                command.Parameters.AddWithValue("@name", subscriptionName.InnerText);
                command.Parameters.AddWithValue("@event", subscriptionEvent.InnerText);
                command.Parameters.AddWithValue("@endpoint", subscriptionEndpoint.InnerText);
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

        [HttpDelete]
        [Route("{appName}/{containerName}/{subscriptionName}")]
        public IHttpActionResult DeleteSubscription(String appName, String containerName, String subscriptionName)
        {
            SqlConnection sqlConnection = null;
            string queryString = "DELETE FROM subscription WHERE name = @subscriptionName AND parent = (SELECT id FROM container WHERE name = @containerName AND parent = (SELECT id FROM application WHERE name = @appName));";

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                command.Parameters.AddWithValue("@appName", appName);
                command.Parameters.AddWithValue("@containerName", containerName);
                command.Parameters.AddWithValue("@subscriptionName", subscriptionName);
                SqlDataReader reader = command.ExecuteReader();

                return Ok();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return BadRequest(ex.Message);
            }
        }


        #endregion Subscriptions




    }
}
