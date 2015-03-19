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

namespace StravaApiTest.Controllers
{
    public class AthleteController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Sync
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Details
        public ActionResult Details(string accessToken)
        {
            var user = db.Users.Where(p => p.AccessToken == accessToken).FirstOrDefault();
            return View(user);
        }

        public void SyncAthleteDetails(string accessToken)
        {
            Engine.SyncManager.SyncAthlete(accessToken, db, true);

            Response.Redirect(string.Format("~/Athlete/Details?accessToken={0}", accessToken));
        }

        public void ClearAthleteActivities(string accessToken)
        {
            var user = db.Users.Where(p => p.AccessToken == accessToken).FirstOrDefault();
            System.Threading.Tasks.Task.Factory.StartNew(() => Engine.SyncManager.DeleteAthleteActivityData(user));
            //Engine.SyncManager.DeleteAthleteActivityData(user);
            Response.Redirect(string.Format("~/Athlete/Details?accessToken={0}", accessToken));
        }

        public void ViewActivities(string athleteId)
        {
            Response.Redirect(string.Format("~/Activity/Index?athleteId={0}", athleteId));
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
