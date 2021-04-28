using Constants;
using Models;
using System;
using System.Collections.Generic;

namespace Base
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
