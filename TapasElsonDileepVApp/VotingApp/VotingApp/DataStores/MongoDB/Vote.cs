using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace VotingApp.DataStores.MongoDB
{
    public class VoteEntity
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        public int Rating { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public ObjectId ImageId { get; set; }
    }
}