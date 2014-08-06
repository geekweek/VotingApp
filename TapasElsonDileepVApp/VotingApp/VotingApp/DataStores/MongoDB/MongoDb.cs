using System.Collections.Generic;
using VotingApp.Interfaces;
using VotingApp.Models;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Builders;
using System.IO;
using MongoDB.Bson;

namespace VotingApp.DataStores.MongoDB
{
    public class MongoDb : IDataAccess
    {
        private MongoCollection<VoteEntity> collection = null;
        private MongoDatabase database = null;
        public MongoDb(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            database = server.GetDatabase(databaseName);
            collection = database.GetCollection<VoteEntity>(collectionName);
        }
        public bool Insert(VoteModels vote)
        {
            VoteEntity voteEntity = new VoteEntity()
            {
                UserName = vote.UserName,
                Rating = vote.Rating,
                Name = vote.Name,
                Description = vote.Description,
                FileName = vote.FileName,
                ImageId = SaveImageInGridFS(vote),
                ContentType = vote.ContentType
            };
            collection.Insert(voteEntity);
            return true;
        }

        public List<VoteModels> Retrieve(string userName)
        {
            List<VoteModels> voteList = new List<VoteModels>();
            var query = Query<VoteEntity>.EQ(e => e.UserName, userName);
            foreach (VoteEntity vote in collection.Find(query))
            {
                voteList.Add(MapEntityToModel(vote));
                
            }
            return voteList;
        }

        public bool Update(VoteModels vote)
        {
            var query = Query<VoteEntity>.EQ(e => e.Id, vote.Id);
            var voteEntity = collection.FindOne(query);
            VoteEntity updateVoteEntity = new VoteEntity()
            {
                Id = vote.Id,
                UserName = vote.UserName,
                Rating = vote.Rating > 0 ? vote.Rating : voteEntity.Rating,
                Name = !string.IsNullOrWhiteSpace(vote.Name) ? vote.Name : voteEntity.Name,
                Description = !string.IsNullOrWhiteSpace(vote.Description) ? vote.Description : voteEntity.Description,
                FileName = !string.IsNullOrWhiteSpace(vote.FileName) ? vote.FileName : voteEntity.FileName,
                ImageId = vote.Data != null ? SaveImageInGridFS(vote) : voteEntity.ImageId,
                ContentType = !string.IsNullOrWhiteSpace(vote.ContentType)? vote.ContentType : voteEntity.ContentType
            };
            collection.Save(updateVoteEntity);
            return true;
        }

        public bool Delete(VoteModels vote)
        {
            var query = Query<VoteEntity>.EQ(e => e.Id, vote.Id);
            var voteEntity = collection.FindOne(query);
            collection.Remove(query);
            database.GridFS.DeleteById(voteEntity.ImageId);
            return true;
        }

        public VoteModels Find(VoteModels vote)
        {
            var query = Query<VoteEntity>.EQ(e => e.Id, vote.Id);
            var voteEntity = collection.FindOne(query);           
            return MapEntityToModel(voteEntity);
        }

        public List<VoteModels> RetrieveAll()
        {
            List<VoteModels> voteList = new List<VoteModels>();
            foreach (VoteEntity vote in collection.FindAll())
            {
                voteList.Add(MapEntityToModel(vote));

            }
            return voteList;
        }

        VoteModels MapEntityToModel(VoteEntity voteEntity)
        {
            VoteModels voteModel = new VoteModels()
            {
                UserName = voteEntity.UserName,
                Data = GetImageFromGridFS(voteEntity.ImageId),
                Rating = voteEntity.Rating,
                Id = voteEntity.Id,
                Description = voteEntity.Description,
                FileName = voteEntity.FileName,
                Name = voteEntity.Name,
                ContentType = voteEntity.ContentType
            };
            return voteModel;
        }

        byte[] GetImageFromGridFS(ObjectId id)
        {
            byte[] bytes;
            MongoGridFSFileInfo file = database.GridFS.FindOne(Query.EQ("_id", id));
            using (var stream = file.OpenRead())
            {
                bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
            }
            return bytes;
        }

        ObjectId SaveImageInGridFS(VoteModels vote)
        {
            Stream memoryStream = new MemoryStream(vote.Data);
            MongoGridFSFileInfo gfsi = database.GridFS.Upload(memoryStream, vote.UserName);
            return gfsi.Id.AsObjectId;
        }

    }


}