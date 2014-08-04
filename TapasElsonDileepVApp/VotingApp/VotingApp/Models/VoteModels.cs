using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VotingApp.Models
{
    public class VoteModels
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Data { get; set; }
    }
}