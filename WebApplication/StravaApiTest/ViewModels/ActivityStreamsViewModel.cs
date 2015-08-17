using com.strava.api.Streams;
using StravaApiTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace StravaApiTest.ViewModels
{
    public class ActivityStreamsViewModel
    {
        public string StreamDataAsHtmlTable { get; set; }

        public string TimeData { get; set; }
        public string PaceData { get; set; }
        public string TargetPaceData { get; set; }

        public long ActivityId { get; set; }

        public ActivityStreamsViewModel(long activityId, List<StreamEntity> streams)
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
                var tableRows = streamLength;
                var tableColumns = streams.Count();
                if (streamLength > 500)
                {
                    tableRows = 500;
                    sb.Append("<tr><td colspan=");
                    sb.Append(streams.Count());
                    sb.Append(" style='background:lightgray; text-align:center; font-weight:bold;'>Streams too large to display - limited to first 500 points.</td></tr>");
                }
                sb.Append("<tr>");
                for (int j = 0; j < tableColumns; j++)
                {
                    sb.Append(string.Format("<th>{0}</th>", streams[j].StreamType));
                }
                sb.Append("</tr>");
                for (int i = 0; i < tableRows; i++)
                {
                    sb.Append("<tr>");
                    for (int j = 0; j < tableColumns; j++)
                    {
                        sb.Append(string.Format("<td>{0}</td>", streams[j].Data.Data[i]));
                    }                    
                    sb.Append("</tr>");
                }
            }
            else
            {
                sb.Append("<tr><td colspan=");
                sb.Append(streams.Count());
                sb.Append(" style='background:lightgray; text-align:center; font-weight:bold;'>");
                sb.Append("No stream data found for this activity - this may be due to an ongoing background sync, please try again in a few minutes.");
                sb.Append("</td></tr>");
            }
            sb.Append("</table>");

            StreamDataAsHtmlTable = sb.ToString();

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