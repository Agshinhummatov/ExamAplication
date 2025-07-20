using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class ExamDbContextFactory : IDesignTimeDbContextFactory<ExamDbContext>
    {
        public ExamDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ExamDbContext>();

          
            optionsBuilder.UseSqlServer("Server=DESKTOP-BHOOLBN\\SQLEXPRESS;Database=ExamProgramDb;Trusted_Connection=True;TrustServerCertificate=true;");

            return new ExamDbContext(optionsBuilder.Options);
        }
    }
}
