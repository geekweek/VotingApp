using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accolite.GeekWeek.Voting.Models
{
    public class VotingModel
    {
        public ViewPresentations ViewPresentationsDetails;
        public UpcomingPresentation UpcomingPresentationDetails;
        public UserDetails UserData;

        public PresentationDetails SelectedPresentation;
        public Rating RatingData;
        public PresentationDetails NewPresentation;

        public VotingModel()
        {
            ViewPresentationsDetails = new ViewPresentations();
            UpcomingPresentationDetails = new UpcomingPresentation();
            UserData = new UserDetails();
            SelectedPresentation = new PresentationDetails();
            RatingData = new Rating();
            NewPresentation = new PresentationDetails();
        }        
    }

    public class ViewPresentations
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<PresentationDetails> SearchedPresentationDetails { get; set; }

        public ViewPresentations()
        {
            SearchedPresentationDetails = new List<PresentationDetails>();
        }
    }

    public class PresentationDetails
    {
        public int _id { get; set; } 
        public string Title { get; set; }
        public string Presenter { get; set; }
        public int AverageRating { get; set; }
        public bool IsEditable { get; set; }
        public string UserName { get; set; }
        public DateTime PresentationDate { get; set; }
        public string ATtachementPath { get; set; }
    }


    public class NewPresentation
    {
        public DateTime PresentationDate { get; set; }
        public string Title { get; set; }
        public string Presenter { get; set; }
        public string AttachmentPath { get; set; }
        public string UserName { get; set; }
    }

    public class UpcomingPresentation
    {
        public List<PresentationDetails> UpcomingSessions { get; set; }
    }

    public class Rating
    {
        public int _id;
        public string UserName { get; set; }
        public int Ratingpoint { get; set; }
        public string Comments { get; set; }
        public string ForPresentationID { get; set; }
    }
}