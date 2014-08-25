using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accolite.GeekWeek.Voting.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace Accolite.GeekWeek.Voting.DataLayer
{
    public static class DataProvider
    {
        public static string ConnectionString = string.Empty;
        public static MongoServer Server = null;
        public static MongoDatabase MyVotingDB = null;



        public static void InitMethod()
        {
            ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            Server = MongoServer.Create(ConnectionString);
            MyVotingDB = Server.GetDatabase("Voting1");

        }
        
        public static void CreatePresentations(PresentationDetails NewPresentation)
        {
            InitMethod();
            var _Id = GetNextPresentationID();
            MongoCollection<BsonDocument> presentation = MyVotingDB.GetCollection<BsonDocument>("Presentation");

            BsonDocument newPresentation = new BsonDocument {
                { "_id", _Id },
                { "Title", NewPresentation.Title },
                { "Presenter", NewPresentation.Presenter },
                { "AvgRating", 0 },
                { "Status", "Completed" },
                { "PresentationDate", NewPresentation.PresentationDate }
                };

            presentation.Insert(newPresentation);


            //inserting of Attachment shd be done

        }

        public static List<Presentation> GetAllPresentations()
        {
            List<Presentation> presentations = new List<Presentation>();
            InitMethod();

            var collection = MyVotingDB.GetCollection<Presentation>("Presentation");
            presentations = collection.FindAllAs<Presentation>().ToList();

            return presentations;
        }

        public static void AddNewRating(Rating RatingDetails)
        {
            InitMethod();
            var _Id = GetNextUserRatingID();
            MongoCollection<BsonDocument> rating = MyVotingDB.GetCollection<BsonDocument>("UserRating");

            BsonDocument newrating = new BsonDocument {
                { "_id", _Id },
                { "PresentationID", RatingDetails.ForPresentationID },
                { "Username", RatingDetails.UserName },
                { "Rating", RatingDetails.Ratingpoint },
                { "Comments", RatingDetails.Comments }
                };
            rating.Insert(newrating);



            //Update Avg Rating
            //var Presentationcollection = MyVotingDB.GetCollection<Presentation>("Presentation");
            //var selectedPresentation = Presentationcollection.FindOneAs<Presentation>(Query.EQ("_Id", RatingDetails.ForPresentationID));
            //var avg = (selectedPresentation.AvgRating + RatingDetails.Ratingpoint) / 2;

            //var query = new QueryDocument("_Id", RatingDetails.ForPresentationID);
            //var update = Update.Set("AvgRating", avg);


            //Presentationcollection.Update(query, update, UpdateFlags.Upsert, SafeMode.False);            


        }

        public static int GetNextPresentationID()
        {
            Presentation LastPresentation = new Presentation();
            var collection = MyVotingDB.GetCollection<Presentation>("Presentation");
            LastPresentation = collection.FindAllAs<Presentation>().ToList().Last();
            return (LastPresentation._id) + 1;
        }

        public static int GetNextUserRatingID()
        {
            UserRating LastUser = new UserRating();
            var collection = MyVotingDB.GetCollection<UserRating>("UserRating");
            LastUser = collection.FindAllAs<UserRating>().ToList().Last();
            return (LastUser._id) + 1;
        }


       

    }
}