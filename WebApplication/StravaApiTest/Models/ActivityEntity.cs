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
    }
}