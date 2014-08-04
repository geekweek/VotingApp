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
        private MongoDatabase database = null;
        public MongoDb(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            database = server.GetDatabase(databaseName);
            
        }
        public bool Insert(VoteModels vote)
        {
            var collection = database.GetCollection<VoteEntity>(vote.UserName);
            VoteEntity voteEntity = new VoteEntity()
            {
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

        public List<VoteModels> RetrieveAll(string userName)
        {
            var collection = database.GetCollection<VoteEntity>(userName);
            if (collection == null || collection.Count() <= 0)
            {
                return null;
            }
            List<VoteModels> voteList = new List<VoteModels>();
            foreach (VoteEntity vote in collection.FindAll())
            {
                voteList.Add(MapEntityToModel(vote, userName));
                
            }
            return voteList;
        }

        public bool Update(VoteModels vote)
        {
            var collection = database.GetCollection<VoteEntity>(vote.UserName);
            var query = Query<VoteEntity>.EQ(e => e.Id, vote.Id);
            var voteEntity = collection.FindOne(query);
            VoteEntity updateVoteEntity = new VoteEntity()
            {
                Id = vote.Id,
                Rating = vote.Rating > 0 ? vote.Rating : voteEntity.Rating,
                Name = !string.IsNullOrWhiteSpace(vote.Name) ? vote.Name : voteEntity.Name,
                Description = !string.IsNullOrWhiteSpace(vote.Description) ? vote.Description : voteEntity.Description,
                FileName = !string.IsNullOrWhiteSpace(vote.FileName) ? vote.FileName : voteEntity.FileName,
                ImageId = vote.Data != null ? SaveImageInGridFS(vote) : voteEntity.ImageId,
                ContentType = !string.IsNullOrWhiteSpace(vote.ContentType) != null ? vote.ContentType : voteEntity.ContentType
            };
            collection.Save(updateVoteEntity);
            return true;
        }

        public bool Delete(VoteModels vote)
        {
            var collection = database.GetCollection<VoteEntity>(vote.UserName);
            var query = Query<VoteEntity>.EQ(e => e.Id, vote.Id);
            collection.Remove(query);
            return true;
        }

        public VoteModels Find(VoteModels vote)
        {
            var collection = database.GetCollection<VoteEntity>(vote.UserName);
            var query = Query<VoteEntity>.EQ(e => e.Id, vote.Id);
            var voteEntity = collection.FindOne(query);           
            return MapEntityToModel(voteEntity, vote.UserName);
        }

        VoteModels MapEntityToModel(VoteEntity voteEntity, string userName)
        {
            VoteModels voteModel = new VoteModels()
            {
                UserName = userName,
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