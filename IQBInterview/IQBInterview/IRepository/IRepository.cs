using IQBInterview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQBInterview.IRepository
{
    public interface IRepository<T>
    {
        List<T> List();
        T Get(int id);
        ResultClass Delete(int id);
        ResultClass Update(T item, int id);
        ResultClass Add(T item);
    }
}
