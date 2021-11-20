using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleProject.Models.Repositories
{
    public class AuthorDbRepository:IBookRepository<Author>
    {
        private readonly BookStoreContext _db;

        public AuthorDbRepository( BookStoreContext db )
    {
           _db = db;
        }
    public void add(Author entity)
    {
       _db.Authors.Add(entity);
            _db.SaveChanges();
    }

    public void Delete(int id)
    {
        var author = _db.Authors.SingleOrDefault(a => a.ID == id);

        _db.Authors.Remove(author);
            _db.SaveChanges();
    }

    public Author Find(int id)
    {
        var author = _db.Authors.SingleOrDefault(a => a.ID == id);
        return author;
    }

    public IList<Author> List()
    {
        return _db.Authors.ToList();
    }

        public List<Author> Search(string term)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Author NewAuthors)
    {
            _db.Update(NewAuthors);
            _db.SaveChanges();
    }
}
}
