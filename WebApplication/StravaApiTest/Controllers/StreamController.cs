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
using com.strava.api.Clients;
using com.strava.api.Streams;

namespace StravaApiTest.Controllers
{
    public class StreamController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Stream
        //public ActionResult Index()
        //{
        //    return View(db.StreamWrappers.ToList());
        //}

        // GET: Stream/Details/5
        public ActionResult Details(int? athleteId, int? activityId, StreamType streamType)
        {
            if (athleteId == null || activityId == null || streamType == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.FirstOrDefault(p => p.Id == athleteId);
            var auth = new com.strava.api.Authentication.StaticAuthentication(user.StravaAccessToken);
            var streamClient = new StreamClient(auth);

            var streamList = streamClient.GetActivityStream(activityId.Value.ToString(), streamType);

            if(streamType == StreamType.Distance)
            {
                return View(streamList[0]);
            }
            if(streamType != StreamType.Distance && streamList.Count==2)
            {
                return View(streamList[1]);
            }

            return View(new ActivityStream() { SeriesType = string.Format("No {0} stream found for this activity", streamType.ToString()), Data = new List<object>() });
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
