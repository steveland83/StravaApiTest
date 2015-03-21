using com.strava.api.Streams;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StravaApiTest.Models
{
    public class StreamEntity
    {
        [Key]
        [Column(Order = 1)]
        public long AthleteId { get; set; }
        [Key]
        [Column(Order = 2)]
        public long ActivityId { get; set; }
        [Key]
        [Column(Order = 3)]
        public StreamType StreamType{ get; set; }

        public StreamData Data { get; set; }

        public String SeriesType { get; set; }

        public int OriginalSize { get; set; }

        public string Resolution { get; set; }

        public StreamEntity()
        {
            Data = new StreamData();
        }

        public StreamEntity(long athleteId, long activityId)
        {
            AthleteId = athleteId;
            ActivityId = activityId;
            Data = new StreamData();
        }
    }

    [Serializable]
    public class StreamData
    {
        public string SerialisedData { get; private set; }
        public List<object> Data
        {
            get
            {
                if (string.IsNullOrEmpty(SerialisedData))
                    return new List<object>();

                return System.Web.Helpers.Json.Decode(SerialisedData, typeof(List<object>));
            }
            set
            {
                SerialisedData = System.Web.Helpers.Json.Encode(value);
            }
        }
    }
}