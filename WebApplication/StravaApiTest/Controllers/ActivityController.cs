using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StravaApiTest.DAL;
using StravaApiTest.Models;
using com.strava.api.Activities;
using com.strava.api.Clients;
using com.strava.api.Streams;

namespace StravaApiTest.Controllers
{
    public class ActivityController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Activity
        public ActionResult Index(int athleteId)
        {
            if(athleteId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.FirstOrDefault(p => p.Id == athleteId);

            var auth = new com.strava.api.Authentication.StaticAuthentication(user.StravaAccessToken);
            var activityClient = new ActivityClient(auth);
            return View(activityClient.GetActivitiesBefore(DateTime.Now));
        }

        public void OpenDistanceStream(int? athleteId, int? activityId)
        {
            Response.Redirect(string.Format("~/Stream/Details?athleteId={0}&activityId={1}&streamType={2}", athleteId, activityId, StreamType.Distance.ToString()));
        }

        public void OpenHeartRateStream(int? athleteId, int? activityId)
        {
            Response.Redirect(string.Format("~/Stream/Details?athleteId={0}&activityId={1}&streamType={2}", athleteId, activityId, StreamType.Heartrate.ToString()));
        }

        public void OpenVelocityStream(int? athleteId, int? activityId)
        {
            Response.Redirect(string.Format("~/Stream/Details?athleteId={0}&activityId={1}&streamType={2}", athleteId, activityId, StreamType.Velocity_Smooth.ToString()));
        }

        

        // GET: Activity/Details/5
        public ActionResult Details(int athleteId, string activityId)
        {
            if (String.IsNullOrEmpty(activityId) || athleteId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = db.Users.FirstOrDefault(p => p.Id == athleteId);

            var auth = new com.strava.api.Authentication.StaticAuthentication(user.StravaAccessToken);
            var activityClient = new ActivityClient(auth);

            return View(activityClient.GetActivity(activityId, false));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
