using com.strava.api.Authentication;
using com.strava.api.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StravaApiTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public void GetAuthWebServiceCode()
        {
            string clientId = "4832";
            string postbackUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/Permission";
            
            var webAddr = String.Format("https://www.strava.com/oauth/authorize?client_id={0}&response_type=code&redirect_uri={1}", clientId, postbackUrl);
            
            Response.Redirect(webAddr);                        
        }
    }
}