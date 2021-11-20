using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleProject.Models;
using SimpleProject.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleProject.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBookRepository<Author> _authorRepository;

        public AuthorController(IBookRepository<Author> authorRepository )
        {
           _authorRepository = authorRepository;
        }
        // GET: AuthorController
        public ActionResult Index()
        {
            var Auth = _authorRepository.List();
            return View(Auth);
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(int id)
        {
            var authr = _authorRepository.Find(id);
            return View(authr);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Author ath)
        {
            try
            {
                _authorRepository.add(ath);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = _authorRepository.Find(id);

            return View(author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            try
            {
                _authorRepository.Update(id, author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            var aut = _authorRepository.Find(id);
            return View(aut);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try
            {
                _authorRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
