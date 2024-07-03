using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MODELS.Models
{
    public class BookletContext:DbContext
    {
        public BookletContext(DbContextOptions <BookletContext> booklet):base(booklet)
        {

        }
        public DbSet<Booklet> Booklets { get; set; }

    }
}
