using IQBInterview.Context;
using IQBInterview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IQBInterview.Repository
{
    public class CourseRepository : BaseRepository<Course>
    {
        private readonly IQBDbContext _context;
        public CourseRepository(IQBDbContext context) : base(context)
        {
            _context = context;
        }
    }
}