using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template__.Domain.Interface
{
    public interface IRepository<T>
    {
        int Count();
        void Add(T entity);
        List<T> GetAll();

        T Find(Predicate<T> match);
        List<T> FindAll(Predicate<T> match);
        void Update(T entity);

        bool Exist(string _id);
        void Delete(string _id);
    }
}
