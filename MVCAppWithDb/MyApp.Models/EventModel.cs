using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class EventModel
    {
        [Key]
        public int EventId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime Date { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Display(Name = "Start Time")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Start Time is required")]
        public System.TimeSpan StartTime { get; set; }
        public string Type { get; set; }
        public Nullable<int> Duration { get; set; }
        public string Description { get; set; }
        public string OtherDetails { get; set; }
        public string InviteByEmail { get; set; }
        public string comments { get; set; }

        public string Comment { get; set; }
        public string UserEmailId { get; set; }
        [ForeignKey("Email")]
        public virtual UserModel User { get; set; }

    }
}
