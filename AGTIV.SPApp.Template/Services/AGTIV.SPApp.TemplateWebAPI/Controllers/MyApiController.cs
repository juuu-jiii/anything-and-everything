using AGTIV.SPApp.Template.Entities;
using AGTIV.SPApp.Template.BusinessComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AGTIV.SPApp.Template.Framework;
using Microsoft.SharePoint.Client;
using System.Data.SqlClient;

namespace AGTIV.SPApp.TemplateWebAPI.Controllers
{
    public class MyApiController : ApiController
    {

        [Route("api/Sample/DemoGet")]
        [HttpGet]
        public IHttpActionResult DemoGet(string param1)
        {
            string result = string.Format("Hello {0}", param1);
            return Ok(result);
        }

        [Route("api/Sample/DemoPost")]
        [HttpPost]
        public IHttpActionResult DemoPost(GradesViewModel vm)
        {
            SampleBL sampleBL = new SampleBL();
            vm = sampleBL.DemoPost(vm);
            return Ok(vm);
        }

        //[Route("api/Sample/GetMyLogin")]
        //[HttpPost]
        //public TokenModel SampleGetMyLogin(TokenModel vm)
        //{
           
        //    SampleBL sampleBL = new SampleBL();
        //    vm = sampleBL.GetMyLogin(vm);           
        //    return vm;
        //}

        [Route("api/Sample/GetMyLogin")]
        [HttpPost]
        public GradesViewModel SampleGetMyLogin(GradesViewModel vm)
        {
            SampleBL sampleBL = new SampleBL();
            vm = sampleBL.GetMyLogin(vm);
            return vm;
        }

        private void InsertError(string msg)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Error;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"); ;
            try { 
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO ErrorLog (Error) VALUES ('" + msg + "')",conn);
            cmd.ExecuteNonQuery();
                
            }catch(Exception ex)
            {
                throw ex;
            }finally
            {
                conn.Close();
            }
        }
    }
}
