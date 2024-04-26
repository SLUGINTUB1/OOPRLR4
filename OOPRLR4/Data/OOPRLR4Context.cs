using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOPRLR4.Models;

namespace OOPRLR4.Data
{
    public class OOPRLR4Context : DbContext
    {
        public OOPRLR4Context (DbContextOptions<OOPRLR4Context> options)
            : base(options)
        {
        }

        public DbSet<OOPRLR4.Models.Mark> Mark { get; set; } = default!;
        public DbSet<OOPRLR4.Models.Exam> Exam { get; set; } = default!;
        public DbSet<OOPRLR4.Models.Applicant> Applicant { get; set; } = default!;
        public DbSet<OOPRLR4.Models.Teacher> Teacher { get; set; } = default!;
        public DbSet<OOPRLR4.Models.Faculty> Faculty { get; set; } = default!;
    }
}
