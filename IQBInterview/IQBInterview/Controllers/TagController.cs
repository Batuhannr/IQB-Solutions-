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
    public class TagController : ApiController
    {
        public static BlogDbContext _context = new BlogDbContext();

        private TagRepository _repo  = new TagRepository(_context);

        [HttpGet]
        [Route("api/tag/get")]
        public ResultClass GetTags()
        {
            List<Tag> tags = _repo.List();
            ResultClass result = new ResultClass();
            if (tags != null)
            {
                result.Result = true;
                result.ResultMessages = new List<string>()
                {
                    "Tags Loaded"
                };
                result.ResultObject = tags;
                return result;
            }
            else
            {
                result.Result = false;
                result.ResultMessages = new List<string>()
                {
                    "Tags Not Loaded"
                };
                result.ResultObject = null;
                return result;
            }
        }

        [HttpGet]
        [Route("api/tag/get/{id}")]
        public ResultClass GetTagById(int id)
        {
            Tag tag = _repo.Get(id);
            ResultClass result = new ResultClass();
            if (tag != null)
            {
                result.Result = true;
                result.ResultMessages = new List<string>()
                {
                    "Tag Loaded"
                };
                result.ResultObject = tag;
                return result;

            }
            else
            {
                result.Result = false;
                result.ResultMessages = new List<string>()
                {
                    "Tags is not found"
                };
                result.ResultObject = null;
                return result;
            }
        }

        [HttpPost]
        [Route("api/tag/addTag")]
        public ResultClass AddTag(Tag item)
        {
            item.CreatedOn = DateTime.Now;
            item.LastModifiedOn = DateTime.Now;
            item.PublishedOn = DateTime.Now;
            ResultClass response = new ResultClass();
            response = _repo.Add(item);
            //return Request.CreateResponse(HttpStatusCode.OK, "Ekleme Başarılı");
            return response;
        }
        [HttpPut]
        [Route("api/tag/updateTag/{Id}")]
        public ResultClass UpdateTag(Tag tag, int id)
        {
            ResultClass response = new ResultClass();
            if (ModelState.IsValid)
            {
                response = _repo.Update(tag, id);
                return response;
            }
            else
            {
                response.Result = false;
                response.ResultMessages = new List<string>(){
                    "Tag is not updated. Please send to required prop"
                };
                response.ResultObject = null;
                return response;
            }
        }

        [HttpDelete]
        [Route("api/tag/removeTag/{id}")]
        public ResultClass RemoveTag(int id)
        {
            return _repo.Delete(id);
        }

    }
}