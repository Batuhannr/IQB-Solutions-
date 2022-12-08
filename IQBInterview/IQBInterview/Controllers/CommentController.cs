using BlogProjectWebApi.Context;
using BlogProjectWebApi.Models;
using BlogProjectWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//using System.Web.Http.Cors;

namespace BlogProjectWebApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CommentController : ApiController
    {
        private CommentRepository _repo = new CommentRepository(new BlogDbContext());
        [HttpGet]
        [Route("api/comment/get")]
        public ResultClass GetComment()
        {
            List<Comment> comments = _repo.List();
            ResultClass result = new ResultClass();
            if (comments != null)
            {
                result.Result = true;
                result.ResultMessages = new List<string>()
                {
                    "Comments Loaded"
                };
                result.ResultObject = comments;
                return result;
            }
            else
            {
                result.Result = false;
                result.ResultMessages = new List<string>()
                {
                    "Comments Not Loaded"
                };
                result.ResultObject = null;
                return result;
            }
        }

        [HttpGet]
        [Route("api/comment/get/{id}")]
        public ResultClass GetCommentById(int id)
        {
            Comment comment = _repo.Get(id);
            ResultClass result = new ResultClass();
            if (comment != null)
            {
                result.Result = true;
                result.ResultMessages = new List<string>()
                {
                    "Comment Loaded"
                };
                result.ResultObject = comment;
                return result;

            }
            else
            {
                result.Result = false;
                result.ResultMessages = new List<string>()
                {
                    "Comment is not found"
                };
                result.ResultObject = null;
                return result;
            }
        }

        [HttpPost]
        [Route("api/comment/addComment")]
        public ResultClass AddComment(Comment item)
        {
            item.IsPublished = true;
            item.CreatedOn = DateTime.Now;
            item.LastModifiedOn = DateTime.Now;
            item.PublishedOn = DateTime.Now;
            ResultClass response = new ResultClass();
            if (ModelState.IsValid)
            {
                response = _repo.Add(item);
                return response;
            }
            else
            {
                response.Result = false;
                response.ResultMessages = new List<string>(){
                    "Comment is not added. Please send to required prop"
                };
                response.ResultObject = null;
                return response;
            }
        }
        [HttpPut]
        [Route("api/comment/updateComment/{Id}")]
        public ResultClass UpdateComment(Comment item, int id)
        {
            ResultClass response = new ResultClass();
            if (ModelState.IsValid)
            {
                response = _repo.Update(item, id);
                return response;
            }
            else
            {
                response.Result = false;
                response.ResultMessages = new List<string>(){
                    "Comment is not updated. Please send to required prop"
                };
                response.ResultObject = null;
                return response;
            }
        }

        [HttpDelete]
        [Route("api/comment/removeComment/{id}")]
        public ResultClass CommentTag(int id)
        {
            return _repo.Delete(id);
        }
    }
}