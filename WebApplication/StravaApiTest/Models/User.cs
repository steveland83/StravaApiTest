using com.strava.api.Athletes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StravaApiTest.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        [Index(IsUnique = true)]
        [StringLength(60)]
        public string StravaAccessToken { get; set; }
        public string Email { get; set; }
        public DateTime? LastSyncDate { get; set; }

        public long AthleteId { get; set; }
        public virtual Athlete Athlete { get; set; }

        
    }
}