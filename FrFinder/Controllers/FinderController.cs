using System;
using System.Text;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Data.Entity;
using System.Web.Security;//Giriş Kontrolü Kütüphanesi
using FrFinder.Models;


namespace FrFinder.Controllers
{
    public class FinderController : Controller
    {
        Ffinder_Database db = new Ffinder_Database();
     
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Getreturn(int ID)
        {
            List<User> users = db.users.ToList();
            User user = users.FirstOrDefault(x => x.Id == ID);

            return Json(user, JsonRequestBehavior.AllowGet);
        }

            [HttpPost]
        public JsonResult GetID(int ID)
        {
            List<User> users = db.users.ToList();
            User user = users.FirstOrDefault(x => x.Id == ID && x.Id!= Convert.ToInt32(Session["UserId"]));

            MatchRequest match = new MatchRequest();
            Match matches = new Match();
            var authuser = db.matchRequests.Where(x => x.SenderId == ID).FirstOrDefault();
          



            if (authuser == null)
                {
                    match.SenderId = Convert.ToInt32(Session["UserId"]);
                    match.ReceiverId = Convert.ToInt32(ID);
                    match.Status = false;

                    db.matchRequests.Add(match);
                    db.SaveChanges();
                }
                else
                {
                    matches.SenderId = Convert.ToInt32(Session["UserId"]);
                    matches.ReceiverId = Convert.ToInt32(ID);
                    matches.Status = false;
                    matches.MatchDate = DateTime.Now;

                    db.matches.Add(matches);
                    db.SaveChanges();

                }
            
           
           

            return Json(user, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Finder()
        {
          
            return View();
        }
        [HttpPost]
        public ActionResult Finder(string receiver)
        {
            MatchRequest match = new MatchRequest();

            match.SenderId = Convert.ToInt32(Session["UserId"]);
            match.ReceiverId = Convert.ToInt32(receiver);
            match.Status = false;

            db.matchRequests.Add(match);
            db.SaveChanges();


            return View();
        }
       

    }
}