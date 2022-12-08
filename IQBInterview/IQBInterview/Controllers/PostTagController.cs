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
    public class PostTagController : ApiController
    {
        public static BlogDbContext _context = new BlogDbContext();
        private PostTagRepository _repo = new PostTagRepository(_context) ;

        [HttpGet]
        [Route("api/postTag/get")]
        public ResultClass GetPostTag()
        {
            List<PostTag> comments = _repo.List();
            ResultClass result = new ResultClass();
            if (comments != null)
            {
                result.Result = true;
                result.ResultMessages = new List<string>()
                {
                    "PostTag Loaded"
                };
                result.ResultObject = comments;
                return result;
            }
            else
            {
                result.Result = false;
                result.ResultMessages = new List<string>()
                {
                    "PostTag Not Loaded"
                };
                result.ResultObject = null;
                return result;
            }
        }

        public List<PostTag> GetPostTagById(int postId)
        {
            List<PostTag> postTags = _repo.GetPostTag(postId);
            if (postTags != null)
            {
                return postTags;

            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        [Route("api/postTag/addPostTag")]
        public ResultClass AddPostTag(PostTag item)
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
                    "PostTag is not added. Please send to required prop"
                };
                response.ResultObject = null;
                return response;
            }
        }
        [HttpPut]
        [Route("api/postTag/updatePostTag/{Id}")]
        public ResultClass UpdatePostTag(PostTag item, int id)
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
                    "Post is not updated. Please send to required prop"
                };
                response.ResultObject = null;
                return response;
            }
        }

        [HttpDelete]
        [Route("api/postTag/removePostTag/{id}")]
        public ResultClass CommentPostTag(int id)
        {
            return _repo.Delete(id);
        }
    }
}