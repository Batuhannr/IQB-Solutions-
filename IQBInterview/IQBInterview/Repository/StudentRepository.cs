using IQBInterview.Context;
using IQBInterview.IRepository;
using IQBInterview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IQBInterview.Repository
{
    public class StudentRepository : BaseRepository<Student>
    {
        private readonly IQBDbContext _context;
        public StudentRepository(IQBDbContext context) : base(context)
        {
            _context = context;
        }
    }
}