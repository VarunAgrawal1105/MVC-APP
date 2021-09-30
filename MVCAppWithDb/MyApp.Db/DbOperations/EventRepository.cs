using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Db.Model;
using MyApp.Models;
using System.Collections.ObjectModel;

namespace MyApp.Db.DbOperations
{
    public class EventRepository
    {
        public int AddEvent(EventModel model)
        {
            using(var context = new DatabaseContext())
            {

                Event eve = new Event()
                {
                    Title = model.Title,
                    Date = model.Date,
                    Location = model.Location,
                    StartTime = model.StartTime,
                    Type = model.Type,
                    Duration = model.Duration,
                    Description = model.Description,
                    OtherDetails = model.OtherDetails,
                    InviteByEmail = model.InviteByEmail,
                    comments = "",
                    UserEmailId = (string)HttpContext.Current.Session["UserEmailId"]
                };

                context.Event.Add(eve);
                context.SaveChanges();

                return eve.EventId;
            }
        }

        public List<EventModel> GetAllEents()
        {
            using (var context = new DatabaseContext())
            {
                var result = context.Event.Select(x => new EventModel()
                {
                    EventId = x.EventId,
                    Title = x.Title,
                    Date = x.Date,
                    Location = x.Location,
                    StartTime = x.StartTime,
                    Duration = x.Duration,
                    Description = x.Description,
                    OtherDetails = x.OtherDetails,
                    InviteByEmail = x.InviteByEmail,
                    comments = x.comments
                }).ToList();

                return result;
            }
        }

        public List<EventModel> GetAllUpcomingEvents()
        {
            using(var context = new DatabaseContext())
            {
                string email = (string)HttpContext.Current.Session["UserEmailId"];
                if (email == "myadmin@bookevents.com")
                {
                    var result = context.Event
                    .Where(x => x.Date >= DateTime.Now)
                    .Select(x => new EventModel()
                    {
                        EventId = x.EventId,
                        Title = x.Title,
                        Date = x.Date,
                        Location = x.Location,
                        StartTime = x.StartTime,
                        Duration = x.Duration,
                        Description = x.Description,
                        OtherDetails = x.OtherDetails,
                        InviteByEmail = x.InviteByEmail,
                        comments = x.comments
                    }).ToList();

                    return result;
                }
                else
                {
                    var result = context.Event
                    .Where(x => x.Date >= DateTime.Now && x.Type == "Public")
                    .Select(x => new EventModel()
                    {
                        EventId = x.EventId,
                        Title = x.Title,
                        Date = x.Date,
                        Location = x.Location,
                        StartTime = x.StartTime,
                        Duration = x.Duration,
                        Description = x.Description,
                        OtherDetails = x.OtherDetails,
                        InviteByEmail = x.InviteByEmail,
                        comments = x.comments
                    }).ToList();

                    return result;
                }
            }
        }

        public List<EventModel> GetAllPastEvents()
        {
            using (var context = new DatabaseContext())
            {
                string email = (string)HttpContext.Current.Session["UserEmailId"];
                if (email == "myadmin@bookevents.com")
                {
                    var result = context.Event
                    .Where(x => x.Date < DateTime.Now)
                    .Select(x => new EventModel()
                    {
                        EventId = x.EventId,
                        Title = x.Title,
                        Date = x.Date,
                        Location = x.Location,
                        StartTime = x.StartTime,
                        Duration = x.Duration,
                        Description = x.Description,
                        OtherDetails = x.OtherDetails,
                        InviteByEmail = x.InviteByEmail,
                        comments = x.comments
                    }).ToList();

                    return result;
                }
                else
                {
                    var result = context.Event
                        .Where(x => x.Date < DateTime.Now && x.Type == "Public")
                        .Select(x => new EventModel()
                        {
                            EventId = x.EventId,
                            Title = x.Title,
                            Date = x.Date,
                            Location = x.Location,
                            StartTime = x.StartTime,
                            Duration = x.Duration,
                            Description = x.Description,
                            OtherDetails = x.OtherDetails,
                            InviteByEmail = x.InviteByEmail,
                            comments = x.comments
                        }).ToList();

                    return result;
                }
            }
        }

        public List<EventModel> MyEvents()
        {
            using (var context = new DatabaseContext())
            {
                var temp = HttpContext.Current.Session["UserEmailId"].ToString();
                var result = context.Event
                    .Where(x => x.UserEmailId == temp )
                    .OrderBy(y => y.Date)
                    .Select(x => new EventModel()
                     {
                         EventId = x.EventId,
                         Title = x.Title,
                         Date = x.Date,
                         Location = x.Location,
                         StartTime = x.StartTime,
                         Duration = x.Duration,
                         Description = x.Description,
                         OtherDetails = x.OtherDetails,
                         InviteByEmail = x.InviteByEmail,
                         comments = x.comments
                    }).ToList();

                return result;

            }
        }

        public EventModel GetEvent(int id)
        {
            using (var context = new DatabaseContext())
            {
                var result = context.Event
                    .Where(x => x.EventId == id)
                    .Select(x => new EventModel()
                {
                    EventId = x.EventId,
                    Title = x.Title,
                    Date = x.Date,
                    Location = x.Location,
                    StartTime = x.StartTime,
                    Duration = x.Duration,
                    Description = x.Description,
                    OtherDetails = x.OtherDetails,
                    InviteByEmail = x.InviteByEmail,
                    comments = x.comments
                }).FirstOrDefault();

                return result;
            }
        }

        public bool UpdateEvent(int id, EventModel model) 
        {
            using (var context = new DatabaseContext())
            {
                var eve = context.Event.FirstOrDefault(x => x.EventId == id);

                if(eve != null)
                {
                    eve.Title = model.Title;
                    eve.Date = model.Date;
                    eve.Location = model.Location;
                    eve.StartTime = model.StartTime;
                    eve.Type = model.Type;
                    eve.Duration = model.Duration;
                    eve.Description = model.Description;
                    eve.OtherDetails = model.OtherDetails;
                    eve.InviteByEmail = model.InviteByEmail;
                }
                context.SaveChanges();
                return true;
            }
        }
        public void AddComment(int eventId, EventModel b)
        {
            using (var context = new DatabaseContext())
            {
                var eve = context.Event.Where(x => x.EventId == b.EventId).FirstOrDefault();
                if (eve != null)
                {
                    eve.comments = eve.comments + "," + b.Comment;
                    context.Entry(eve).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();

                }

            }
        }
    }
}
