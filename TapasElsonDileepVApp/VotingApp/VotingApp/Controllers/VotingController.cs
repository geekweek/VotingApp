using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VotingApp.Interfaces;
using VotingApp.Models;
using VotingApp.Factory;
using System.IO;
using VotingApp.Utilities;

namespace VotingApp.Controllers
{
    public class VotingController : Controller
    {
        IDataAccess dataAccsess = null;
        public VotingController()
        {
            dataAccsess = DataStoreFactory.GetDataStore();
        }

        public ActionResult Index()
        {
            MongoModel model = new MongoModel();
            model.ListOFVote = dataAccsess.RetrieveAll();
            return View(model);
        }

        public ActionResult Create()
        {
            VoteModels model = new VoteModels();
            return View(model);
        }

        public ActionResult Upload(CreateModel model, HttpPostedFileBase file)
        {
            dataAccsess.Insert(new VoteModels()
            {
                UserName = User.Identity.Name,
                Name = "elson paul",
                Rating = model.Rating,
                Description = "description",
                FileName = file.FileName,
                Data = ByteStreamConverter.StreamToByte(file.InputStream),
                ContentType = file.ContentType
            });
            return RedirectToAction("Index");
        }

        public FileContentResult Download(string id)
        {
            var vote = dataAccsess.Find(new VoteModels() { UserName = User.Identity.Name, Id = new Guid(id) });
            return new FileContentResult(vote.Data, vote.ContentType);
        }
    }
}
