using System;
using System.Collections.Generic;
using TechTuesdayFeedbackTool.Domain;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        public IList<T> Query()
        {
            throw new NotImplementedException();
        }

        public T Get(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Save(T data)
        {
            throw new NotImplementedException();
        }

        public bool Update(T data)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
