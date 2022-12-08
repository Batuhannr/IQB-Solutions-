using IQBInterview.Context;
using IQBInterview.IRepository;
using IQBInterview.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IQBInterview.Repository
{
    public class BaseRepository<T> : IRepository<T>
        where T : EnBaseEntity, new()
    {
        private readonly IQBDbContext _context;
        ResultClass resultClas;
        public BaseRepository(IQBDbContext context)
        {
            _context = context;
            resultClas = new ResultClass();
        }

        public ResultClass Add(T item)
        {
            resultClas = new ResultClass();
            DbSet<T> dbSet = _context.Set<T>();
            dbSet.Add(item);
            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            //int result = 0;
            int result = _context.SaveChanges();
            if (result >= 1)
            {
                resultClas.Result = true;
                resultClas.ResultMessages = new List<string>()
                {
                    "added"
                };
                resultClas.ResultObject = item;

            }
            else
            {
                resultClas.Result = false;
                resultClas.ResultMessages = new List<string>()
                {
                    "Error"
                };
                resultClas.ResultObject = item;
            }
            return resultClas;


        }

        public ResultClass Delete(int id)
        {
            resultClas = new ResultClass();
            T entity = _context.Set<T>().FirstOrDefault(s => s.Id == id);

            DbSet<T> dbSet = _context.Set<T>();
            dbSet.Remove(entity);
            int saveChanges = _context.SaveChanges();
            if (saveChanges >= 1)
            {
                resultClas.Result = true;
                resultClas.ResultMessages = new List<string>()
                {
                    "Remove Succes"
                };
                resultClas.ResultObject = entity;
            }
            else
            {
                resultClas.Result = false;
                resultClas.ResultMessages = new List<string>()
                {
                    "Error"
                };
                resultClas.ResultObject = null;
            }
            return resultClas;
        }

        public T Get(int id)
        {
            T entity = _context.Set<T>().FirstOrDefault(s => s.Id == id);
            return entity;
            
        }

        public List<T> List()
        {
            List<T> entity = _context.Set<T>().ToList();
            return entity;
        }

        public ResultClass Update(T item, int id) 
        {
            DbSet<T> dbSet = _context.Set<T>();
            T entity = dbSet.Find(id);
            _context.Entry(entity).CurrentValues.SetValues(item);
            int resultBool = _context.SaveChanges();
            ResultClass result = new ResultClass();
            if (resultBool == 1)
            {
                result.Result = true;
                result.ResultMessages = new List<string>()
                {
                    "Entity Updated"
                };
                result.ResultObject = item;
                return result;
            }
            else
            {
                result.Result = false;
                result.ResultMessages = new List<string>()
                {
                    "Error"
                };
                result.ResultObject = entity;
                return result;
            }
        }
    }
}