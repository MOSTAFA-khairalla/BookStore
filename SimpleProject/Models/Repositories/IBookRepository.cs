﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleProject.Models.Repositories
{
   public interface IBookRepository<TEntity>
    {
        IList<TEntity> List();
        TEntity Find(int id);
        void add(TEntity entity);
        void Update(int id,TEntity entity);
        void Delete(int id);
        List<TEntity> Search(string term);

    }
}
