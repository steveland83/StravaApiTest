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
using StravaApiTest.ViewModels;

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
        public ActionResult Details(long? athleteId, long? activityId, StreamType streamType)
        {
            if (athleteId == null || activityId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var stream = db.ActivityStreams.Where(p=>p.ActivityId == activityId.Value && p.StreamType == streamType).FirstOrDefault();

            if (stream == null)
                stream = new StreamEntity(athleteId.Value, activityId.Value) { SeriesType = string.Format("No {0} stream found for this activity", streamType.ToString()) };

            return View(stream);
        }

        public ActionResult AllStreams(long athleteId, long activityId)
        {
            var streams = db.ActivityStreams.Where(p => p.ActivityId == activityId).ToList();

            var viewModel = new ActivityStreamsViewModel(streams);

            return View(viewModel);
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
