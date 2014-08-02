using System.Collections.Generic;
using TechTuesdayFeedbackTool.Domain;

namespace TechTuesdayFeedbackTool.Repository
{
    public interface IRepository<T> where T : Entity
    {
        IList<T> Query();
        T Get(int ID);
        bool Save(T data);
        bool Update(T data);
        bool Delete(int ID);
    }
}
