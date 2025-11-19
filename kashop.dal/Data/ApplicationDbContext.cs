using kashop.dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.dal.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Category>Categories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
        {
        }
    }
}
