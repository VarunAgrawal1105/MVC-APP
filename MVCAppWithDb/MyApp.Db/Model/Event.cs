//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyApp.Db.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public System.DateTime Date { get; set; }
        public string Location { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public Nullable<int> Duration { get; set; }
        public string Description { get; set; }
        public string OtherDetails { get; set; }
        public string InviteByEmail { get; set; }
        public string UserEmailId { get; set; }
        public string Type { get; set; }
        public string comments { get; set; }
    
        public virtual User User { get; set; }
    }
}
