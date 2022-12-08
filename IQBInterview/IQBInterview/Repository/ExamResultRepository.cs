using IQBInterview.Context;
using IQBInterview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IQBInterview.Repository
{
    public class ExamResultRepository : BaseRepository<ExamResult>
    {
        private readonly IQBDbContext _context;
        public ExamResultRepository(IQBDbContext context) : base(context)
        {
            _context = context;
        }
        public List<ExamResult> GetByStudentId(int studentId)
        {
            List<ExamResult> entity = _context.ExamResults.Where(s => s.StudentId == studentId).ToList() ;
            return entity;
        }
    }
}