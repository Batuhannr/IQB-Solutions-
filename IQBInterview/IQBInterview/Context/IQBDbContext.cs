using IQBInterview.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IQBInterview.Context
{
    public class IQBDbContext : DbContext
    {
        public IQBDbContext(): base("name=IQBContext")
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }



    }
}