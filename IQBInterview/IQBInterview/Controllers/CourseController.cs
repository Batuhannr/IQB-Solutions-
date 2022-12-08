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
    public class CourseController : ApiController
    {
        private CourseRepository _repo = new CourseRepository(new IQBDbContext());
        [HttpGet]
        [Route("api/Course/get")]
        public ResultClass GetCourse()
        {
            List<Course> Courses = _repo.List();
            ResultClass result = new ResultClass();
            if (Courses.Count > 0)
            {
                result.Result = true;
                result.ResultMessages = new List<string>()
                {
                    "Succes"
                };
                result.ResultObject = Courses;
                return result;

            }
            else
            {
                result.Result = false;
                result.ResultMessages = new List<string>()
                {
                    "Courses Not loaded"
                };
                result.ResultObject = null;
                return result;

            }
        }

        [HttpGet]
        [Route("api/Course/get/{id}")]
        public ResultClass GetCourseById(int id)
        {
            Course Course = _repo.Get(id);
            ResultClass result = new ResultClass();
            if (Course != null)
            {
                result.Result = true;
                result.ResultMessages = new List<string>()
                {
                    "Succes"
                };
                result.ResultObject = Course;
                return result;

            }
            else
            {
                result.Result = false;
                result.ResultMessages = new List<string>()
                {
                    "Course Not loaded"
                };
                result.ResultObject = null;
                return result;
            }
        }
        [HttpPost]
        [Route("api/Course/addCourse")]
        public ResultClass PostCourse(Course Course)
        {
            ResultClass response = new ResultClass();
            response = _repo.Add(Course);
            //return Request.CreateResponse(HttpStatusCode.OK, "Ekleme Başarılı");
            return response;

        }
        [HttpPut]
        [Route("api/Course/updateCourse/{Id}")]
        public ResultClass UpdateCourse(Course Course, int Id)
        {
            ResultClass response = new ResultClass();
            if (ModelState.IsValid)
            {
                response = _repo.Update(Course, Id);
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
                response.ResultObject = Course;
                //return Request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState);
                return response;
            }
        }

        [HttpDelete]
        [Route("api/Course/removeCourse/{id}")]
        public ResultClass RemoveCourse(int id)
        {
            return _repo.Delete(id);
        }
    }

}