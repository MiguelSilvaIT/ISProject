using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Xml;
using ISProject.Models;


namespace ISProject.Controllers
{
    [RoutePrefix("api/somiod")]
    public class SomiodController : ApiController
    {

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ISProject.Properties.Settings.ConnStr"].ConnectionString;

        [HttpGet]
        [Route("{name}")]
        public IHttpActionResult discoverAppOrContainer(String name)
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

        [HttpPost]
        [Route("{appName}/{containerName}")]
        public IHttpActionResult createDataOrSubscription(string appName, string containerName)
        {
            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            string xml = bodyStream.ReadToEnd();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNode res_type = doc.SelectSingleNode("/Data/res_type");
            XmlNode dataName = doc.SelectSingleNode("/Data/name");
            XmlNode dataContent = doc.SelectSingleNode("/Data/content");

            if (res_type.InnerText.Equals("data")){
                //return PostData
                return PostData(appName, containerName, dataName.InnerText, dataContent.InnerText);
            }
            else
            {
                return BadRequest("Unable To Create: " + res_type.InnerText);
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

        [HttpGet]
        [Route("{appName}/{containerName}")]
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
            string queryString = "UPDATE container SET name = @newName WHERE name = @name; ";


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

        #region Data

        //Get all data from 1 container
        [HttpGet]
        [Route("{appName}/{containerName}/data")]
        public IHttpActionResult GetALLData(string appName, string containerName)
        {
            List<Data> data = new List<Data>();
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
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return BadRequest(ex.Message);
            }

            if (data.Count() > 0)
            {
                return Ok(data);
            }
            return BadRequest("Error retrieving data");
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
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return BadRequest(ex.Message);
            }

            if (data.id != 0)
            {
                return Ok(data);
            }
            return BadRequest();
        }

        public IHttpActionResult PostData(string appName, string containerName, string dataName, string dataContent)
        {
            int containerID= -1;

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

                        string createData = "INSERT INTO data (name, content, creation_dt, parent) VALUES (@dataName, @content, @creation_dt, @parent)";
                        SqlCommand command = new SqlCommand(createData, conn);

                        command.Parameters.AddWithValue("@dataName", dataName);
                        command.Parameters.AddWithValue("@content", dataContent);
                        command.Parameters.AddWithValue("@creation_dt", DateTime.Now);
                        command.Parameters.AddWithValue("@parent", containerID);

                        SqlDataReader reader = command.ExecuteReader();

                        return Ok("Data created");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return BadRequest(ex.Message);
            }
            return BadRequest();
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
