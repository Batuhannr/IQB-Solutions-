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
    public class PostCategoryController : ApiController
    {
        private PostCategoryRepository _repo = new PostCategoryRepository(new BlogDbContext());

        [HttpGet]
        [Route("api/postcategory/get")]
        public ResultClass GetPostCategory()
        {
            List<PostCategory> comments = _repo.List();
            ResultClass result = new ResultClass();
            if (comments != null)
            {
                result.Result = true;
                result.ResultMessages = new List<string>()
                {
                    "PostCategory Loaded"
                };
                result.ResultObject = comments;
                return result;
            }
            else
            {
                result.Result = false;
                result.ResultMessages = new List<string>()
                {
                    "PostCategory Not Loaded"
                };
                result.ResultObject = null;
                return result;
            }
        }

        [HttpGet]
        [Route("api/postcategory/get/{id}")]
        public ResultClass GetPostCategoryById(int id)
        {
            PostCategory comment = _repo.Get(id);
            ResultClass result = new ResultClass();
            if (comment != null)
            {
                result.Result = true;
                result.ResultMessages = new List<string>()
                {
                    "PostCategory Loaded"
                };
                result.ResultObject = comment;
                return result;

            }
            else
            {
                result.Result = false;
                result.ResultMessages = new List<string>()
                {
                    "PostCategory is not found"
                };
                result.ResultObject = null;
                return result;
            }
        }

        [HttpPost]
        [Route("api/postcategory/addPostCategory")]
        public ResultClass AddPostCategory(PostCategory item)
        {
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
                    "PostCategory is not added. Please send to required prop"
                };
                response.ResultObject = null;
                return response;
            }
        }
        [HttpPut]
        [Route("api/postcategory/updatePostCategory/{Id}")]
        public ResultClass UpdatePostCategory(PostCategory item, int id)
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
                    "PostCategory is not updated. Please send to required prop"
                };
                response.ResultObject = null;
                return response;
            }
        }

        [HttpDelete]
        [Route("api/postcategory/removePostCategory/{id}")]
        public ResultClass CommentPostCategory(int id)
        {
            return _repo.Delete(id);
        }
    }
}