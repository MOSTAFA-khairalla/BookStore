using Microsoft.AspNetCore.Http;
using SimpleProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleProject.ViewModels
{
    public class BookAuthorViewModel
    {

        public int BookId { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public int AuthorId { get; set; }
        public List<Author> Authors { get; set; }

        public IFormFile  file { get; set; }
        public string ImageUrl { get; set; }
    }
}
