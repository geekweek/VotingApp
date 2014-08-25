using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accolite.GeekWeek.Voting.Models
{
    public class DataModel
    {
        
    }

    public class Presentation
    {
        public int _id { get; set; }
        public string Title { get; set; }
        public string Presenter { get; set; }
        public int AvgRating { get; set; }
        public string Status { get; set; }
        public DateTime PresentationDate { get; set; }
        public string UserName { get; set; }

    }

    public class UserRating
    {
        public int _id { get; set; }
        public int PresentationID { get; set; }
        public string Username { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }

    }
}