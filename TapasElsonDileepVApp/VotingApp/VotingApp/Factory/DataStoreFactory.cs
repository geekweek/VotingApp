using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VotingApp.Interfaces;
using VotingApp.DataStores.MongoDB;
namespace VotingApp.Factory
{
    public class DataStoreFactory
    {
        public static IDataAccess GetDataStore()
        {
            return new MongoDb("mongodb://localhost", "test");
        }
    }
}