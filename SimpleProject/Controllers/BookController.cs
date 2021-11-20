using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleProject.Models;
using SimpleProject.Models.Repositories;
using SimpleProject.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleProject.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository<Book> _bookRepository;
        private readonly IBookRepository<Author> _authorRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BookController(IBookRepository<Book> bookRepository
            , IBookRepository<Author> authorRepository, IHostingEnvironment hostingEnvironment)
        {
          _bookRepository = bookRepository;
            _authorRepository = authorRepository;
           _hostingEnvironment = hostingEnvironment;
        }

        // GET: BookController
        public ActionResult Index()
        {
            var book = _bookRepository.List();
            return View(book);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = _bookRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = _authorRepository.List().ToList()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            try
            {
                string fileName = string.Empty;
                if (model.file !=null)
                {
                    string upload = Path.Combine(_hostingEnvironment.WebRootPath,"upload");
                    fileName = model.file.FileName;
                    string fullpath = Path.Combine(upload, fileName);
                    model.file.CopyTo(new FileStream(fullpath, FileMode.Create));
                }
                var author = _authorRepository.Find(model.AuthorId);
                Book book = new Book
                {
                    ID = model.BookId,
                    Title = model.Title,
                    Description = model.Desc,
                    Author =author,
                    ImageUrl = fileName
                };
                _bookRepository.add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = _bookRepository.Find(id);
            var authorid = book.Author == null ? book.Author.ID = 0 : book.Author.ID;
            var ViewModel = new BookAuthorViewModel
            {
                BookId = book.ID,
                Title = book.Title,
                Desc = book.Description,
                AuthorId = authorid,
                Authors = _authorRepository.List().ToList()
            };
            return View( ViewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( BookAuthorViewModel model)
        {
            try
            {
                string fileName = string.Empty;
                if (model.file != null)
                {
                    string upload = Path.Combine(_hostingEnvironment.WebRootPath,"upload");
                    fileName = model.file.FileName;
                    string fullpath = Path.Combine(upload, fileName);
                    //Delete Old File
                    string oldfile = model.ImageUrl;
                    string fulloldpath = Path.Combine(upload, oldfile);

                    if (fullpath != fulloldpath)
                    {
                        System.IO.File.Delete(fulloldpath);
                        model.file.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }
                   
                }
                var author = _authorRepository.Find(model.AuthorId);
                Book book = new Book
                {
                  ID = model.BookId,
                    Title = model.Title,
                    Description = model.Desc,
                    Author = author,
                    ImageUrl = fileName
                };
                _bookRepository.Update(model.BookId,book);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var auth = _bookRepository.Find(id);
            return View(auth);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfrimsDelete(int id)
        {
            try
            {
                _bookRepository.Delete(id);
         
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

      
        public IActionResult Search(string term)
        {
            var result = _bookRepository.Search(term);

            return View("Index",result);
        }

    }
}
