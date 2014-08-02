using System;
using System.Collections.Generic;
using TechTuesdayFeedbackTool.Domain;
using System.Data.Entity;
using System.Linq;

namespace TechTuesdayFeedbackTool.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        DatabaseContext context;

        public Repository()
        {
            context = new DatabaseContext();
        }
        public IList<T> Query()
        {
            return context.Set<T>().Select(o => o).ToList();
        }

        public T Get(int id)
        {
            var data = context.Set<T>().Select(o => o.ID == id);
            return data == null ? default(T) : data as T;
        }

        public bool Save(T data)
        {
            try
            {
                context.Set<T>().Add(data);
                var rowsAffected = context.SaveChanges();
                context.Dispose();
                return rowsAffected > 0;
            }
            catch(Exception e)
            {
                return false;
            }
            
        }

        public bool Update(T data)
        {
            try
            {
                //var oldvalue = context.Set<T>().Select(o => o.ID == data.ID).FirstOrDefault();
                //oldvalue = data;
                //var rowsAffected = context.SaveChanges();
                //context.Dispose();
                //return rowsAffected > 0;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
