﻿using System;
using System.Collections.Generic;

namespace ServerUpload.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> GetAll(Func<T, bool> predicate);
        public T Get(int id);
        public T Find(Func<T, bool> predicate);
        public T Create(T item);
        public T Update(T item);
        public void Delete(int id);

        public void Save();
    }
}
