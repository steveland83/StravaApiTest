using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StravaApiTest.Controllers.Strava
{
    public class ApplicationRegistrationController : Controller
    {
        // GET: ApplicationRegistration
        public ActionResult Register()
        {
            return View();
        }
    }
}