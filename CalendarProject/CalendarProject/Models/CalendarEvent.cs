using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalendarProject.Models
{
    public class CalendarEvent
    {
        public int ID { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}