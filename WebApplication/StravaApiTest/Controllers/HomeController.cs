using com.strava.api.Authentication;
using com.strava.api.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StravaApiTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.QueryString.HasKeys() && !String.IsNullOrEmpty(Request.QueryString["code"]))
            {
                StaticAuthentication staticAuth = new StaticAuthentication(Request.QueryString["code"]);
                StravaClient steviekins = new StravaClient(staticAuth);

                var athlete = steviekins.Athletes.GetAthlete();

                ViewBag.Authorised = "Authorised!!!   " + string.Format("{0} {1}", athlete.FirstName, athlete.LastName);; ;
            }
            else
            {
                ViewBag.Authorised = "Not Authorised";
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string TestAuthorisation(){
            StaticAuthentication staticAuth = new StaticAuthentication("d8a4b85f2511530f93b8574ed64697557f85eeea");
            StravaClient steviekins = new StravaClient(staticAuth);

            return steviekins.Activities.GetTotalActivityCount().ToString();
        }

        public void AuthoriseStrava()
        {
            WebAuthentication auth = new WebAuthentication();
            auth.GetTokenAsync("4832", "8a5369c4685ac2380321b9d1e0faa001d37d8502", Scope.Full, 50689);

            auth.AccessTokenReceived +=
                delegate
                {
                    string shittyfuckballs = "yeahBaby";
                };
        }
    }
}