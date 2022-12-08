using IQBInterview.Context;
using IQBInterview.Models;
using IQBInterview.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IQBInterview.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ExamResultController : ApiController
    {
        private StudentRepository _studentRepository;
        private CourseRepository _courseRepository;
        public IQBDbContext _context;
        private ExamResultRepository _repo;
        public ExamResultController()
        {
            _context = new IQBDbContext();
            _studentRepository = new StudentRepository(_context);
            _courseRepository = new CourseRepository(_context);
            _repo = new ExamResultRepository(_context);
        }
        [HttpGet]
        [Route("api/ExamResult/get")]
        public ResultClass GetExamResult()
        {
            List<ExamResult> ExamResults = _repo.List();
            ResultClass result = new ResultClass();
            if (ExamResults.Count > 0)
            {
                result.Result = true;
                result.ResultMessages = new List<string>()
                {
                    "Succes"
                };
                foreach (var item in ExamResults)
                {
                    item.Students = GetStudentById(item.StudentId);
                    item.Courses = GetCourseById(item.CourseId);
                }
                result.ResultObject = ExamResults;
                return result;

            }
            else
            {
                result.Result = false;
                result.ResultMessages = new List<string>()
                {
                    "ExamResults Not loaded"
                };
                result.ResultObject = null;
                return result;

            }
        }
        public Student GetStudentById(int postId)
        {
            Student students = _studentRepository.Get(postId);
            return students;
        }
        public Course GetCourseById(int postId)
        {
            Course courses = _courseRepository.Get(postId);
            return courses;
        }
        [HttpGet]
        [Route("api/ExamResult/get/{id}")]
        public ResultClass GetExamResultById(int id)
        {
            List<ExamResult> ExamResults = _repo.GetByStudentId(id);
            ResultClass result = new ResultClass();
            if (ExamResults != null)
            {
                result.Result = true;
                result.ResultMessages = new List<string>()
                {
                    "Succes"
                };
                foreach (var item in ExamResults)
                {
                    item.Students = GetStudentById(item.StudentId);
                    item.Courses = GetCourseById(item.CourseId);
                }
                result.ResultObject = ExamResults;
                return result;

            }
            else
            {
                result.Result = false;
                result.ResultMessages = new List<string>()
                {
                    "ExamResult Not loaded"
                };
                result.ResultObject = null;
                return result;
            }
        }
        [HttpPost]
        [Route("api/ExamResult/addExamResult")]
        public ResultClass PostExamResult(ExamResult ExamResult)
        {
            ResultClass response = new ResultClass();
            response = _repo.Add(ExamResult);
            //return Request.CreateResponse(HttpStatusCode.OK, "Ekleme Başarılı");
            return response;

        }
        [HttpPut]
        [Route("api/ExamResult/updateExamResult/{Id}")]
        public ResultClass UpdateExamResult(ExamResult ExamResult, int Id)
        {
            ResultClass response = new ResultClass();
            if (ModelState.IsValid)
            {
                response = _repo.Update(ExamResult, Id);
                //return Request.CreateResponse(HttpStatusCode.OK, "Güncelleme Başarılı");
                return response;
            }
            else
            {
                response.Result = false;
                response.ResultMessages = new List<string>()
                {
                    "Please Required prop sent"
                };
                response.ResultObject = ExamResult;
                //return Request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState);
                return response;
            }
        }

        [HttpDelete]
        [Route("api/ExamResult/removeExamResult/{id}")]
        public ResultClass RemoveExamResult(int id)
        {
            return _repo.Delete(id);
        }

        [HttpGet]
        [Route("api/ExamResult/studentAverage/{id}")]
        public ResultClass StudentAverage(int id)
        {
            int allcourse, successCourse, unsuccessCourse, allScore = 0;
            decimal average;
            ResultClass response = new ResultClass();
            Student st = GetStudentById(id);
            if (st != null)
            {
                List<ExamResult> ExamResults = _repo.List().Where(s => s.StudentId == id).ToList();
                allcourse = ExamResults.Count();
                successCourse = ExamResults.Where(s => s.Score >= 3).ToList().Count();
                unsuccessCourse = ExamResults.Where(s => s.Score < 3).ToList().Count();
                foreach (var item in ExamResults)
                {
                    allScore += item.Score;
                }
                average = Decimal.Divide((decimal)allScore , (decimal)allcourse);
                response.Result = true;
                response.ResultMessages = new List<string>()
                {
                "This Student Course Info",
                };
                StudentAverageClass sac = new StudentAverageClass();
                sac.allcourse = allcourse;
                sac.average = average;
                sac.Students = st;
                sac.successCourse = successCourse;
                sac.unsuccessCourse = unsuccessCourse;
                response.ResultObject = sac;
            }
            else
            {
                response.Result = false;
                response.ResultMessages = new List<string>()
                {
                    "Student Is Not Found"
                };
                response.ResultObject = null;
            }
            return response;
        }
        class StudentAverageClass
        {
            public Student Students { get; set; }
            public int allcourse { get; set; }
            public int successCourse { get; set; }
            public int unsuccessCourse { get; set; }
            public decimal average { get; set; }
        }
    }

}