using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ISProject.Models;


namespace ISProject.Controllers
{
    [RoutePrefix("api/somiod")]
    public class SomiodController : ApiController
    {

        string connectionString =
        System.Configuration.ConfigurationManager.ConnectionStrings["SOMIOD_DB.Properties.Settings.ConnStr"].ConnectionString;

        #region HardCoded

        List <Application> applications = new List<Application>
        {
            new Application { id = 1, name = "Application 1", creation_dt = DateTime.Now }
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
            return applications;
        }

        //[Route("applications/{id:int}")]
        //public IHttpActionResult GetApplication(int id)
        //{
        //    var application = applications.FirstOrDefault((p) => p.id == id);
        //    if (application == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(application);
        //}

        //Post Application
        //[HttpPost]
        //[Route("applications")]
        //public IHttpActionResult PostApplication(Application application)
        //{
        //   //procurar se existe uma aplicação com o mesmo nome
        //   var app = applications.FirstOrDefault((p) => p.name == application.name);
           
        //   if(app != null)
        //    {
        //       return BadRequest("Application already exists");
        //   }
        //   else
        //   {
        //       applications.Add(application);
        //       return Ok(application);
        //   }
        //}

        #endregion Applications




    }
}
