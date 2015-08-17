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
using StravaApiTest.ViewModels;

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
            var activities = db.Activities.Where(p => p.AthleteId == athleteId).OrderBy(p=>p.ActivityDate);
            return View(activities);
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

        public void OpenAllStreams(int athleteId, int activityId)
        {
            Response.Redirect(string.Format("~/Stream/AllStreams?athleteId={0}&activityId={1}", athleteId, activityId));
        }
        

        // GET: Activity/Details/5
        public ActionResult Details(long activityId)
        {
            if (activityId==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var activity = db.Activities.Where(p => p.Id == activityId).FirstOrDefault();
            return View(activity);
        }

        public PartialViewResult LoadActivityGraph(long activityId)
        {
            //var streams = db.ActivityStreams.Where(p => p.ActivityId == activityId).ToList();

            //var viewModel = new StreamGraphViewModel(activityId, streams);

            return PartialView("ActivityGraph2", activityId);
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
