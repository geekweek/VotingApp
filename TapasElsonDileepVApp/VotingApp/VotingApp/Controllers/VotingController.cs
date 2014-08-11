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
    [Authorize]
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

        public ActionResult MyPresentations()
        {
            MongoModel model = new MongoModel();
            model.ListOFVote = dataAccsess.Retrieve(User.Identity.Name);
            return View("Index",model);
        }

        public ActionResult Create()
        {
            VoteModels model = new VoteModels();
            model.IsEditable = true;
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            VoteModels model = dataAccsess.Find(new VoteModels() { UserName = User.Identity.Name, Id = new Guid(id) });
            model.IsEditable = true;
            return View("Edit",model);
        }

        public ActionResult View(string id)
        {
            VoteModels model = dataAccsess.Find(new VoteModels() { UserName = User.Identity.Name, Id = new Guid(id) });
            model.IsEditable = false;
            return View("View", model);
        }

        public ActionResult Update(VoteModels model, HttpPostedFileBase file)
        {
            var vote = MapVoteModel(model, file);
            vote.Id = model.Id;
            dataAccsess.Update(vote);
            return RedirectToAction("Index");
        }

        public ActionResult Upload(VoteModels model, HttpPostedFileBase file)
        {
            dataAccsess.Insert(MapVoteModel(model,file));
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            dataAccsess.Delete(new VoteModels() { UserName = User.Identity.Name, Id = new Guid(id) });
            return RedirectToAction("Index");
        }

        public FileContentResult Download(string id)
        {
            var vote = dataAccsess.Find(new VoteModels() { UserName = User.Identity.Name, Id = new Guid(id) });
            return new FileContentResult(vote.Data, vote.ContentType);
        }

        VoteModels MapVoteModel(VoteModels model, HttpPostedFileBase file)
        {
            return new VoteModels()
            {
                UserName = User.Identity.Name,
                Name = model.Name,
                Rating = model.Rating,
                Description = model.Description,
                FileName = file != null ? file.FileName : null,
                Data = file != null ? ByteStreamConverter.StreamToByte(file.InputStream): null,
                ContentType = file != null ? file.ContentType : null
            };
        }
    }
}
