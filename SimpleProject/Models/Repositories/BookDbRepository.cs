using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleProject.Models.Repositories
{
    public class BookDbRepository : IBookRepository<Book>
    {
        private readonly BookStoreContext _db;
       
    public BookDbRepository(BookStoreContext db)
    {
            _db = db;
     }
    public void add(Book entity)
    {
      _db.Books.Add(entity);
      _db.SaveChanges();
    }

    public void Delete(int id)
    {
        var book = _db.Books.SingleOrDefault(a => a.ID == id);
        _db.Books.Remove(book);
        _db.SaveChanges();
    }

    public Book Find(int id)
    {
        var Book = _db.Books.Include(a => a.Author).SingleOrDefault(d => d.ID == id);
        return Book;
    }

    public IList<Book> List()
    {
        return _db.Books.Include(a=>a.Author).ToList();
    }

    public void Update(int id, Book newbook)
    {
            _db.Update(newbook);
            _db.SaveChanges();
    }

        public List<Book> Search(string term)
        {

            var result = _db.Books.Include(a => a.Author).Where(a => a.Title.Contains(term)
               || a.Description.Contains(term) || a.Author.FullName.Contains(term)
            ).ToList();
            return result;
        }
    }
}
