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
using System.Web.Script.Serialization;

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

        private class GraphDataPoint
        {
            public string x { get; set; }
            public object y { get; set; }
            public object z { get; set; }
            public object r { get; set; }
        }

        private string TimeFormatter(double timeInSeconds)
        {
            var hours = Math.Floor(timeInSeconds / 3600);
            var minutes = Math.Floor((timeInSeconds - (hours * 3600)) / 60);
            var seconds = timeInSeconds - (hours * 3600) - (minutes * 60);

            var formattedTime = string.Format("{0:00}-{1:00}-{2:00}", hours, minutes, seconds);
            return formattedTime;
        }

        public string StreamGraphData(long activityId)
        {
            if (activityId == 0)
            {
                return null;
            }
            
            var timeStream = db.ActivityStreams.Where(p => p.ActivityId == activityId && p.StreamType == StreamType.Time).FirstOrDefault();
            var hrStream = db.ActivityStreams.Where(p => p.ActivityId == activityId && p.StreamType == StreamType.Heartrate).FirstOrDefault();
            var paceStream = db.ActivityStreams.Where(p => p.ActivityId == activityId && p.StreamType == StreamType.Velocity_Smooth).FirstOrDefault();
            var altitudeStream = db.ActivityStreams.Where(p => p.ActivityId == activityId && p.StreamType == StreamType.Altitude).FirstOrDefault();

            var smoothingPoints = 5;
            var totalPoints = timeStream.Data.Data.Count;
            var points = new List<GraphDataPoint>();
            for (int i = 0; i < totalPoints; i++)
            {
                if (i < 3000 /*&& i % 2 == 0*/)
                {
                    double smoothedPace = 0;
                    /*Velocity Smoothing*/
                    if (i > smoothingPoints && i < totalPoints - smoothingPoints - 1)
                    {
                        for (int x = i - smoothingPoints; x < i + smoothingPoints; x++)
                        {
                            smoothedPace += paceStream.Data.Data[x];
                        }
                        smoothedPace = smoothedPace / (smoothingPoints * 2 + 1);
                    }
                    else
                    {
                        smoothedPace = paceStream.Data.Data[i];
                    }
                    /*End Smoothing*/

                    if(hrStream!=null)
                        points.Add(new GraphDataPoint() { x = TimeFormatter(timeStream.Data.Data[i]), y = hrStream.Data.Data[i], z = PacePerKm(smoothedPace), r = altitudeStream.Data.Data[i] });
                    else
                        points.Add(new GraphDataPoint() { x = TimeFormatter(timeStream.Data.Data[i]), y = 0, z = PacePerKm(smoothedPace), r = altitudeStream.Data.Data[i] });
                }
            }

            var jsSerializer = new JavaScriptSerializer();
            string json = jsSerializer.Serialize(points);
            return json;
        }

        private object PacePerKm(double metersPerSecond)
        {
            if(metersPerSecond>0)
            {
                double secondsPerKm = 1000 / (metersPerSecond);
                return Math.Floor(secondsPerKm);
            }

            return 3600;
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
