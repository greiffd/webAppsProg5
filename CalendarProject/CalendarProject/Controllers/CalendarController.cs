using CalendarProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalendarProject.Controllers
{
    public class CalendarController : Controller
    {

        private DatabaseDataContext context;
        public CalendarController()
        {
            context = new DatabaseDataContext();
        }

        // GET: Calendar
        public ActionResult Index()
        {
            IList<CalendarEvent> Events = new List<CalendarEvent>();

            var query = from Event in context.Events
                        select Event;

            var events = query.ToList();

            foreach (var e in events)
            {
                Events.Add(new CalendarEvent()
                {
                    ID = e.Id,
                    EventName = e.Event_Name,
                    EventDescription = e.Event_Description,
                    Date = e.Date
                });
            }
            return View(Events);
        }

        public ActionResult Create()
        {
            CalendarEvent model = new CalendarEvent();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CalendarEvent model)
        {
            try
            {
                Event e = new Event()
                {
                    //Id = model.ID,
                    Event_Name = model.EventName,
                    Event_Description = model.EventDescription,
                    Date = model.Date
                };

                context.Events.InsertOnSubmit(e);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            CalendarEvent model = context.Events.Where(x => x.Id == id).Select(x =>
                new CalendarEvent()
                {
                    ID = x.Id,
                    EventName = x.Event_Name,
                    EventDescription = x.Event_Description,
                    Date = x.Date
                }).SingleOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(CalendarEvent model)
        {
            try
            {
                Event e = context.Events.Where(x => x.Id == model.ID).Single<Event>();
                context.Events.DeleteOnSubmit(e);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            CalendarEvent model = context.Events.Where(x => x.Id == id).Select(x =>
                    new CalendarEvent()
                    {
                        ID = x.Id,
                        EventName = x.Event_Name,
                        EventDescription = x.Event_Description,
                        Date = x.Date
                    }).SingleOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CalendarEvent model)
        {
            try
            {
                Event e = context.Events.Where(x => x.Id == model.ID).Single<Event>();
                e.Event_Name = model.EventName;
                e.Event_Description = model.EventDescription;
                e.Date = model.Date;
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Details(int id)
        {
            CalendarEvent model = context.Events.Where(x => x.Id == id).Select(x =>
                    new CalendarEvent()
                    {
                        EventName = x.Event_Name,
                        EventDescription = x.Event_Description,
                        Date = x.Date
                    }).SingleOrDefault();
            return View(model);
        }
    }
}