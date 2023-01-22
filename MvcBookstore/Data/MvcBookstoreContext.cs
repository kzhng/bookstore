using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcBookstore.Models;

namespace MvcBookstore.Data
{
    public class MvcBookstoreContext : DbContext
    {
        public MvcBookstoreContext (DbContextOptions<MvcBookstoreContext> options)
            : base(options)
        {
        }

        public DbSet<MvcBookstore.Models.Book> Book { get; set; } = default!;
    }
}
