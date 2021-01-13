using DailyPlanner.BL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanner.CMD
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("DBConnection")
        {
        }

        public DbSet<Note> Notes { get; set; }
    }
}
