using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VotingApp.Models;

namespace VotingApp.Interfaces
{
    public interface IDataAccess
    {
        bool Insert(VoteModels vote);

        List<VoteModels> RetrieveAll(string userName);

        bool Update(VoteModels vote);

        bool Delete(VoteModels vote);

        VoteModels Find(VoteModels vote);

    }
}