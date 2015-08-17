using com.strava.api.Streams;
using StravaApiTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StravaApiTest.ViewModels
{
    public class StreamGraphViewModel
    {
        public string TimeData { get; set; }
        public string PaceData { get; set; }
        public string TargetPaceData { get; set; }

        public long ActivityId { get; set; }

        public StreamGraphViewModel(long activityId, List<StreamEntity> streams)
        {
            ActivityId = activityId;
            var rand = new Random();
            var sb = new System.Text.StringBuilder();
            var sbTime = new System.Text.StringBuilder();
            var sbPace = new System.Text.StringBuilder();
            var sbTargetPace = new System.Text.StringBuilder();

            sb.Append("<table class='table-bordered'>");

            if (streams.Count() > 0)
            {
                var streamLength = streams[0].Data.Data.Count();

                for (int j = 0; j < streams.Count; j++)
                {
                    if (streams[j].StreamType == StreamType.Time || streams[j].StreamType == StreamType.Velocity_Smooth)
                    {
                        for (int i = 0; i < streams[j].Data.Data.Count; i++)
                        {
                            if (i < 50000 && i % 2 == 0)
                            {
                                if (streams[j].StreamType == StreamType.Time)
                                {
                                    sbTime.Append(streams[j].Data.Data[i] + ", ");
                                }
                                if (streams[j].StreamType == StreamType.Velocity_Smooth)
                                {
                                    sbPace.Append(streams[j].Data.Data[i].ToString() + ", ");
                                    var screwedValue = double.Parse(streams[j].Data.Data[i].ToString());
                                    screwedValue += rand.Next(2) - 1;
                                    sbTargetPace.Append(screwedValue + ", ");
                                }
                            }
                        }
                    }                    
                }
            }

            TimeData = sbTime.ToString();
            PaceData = sbPace.ToString();
            TargetPaceData = sbTargetPace.ToString();

            if (TimeData.Length > 2)
            {
                TimeData = TimeData.Remove(TimeData.Length - 2);
            }
            if (PaceData.Length > 2)
            {
                PaceData = PaceData.Remove(PaceData.Length - 2);
            }
            if (TargetPaceData.Length > 2)
            {
                TargetPaceData = TargetPaceData.Remove(TargetPaceData.Length - 2);
            }
        }        
    }
}