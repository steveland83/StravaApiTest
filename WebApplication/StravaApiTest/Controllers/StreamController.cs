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

            var viewModel = new ActivityStreamsViewModel(activityId, streams);

            return View(viewModel);
        }

        public FileResult Export(long activityId)
        {
            var activity = db.Activities.FirstOrDefault(p=>p.Id == activityId);

            if(activity!=null)
            {
                var detailedActivity = activity.GetDetailedActivityInfo();

                var filestream = new System.IO.MemoryStream();
                var writer = new System.IO.StreamWriter(filestream);

                var csvWriter = new CsvHelper.CsvWriter(writer);
                csvWriter.WriteExcelSeparator();
                csvWriter.WriteRecords(detailedActivity.ActivityDataPoints);
                
                string fileName = string.Format("{0}_{1}.csv", detailedActivity.DateTimeStart, detailedActivity.Name);

                var file = File(filestream.ToArray(), "text/csv", fileName);
                
                return file;
            }
            return null;
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
