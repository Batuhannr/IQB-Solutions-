using IQBInterview.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using IQBInterview.Context;
using IQBInterview.Models;
using System.Web.Http.Cors;

namespace IQBInterview.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StudentController : ApiController
    {
        private StudentRepository _repo = new StudentRepository(new IQBDbContext());
        [HttpGet]
        [Route("api/Student/get")]
        public ResultClass GetStudent()
        {
            List<Student> students = _repo.List();
            ResultClass result = new ResultClass();
            if (students.Count > 0)
            {
                result.Result = true;
                result.ResultMessages = new List<string>()
                {
                    "Succes"
                };
                result.ResultObject = students;
                return result;

            }
            else
            {
                result.Result = false;
                result.ResultMessages = new List<string>()
                {
                    "Students Not loaded"
                };
                result.ResultObject = null;
                return result;

            }
        }

        [HttpGet]
        [Route("api/Student/get/{id}")]
        public ResultClass GetStudentById(int id)
        {
            Student Student = _repo.Get(id);
            ResultClass result = new ResultClass();
            if (Student != null)
            {
                result.Result = true;
                result.ResultMessages = new List<string>()
                {
                    "Succes"
                };
                result.ResultObject = Student;
                return result;

            }
            else
            {
                result.Result = false;
                result.ResultMessages = new List<string>()
                {
                    "Student Not loaded"
                };
                result.ResultObject = null;
                return result;
            }
        }
        [HttpPost]
        [Route("api/Student/addStudent")]
        public ResultClass PostStudent(Student Student)
        {
            ResultClass response = new ResultClass();
            response = _repo.Add(Student);
            //return Request.CreateResponse(HttpStatusCode.OK, "Ekleme Başarılı");
            return response;

        }
        [HttpPut]
        [Route("api/Student/updateStudent/{Id}")]
        public ResultClass UpdateStudent(Student Student, int Id)
        {
            ResultClass response = new ResultClass();
            if (ModelState.IsValid)
            {
                response = _repo.Update(Student, Id);
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
                response.ResultObject = Student;
                //return Request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState);
                return response;
            }
        }

        [HttpDelete]
        [Route("api/Student/removeStudent/{id}")]
        public ResultClass RemoveStudent(int id)
        {
            return _repo.Delete(id);
        }

        
    }

}