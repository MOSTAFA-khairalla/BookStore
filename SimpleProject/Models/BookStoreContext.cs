﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleProject.Models
{
    public class BookStoreContext:DbContext
    {

        public BookStoreContext( DbContextOptions<BookStoreContext> options ):base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
