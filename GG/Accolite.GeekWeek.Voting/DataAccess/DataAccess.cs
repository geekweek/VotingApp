using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accolite.GeekWeek.Voting.Models;
using MongoDB.Driver;

namespace Accolite.GeekWeek.Voting.DataLayer
{
    public class DataAccess
    {
        public void GetPresentationsWithinPeriod(string PresentationStatus, ref List<PresentationDetails> Result, DateTime? StartDate = null, DateTime? EndDate = null)
        {
            var PresentationDetails = new List<PresentationDetails>();
            //Initialize to be deleted
            //StartDate = DateTime.Parse("2014-07-08");//this s -30days from the date selected
            //EndDate = DateTime.Now;

           var ListOfPresentation = DataProvider.GetAllPresentations();
           
           //get details from presentationdetails table - attachement and 

           MapPresentationViewmodel(ref ListOfPresentation, ref PresentationDetails, PresentationStatus);

           if (string.Equals(PresentationStatus, "Completed", StringComparison.OrdinalIgnoreCase))
           {
               if (PresentationDetails.Exists(item => item.PresentationDate >= StartDate && item.PresentationDate <= EndDate))
               {
                   Result = PresentationDetails.FindAll(item => item.PresentationDate >= StartDate && item.PresentationDate <= EndDate).OrderBy(item => item.AverageRating).ToList();
                   Result.Reverse();
               }
           }

           else if (string.Equals(PresentationStatus, "Upcoming", StringComparison.OrdinalIgnoreCase))
           {
               if (PresentationDetails.Exists(item => item.PresentationDate >= DateTime.Now))
               {
                   Result = PresentationDetails.FindAll(item => item.PresentationDate >= DateTime.Now);
               }
           }
        }

        public void AddNewPresentations(PresentationDetails NewPresentation)
        {
            DataProvider.CreatePresentations(NewPresentation);
        }

        public void MapPresentationViewmodel(ref List<Presentation> ListOfPresentation, ref List<PresentationDetails> PresentationDetails, string PresentationStatus)
        {
            foreach (var item in ListOfPresentation)
            {
                if (string.Equals(item.Status, PresentationStatus, StringComparison.OrdinalIgnoreCase))
                {
                    var presentationItem = new PresentationDetails();
                    presentationItem._id = item._id;
                    presentationItem.Title = item.Title;
                    presentationItem.Presenter = item.Presenter;
                    presentationItem.AverageRating = item.AvgRating;
                    presentationItem.UserName = item.UserName;
                    //storing logged-in user details in the session - placeholder for "" is the username form the session object
                    presentationItem.IsEditable = string.Equals(item.UserName, "", StringComparison.OrdinalIgnoreCase) ? true : false;
                    presentationItem.PresentationDate = item.PresentationDate;
                    PresentationDetails.Add(presentationItem);
                }

            }
        }

        public void AddNewUserRating(Rating RatingData)
        {
            DataProvider.AddNewRating(RatingData);
        }
    }
}