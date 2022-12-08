using BlogProjectWebApi.Context;
using BlogProjectWebApi.Models;
using BlogProjectWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BlogProjectWebApi.Controllers
{
    public class EmailController : ApiController
    {
        private EmailRepository _repo = new EmailRepository(new BlogDbContext());
        [HttpPost]
        [Route("api/email/send")]
        public bool SendMail(EmailModel model)
        {
            ResultClass response = new ResultClass();

            response =_repo.Add(model);
            if (response.Result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpGet]
        [Route("api/email/get")]
        public List<EmailModel> GetEmail()
        {
            ResultClass response = new ResultClass();

            List<EmailModel> models = new List<EmailModel>();
            models = _repo.List();
            return models;
        }
        [HttpGet]
        [Route("api/email/get/{id}")]
        public EmailModel GetEmailById(int Id)
        {
            EmailModel models = new EmailModel();
            models = _repo.Get(Id);
            return models;
        }
        [HttpDelete]
        [Route("api/email/delete/{id}")]
        public bool DeleteEmail(int Id)
        {
            ResultClass response = new ResultClass();
            response = _repo.Delete(Id);
            if (response.Result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}