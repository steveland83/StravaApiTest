using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace StravaApiTest.Api
{
    public class ActivityController : ApiController
    {
        // GET api/<controller>/5
        public string Get(int id)
        {
            return new Controllers.StreamController().StreamGraphData(id);
        }        
    }
}