using com.strava.api.Athletes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StravaApiTest.Models
{
    public class UserEntity : com.strava.api.Athletes.Athlete
    {
        [Key]
        public int DatabaseId { get; set; }//Required so that we can insert AthleteMeta.Id into DB without drama
        public string AccessToken { get; set; }
        public DateTime? LastSyncDate { get; set; }        
    }
}