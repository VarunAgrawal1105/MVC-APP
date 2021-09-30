 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using MyApp.Db.DbOperations;
using MyApp.Db.Model;

namespace MVCAppWithDb.Controllers
{
    
    public class HomeController : Controller
    {
        EventRepository repository = null;
        public HomeController()
        {
            repository = new EventRepository();
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            //Session["UserEmailId"] = null;
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult Create(EventModel model)
        {
            if (ModelState.IsValid)
            {
                //model.UserEmailId = HttpContext.Session["UserEmailId"].ToString();
                int id = repository.AddEvent(model);
                if (id > 0)
                {
                    ModelState.Clear();
                    //ViewBag.IsSuccess = "Data Added";
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
            return RedirectToAction("GetAllRecords", new { id = model.EventId });
        }

        public ActionResult GetAllRecords()
        {
            var result = repository.GetAllEents();
            return View(result);
        }
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var eve = repository.GetEvent(id);
            return View(eve);
        }

        public ActionResult Edit(int id)
        {
            var eve = repository.GetEvent(id);
            return View(eve);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Edit(EventModel model)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateEvent(model.EventId, model);
                return RedirectToAction("GetAllRecords");
            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult UpcomingEvents()
        {
            var result = repository.GetAllUpcomingEvents();
            return View(result);
        }
        [AllowAnonymous]
        public ActionResult PastEvents()
        {
            var result = repository.GetAllPastEvents();
            return View(result);
        }
        [HttpGet]
        public ActionResult MyEvents()
        {
            var result = repository.MyEvents();
            return View(result);
        }

        //public ActionResult EventsInvitedTo()
        //{
        //    repository.EventsInvitedTo();
        //    return View();
        //}
        public ActionResult InvitedTo()
        {
            using (var db = new DatabaseContext())
            {
                return View(db.Event.ToList());
            }
        }

        [HttpPost]
        public ActionResult Comment(EventModel b)
        {
            try
            {
                repository.AddComment(b.EventId, b);
                return RedirectToAction("Details", new { id = b.EventId });

            }
            catch
            {
                return View("Index", "Home");
            }
        }
        [AllowAnonymous]
        public ActionResult HelpDesk()
        {
            return Redirect("https://www.nagarro.com/en/contact-us");
        }
    }
}