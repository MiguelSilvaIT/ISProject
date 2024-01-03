using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using ISProject.Models;
using Newtonsoft.Json.Linq;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using RestSharp;
using System.Reflection;


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
        public IHttpActionResult discoverContainerOrSubscriptions(String appName, String containerName) //or all data
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
                else if(somiodDiscover == "data")
                {
                    return GetALLData(appName, containerName);
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

        [HttpPost]
        [Route("{appName}/{containerName}")]
        public IHttpActionResult PostDataOrSubscription(string appName, string containerName)
        {
            // Obter o conteúdo da solicitação como XML

            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);

            string xml = bodyStream.ReadToEnd();


            var xmlDoc = new XmlDocument();
            xmlDoc.Load(Request.Content.ReadAsStreamAsync().Result);

            // Verificar se o XML contém o elemento "res_type"
            XmlNode resTypeNode = xmlDoc.SelectSingleNode("//res_type");
            if (resTypeNode != null)
            {
                // Obter o valor do elemento "res_type"
                string resType = resTypeNode.InnerText;

                // Verificar se o valor é "subscription"
                if (resType == "subscription")
                {
                    // Retornar resultado da assinatura
                    return PostSubscription(appName, containerName, xml);
                }
                else
                {
                    // Retornar resultado do contêiner
                    return PostData(appName, containerName, xml, containerName);
                }
            }
            else
            {
                // "res_type" não encontrado no XML
                return BadRequest("Res-type not found");
            }
        }


        #region Applications

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllApplications()
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


            //// Converte a lista de Application para XML
            var xmlSerializer = new XmlSerializer(typeof(List<Application>));
            StringWriter sw = new StringWriter();
            xmlSerializer.Serialize(sw, applications);
            string xmlResult = sw.ToString();



            //string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><applications><application><name>MinhaApp1</name><creation_dt>2024-01-01T12:00:00</creation_dt><id>123456</id></application><application><name>MinhaApp2</name><creation_dt>2024-01-02T12:00:00</creation_dt><id>789012</id></application></applications>";
            // Retorna os dados em formato XML
            // Retorna os dados em formato XML
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(xmlResult, Encoding.UTF8, "application/xml");

            return ResponseMessage(response);

        }

        public IHttpActionResult GetApplication(String appName)
        {



            Application app = new Application();
            SqlConnection conn = null;
            string queryString =
            "SELECT * FROM application WHERE name = @name;";

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@name", appName);

                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    Application a = new Application
                    {
                        id = (int)reader["id"],
                        name = (string)reader["name"],
                        creation_dt = (DateTime)reader["creation_dt"]

                    };
                    app = a;

                }
                reader.Close();

                //Create XML
                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.CreateElement("Application");
                doc.AppendChild(root);
                XmlElement id = doc.CreateElement("id");
                id.InnerText = app.id.ToString();
                XmlElement name = doc.CreateElement("name");
                name.InnerText = app.name;
                XmlElement creation_date = doc.CreateElement("creation_date");
                creation_date.InnerText = app.creation_dt.ToString();
                
                root.AppendChild(id);
                root.AppendChild(name);
                root.AppendChild(creation_date);

                string xmlOutput = doc.OuterXml;



                return Ok(xmlOutput);


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
            catch (SqlException ex) 
            { 
                if (ex.Number == 2627 || ex.Number == 2601) 
                { 
                    return BadRequest("Application  already exists."); 
                } 
                else 
                { 
                    System.Diagnostics.Debug.WriteLine(ex.ToString()); 
                    return BadRequest(ex.Message); 
                } 
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
                return Conflict();
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


            //// Converte a lista de Application para XML
            var xmlSerializer = new XmlSerializer(typeof(List<Container>));
            StringWriter sw = new StringWriter();
            xmlSerializer.Serialize(sw, containers);
            string xmlResult = sw.ToString();



            //string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><applications><application><name>MinhaApp1</name><creation_dt>2024-01-01T12:00:00</creation_dt><id>123456</id></application><application><name>MinhaApp2</name><creation_dt>2024-01-02T12:00:00</creation_dt><id>789012</id></application></applications>";
            // Retorna os dados em formato XML
            // Retorna os dados em formato XML
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(xmlResult, Encoding.UTF8, "application/xml");

            return ResponseMessage(response);
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


            XmlNode containerName = doc.SelectSingleNode("/Container/name");


            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                command.Parameters.AddWithValue("@name", containerName.InnerText);
                command.Parameters.AddWithValue("@appName", appName);
                command.Parameters.AddWithValue("@creation_dt", DateTime.Now);
                SqlDataReader reader = command.ExecuteReader();

                return Ok();
            }
            catch (SqlException ex)
            {
                // Check if the exception is related to a primary key violation
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    // Error numbers 2627 and 2601 are associated with unique constraint violations
                    return BadRequest("Container  already exists.");
                }
                else
                {
                    // Handle other SQL exceptions
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return BadRequest(ex.Message);
                }
            }
        }

        //Update Container
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

        //delete Container
        [HttpDelete]
        [Route("{appName}/{name}")]
        public IHttpActionResult DeleteContainer(String appName, String name)
        {
            SqlConnection sqlConnection = null;
            string queryString = "DELETE FROM container WHERE name = @name AND parent = (SELECT id FROM application WHERE name = @appName); ";

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@appName", appName);
                SqlDataReader reader = command.ExecuteReader();

                return Ok();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return Conflict();
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


            //// Converte a lista de Subscrições para XML
            var xmlSerializer = new XmlSerializer(typeof(List<Subscription>));
            StringWriter sw = new StringWriter();
            xmlSerializer.Serialize(sw, subscriptions);
            string xmlResult = sw.ToString();



            //string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><applications><application><name>MinhaApp1</name><creation_dt>2024-01-01T12:00:00</creation_dt><id>123456</id></application><application><name>MinhaApp2</name><creation_dt>2024-01-02T12:00:00</creation_dt><id>789012</id></application></applications>";
            // Retorna os dados em formato XML
            // Retorna os dados em formato XML
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(xmlResult, Encoding.UTF8, "application/xml");

            return ResponseMessage(response);
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


       
        public IHttpActionResult PostSubscription(String appName, String containerName, string xml)
        {
            Subscription subscription = new Subscription();
            SqlConnection sqlConnection = null;

            string queryString = "INSERT INTO subscription (name, creation_dt, parent, event, endpoint) VALUES (@name, @creation_dt, " +
                "(SELECT id FROM container WHERE name = @containerName AND parent = (SELECT id FROM application WHERE name = @appName)), @event, @endpoint);";

            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);

   

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
            catch (SqlException ex)
            {
                // Check if the exception is related to a primary key violation
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    // Error numbers 2627 and 2601 are associated with unique constraint violations
                    return BadRequest("Subscription  already exists.");
                }
                else
                {
                    // Handle other SQL exceptions
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return BadRequest(ex.Message);
                }
            }

        }

        [HttpDelete]
        [Route("{appName}/{containerName}/subscription/{subscriptionName}")]
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

        #region Data

        //Get all data from 1 container
        
        public IHttpActionResult GetALLData(string appName, string containerName)
        {
            List<Data> data = new List<Data>();
            int containerID = -1;
            var response = new HttpResponseMessage(HttpStatusCode.OK);


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string getContainer = "SELECT id FROM container WHERE name = @containerName and parent = (SELECT id FROM application WHERE name = @appName)";

                    using (SqlCommand command = new SqlCommand(getContainer, conn))
                    {
                        command.Parameters.AddWithValue("@appName", appName);
                        command.Parameters.AddWithValue("@containerName", containerName);

                        using (SqlDataReader reader1 = command.ExecuteReader())
                        {
                            if (reader1.Read())
                            {
                                containerID = reader1.IsDBNull(0) ? -1 : reader1.GetInt32(0);
                            }
                        }
                    }

                    if (containerID != -1)
                    {
                        string getDataQuery = "SELECT * FROM data WHERE parent = @id";

                        using (SqlCommand command = new SqlCommand(getDataQuery, conn))
                        {
                            command.Parameters.AddWithValue("@id", containerID);

                            using (SqlDataReader dataReader = command.ExecuteReader())
                            {
                                while (dataReader.Read())
                                {
                                    Data d = new Data
                                    {
                                        id = (int)dataReader["id"],
                                        name = (string)dataReader["name"],
                                        content = (string)dataReader["content"],
                                        creation_dt = (DateTime)dataReader["creation_dt"],
                                        parent = (int)dataReader["parent"]
                                    };
                                    data.Add(d);
                                }
                            }
                        }
                    }
                }

                if (data.Count() > 0)
                {


                    var xmlSerializer = new XmlSerializer(typeof(List<Data>));
                    StringWriter sw = new StringWriter();
                    xmlSerializer.Serialize(sw, data);
                    string xmlResult = sw.ToString();



                    //string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><applications><application><name>MinhaApp1</name><creation_dt>2024-01-01T12:00:00</creation_dt><id>123456</id></application><application><name>MinhaApp2</name><creation_dt>2024-01-02T12:00:00</creation_dt><id>789012</id></application></applications>";
                    // Retorna os dados em formato XML
                    // Retorna os dados em formato XML
                    response.Content = new StringContent(xmlResult, Encoding.UTF8, "application/xml");

                    return ResponseMessage(response);
                }
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("No data found", Encoding.UTF8, "application/xml");
                return ResponseMessage(response);
            }
            catch (Exception ex)
            {
                
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new StringContent(ex.Message, Encoding.UTF8, "application/xml");
                return ResponseMessage(response);
            }

        }

        [HttpGet]
        [Route("{appName}/{containerName}/data/{dataName}")]
        public IHttpActionResult GetData(string appName, string containerName, string dataName) {
            Data data = new Data();
            int containerID = -1;


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string getContainer = "SELECT id FROM container WHERE name = @containerName and parent = (SELECT id FROM application WHERE name = @appName)";

                    using (SqlCommand command = new SqlCommand(getContainer, conn))
                    {
                        command.Parameters.AddWithValue("@appName", appName);
                        command.Parameters.AddWithValue("@containerName", containerName);

                        using (SqlDataReader reader1 = command.ExecuteReader())
                        {
                            if (reader1.Read())
                            {
                                containerID = reader1.IsDBNull(0) ? -1 : reader1.GetInt32(0);
                            }
                        }
                    }

                    if (containerID != -1)
                    {
                        string getDataQuery = "SELECT * FROM data WHERE parent = @id AND name = @dataName";

                        using (SqlCommand dataCommand = new SqlCommand(getDataQuery, conn))
                        {
                            dataCommand.Parameters.AddWithValue("@id", containerID);
                            dataCommand.Parameters.AddWithValue("@dataName", dataName);

                            using (SqlDataReader dataReader = dataCommand.ExecuteReader())
                            {
                                while (dataReader.Read())
                                {
                                    Data d = new Data
                                    {
                                        id = (int)dataReader["id"],
                                        content = (string)dataReader["content"],
                                        creation_dt = (DateTime)dataReader["creation_dt"],
                                        parent = (int)dataReader["parent"]
                                    };
                                    data = d;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw;
            }

            if (data.id != 0)
            {
                return Ok(data);
            }
            return BadRequest();
        }


        public IHttpActionResult PostData(string appName, string containerName, string xml, string container)
        {
            SqlConnection sqlConnection = null;

            string checkExistingQuery = "SELECT COUNT(*) FROM data WHERE name = @name AND parent = " +
                "(SELECT id FROM container WHERE name = @containerName AND parent = " +
                "(SELECT id FROM application WHERE name = @appName));";

            string insertQuery = "INSERT INTO data (name, content, parent, creation_dt) VALUES " +
                "(@name, @content, " +
                "(SELECT id FROM container " +
                "WHERE name = @containerName AND parent = (SELECT id FROM application WHERE name = @appName)), @creation_dt);";

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                XmlNode dataName = doc.SelectSingleNode("/Data/name");
                XmlNode dataContent = doc.SelectSingleNode("/Data/content");

                if (dataName == null || dataContent == null)
                {
                    return BadRequest("Invalid XML format. Missing required elements.");
                }

                using (sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    // Verificar a existência do nome na tabela data
                    using (SqlCommand checkCommand = new SqlCommand(checkExistingQuery, sqlConnection))
                    {
                        checkCommand.Parameters.AddWithValue("@appName", appName);
                        checkCommand.Parameters.AddWithValue("@containerName", containerName);
                        checkCommand.Parameters.AddWithValue("@name", dataName.InnerText);

                        int existingCount = (int)checkCommand.ExecuteScalar();

                       
                        if (existingCount > 0)
                        {
                            return sendNotifications(appName, containerName, dataContent.InnerText);
                        }
                    }

                    // Se o nome não existe, realizar a inserção
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@appName", appName);
                        insertCommand.Parameters.AddWithValue("@containerName", containerName);
                        insertCommand.Parameters.AddWithValue("@name", dataName.InnerText);
                        insertCommand.Parameters.AddWithValue("@content", dataContent.InnerText);
                        insertCommand.Parameters.AddWithValue("@creation_dt", DateTime.Now);

                        insertCommand.ExecuteNonQuery();
                        sendNotifications(appName, containerName, "E ON");
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return BadRequest(ex.Message);
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        public IHttpActionResult sendNotifications (string appName, string containerName, string dataContent)
        {
            SqlConnection sqlConnection = null;
            int containerID = -1;

            string getContainerID = "SELECT id FROM container WHERE name = @containerName AND parent = (SELECT id FROM application WHERE name = @appName)";

            try
            { 
                
                using (sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(getContainerID, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@appName", appName);
                        sqlCommand.Parameters.AddWithValue("@containerName", containerName);

                        containerID = (int)sqlCommand.ExecuteScalar();

                    }

                }
                string ipAddressString = null;
                string getipAddress = "SELECT endpoint FROM subscription WHERE parent = @containerID";

                using (sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(getipAddress, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@containerID", containerID);

                        object result = sqlCommand.ExecuteScalar();

                        // Check if the result is not null before converting to string
                        if (result != null)
                        {
                            ipAddressString = result.ToString();
                        }

                    }
                }

                MqttClient mClient = new MqttClient(IPAddress.Parse(ipAddressString));

                mClient.Connect(Guid.NewGuid().ToString());
                if (!mClient.IsConnected)
                {
                    return BadRequest("Error connecting to message broker...");
                }

                mClient.Publish(containerName, Encoding.UTF8.GetBytes(dataContent));
                return Ok("Message Sent");
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        [HttpDelete]
        [Route("{appName}/{containerName}/data/{dataName}")]
        public IHttpActionResult DeleteData(string appName, string containerName, string dataName) {
            int containerID = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string getcontainerid = "SELECT id FROM container WHERE parent = (SELECT id FROM application WHERE name = @appName) AND name = @containerName";

                    using (SqlCommand command = new SqlCommand(getcontainerid, conn))
                    {
                        command.Parameters.AddWithValue("@appName", appName);
                        command.Parameters.AddWithValue("@containerName", containerName);

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                containerID = reader.IsDBNull(0) ? -1 : reader.GetInt32(0);
                            }
                        }
                    }

                    if (containerID != -1)
                    {
                        string deleteData = "DELETE FROM data WHERE name = @dataName";

                        using (SqlCommand command = new SqlCommand(deleteData, conn))
                        {
                            command.Parameters.AddWithValue("@dataName", dataName);

                            SqlDataReader reader = command.ExecuteReader();

                            return Ok("Data deleted");
                        }
                    }

                }
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return BadRequest(ex.Message);
            }
            return BadRequest();
        }

        #endregion


    }
}
