using com.strava.api.Streams;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StravaApiTest.Models
{
    public class ActivityEntity : com.strava.api.Activities.ActivitySummary
    {
        [Key]
        public int DatabaseId { get; set; } //Required so that we can insert AthleteMeta.Id into DB without drama
        public long AthleteId { get; set; } //Strava athlete Id (NOT DATABASE ID)
        public DateTime ActivityDate { get; set; } //api date fields are string, so cant be used in linq expressions.

        public DetailedActivity GetDetailedActivityInfo()
        {
            return new DetailedActivity(this);
        }
    }

    public class DetailedActivity : com.strava.api.Activities.ActivitySummary
    {
        private StravaApiTest.DAL.AppDbContext db = new StravaApiTest.DAL.AppDbContext();

        public IList<ActivityDataPoint> ActivityDataPoints { get; set; }

        public int NumDataPoints { get; set; }

        public DetailedActivity(ActivityEntity activityEntity)
        {
            AutoMapper.Mapper.CreateMap<ActivityEntity, DetailedActivity>();
            AutoMapper.Mapper.Map(activityEntity, this);

            var streams = db.ActivityStreams.Where(p => p.ActivityId == this.Id).ToList();
            NumDataPoints = streams[0].Data.Data.Count();
            ActivityDataPoints = new List<ActivityDataPoint>(NumDataPoints);
            
            for (var i = 0; i < NumDataPoints; i++) 
                ActivityDataPoints.Add(new ActivityDataPoint());

            foreach(var stream in streams)
            {
                switch(stream.StreamType)
                {
                    case StreamType.Altitude:
                        for (var i = 0; i < NumDataPoints; i++)
                            ActivityDataPoints[i].altitude = double.Parse(stream.Data.Data[i].ToString());
                        break;
                    case StreamType.Velocity_Smooth:
                        for (var i = 0; i < NumDataPoints; i++)
                            ActivityDataPoints[i].velocity_smooth = double.Parse(stream.Data.Data[i].ToString());
                        break;
                    case StreamType.Cadence:
                        for (var i = 0; i < NumDataPoints; i++)
                            ActivityDataPoints[i].cadence = int.Parse(stream.Data.Data[i].ToString());
                        break;
                    case StreamType.Distance:
                        for (var i = 0; i < NumDataPoints; i++)
                            ActivityDataPoints[i].distance = double.Parse(stream.Data.Data[i].ToString());
                        break;
                    case StreamType.GradeSmooth:
                        for (var i = 0; i < NumDataPoints; i++)
                            ActivityDataPoints[i].grade_smooth = double.Parse(stream.Data.Data[i].ToString());
                        break;
                    case StreamType.Heartrate:
                        for (var i = 0; i < NumDataPoints; i++)
                            ActivityDataPoints[i].heartrate = int.Parse(stream.Data.Data[i].ToString());
                        break;
                    case StreamType.LatLng:
                        for (var i = 0; i < NumDataPoints; i++)
                            ActivityDataPoints[i].latlng = stream.Data.Data[i].ToString();
                        break;
                    case StreamType.Moving:
                        for (var i = 0; i < NumDataPoints; i++)
                            ActivityDataPoints[i].moving = stream.Data.Data[i] == 1;
                        break;
                    case StreamType.Temperature:
                        for (var i = 0; i < NumDataPoints; i++)
                            ActivityDataPoints[i].temp = int.Parse(stream.Data.Data[i].ToString());
                        break;
                    case StreamType.Time:
                        for (var i = 0; i < NumDataPoints; i++)
                            ActivityDataPoints[i].time = int.Parse(stream.Data.Data[i].ToString());
                        break;
                }
            }
        }
    }

    public class ActivityDataPoint
    {
        public int time { get; set; }
        public string latlng { get; set; }
        public double distance { get; set; }
        public double altitude { get; set; }
        public double velocity_smooth { get; set; }
        public int heartrate { get; set; }
        public int cadence { get; set; }
        public int watts { get; set; }
        public int temp { get; set; }
        public bool moving { get; set; }
        public double grade_smooth { get; set; }

        public ActivityDataPoint() { }
    }
}