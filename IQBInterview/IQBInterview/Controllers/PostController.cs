using BlogProjectWebApi.Context;
using BlogProjectWebApi.Models;
using BlogProjectWebApi.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//using System.Web.Http.Cors;

namespace BlogProjectWebApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PostController : ApiController
    {
        public BlogDbContext _context;
        private PostRepository _repo;
        private TagRepository _TagRepository;
        private CategoryRepository _CategoryRepository;
        private PostTagRepository _postTagRepository;
        private PostCategoryRepository _postCategoryRepository;
        private CommentRepository _commentRepository;
        public PostController()
        {
            _context = new BlogDbContext();
            _repo = new PostRepository(_context);
            _TagRepository = new TagRepository(_context);
            _CategoryRepository = new CategoryRepository(_context);
            _postTagRepository = new PostTagRepository(_context);
            _postCategoryRepository = new PostCategoryRepository(_context);
            _commentRepository = new CommentRepository(_context);
        }

        [HttpGet]
        [Route("api/post/get")]
        public ResultClass GetPost()
        {
            List<Post> Posts = _repo.List();
            ResultClass result = new ResultClass();
            if (Posts != null)
            {
                result.Result = true;
                result.ResultMessages = new List<string>()
                {
                    "Post Loaded"
                };
                foreach (var item in Posts)
                {
                    item.PostTags = GetPostTagById(item.Id);
                    item.PostCategories = GetPostCategory(item.Id);
                    

                }
                result.ResultObject = Posts;
                return result;
            }
            else
            {
                result.Result = false;
                result.ResultMessages = new List<string>()
                {
                    "Post Not Loaded"
                };
                result.ResultObject = null;
                return result;
            }
        }

        [HttpGet]
        [Route("api/post/getnull")]
        public ResultClass GetPostHeaderImageNull()
        {
            List<Post> Posts = _repo.List();
            ResultClass result = new ResultClass();
            if (Posts != null)
            {
                result.Result = true;
                result.ResultMessages = new List<string>()
                {
                    "Post Loaded"
                };
                foreach (var item in Posts)
                {
                    item.PostTags = GetPostTagById(item.Id);
                    item.PostCategories = GetPostCategory(item.Id);
                    item.PostHeaderImage = null;


                }
                result.ResultObject = Posts;
                return result;
            }
            else
            {
                result.Result = false;
                result.ResultMessages = new List<string>()
                {
                    "Post Not Loaded"
                };
                result.ResultObject = null;
                return result;
            }
        }
        public List<PostTag> GetPostTagById(int postId)
        {
            List<PostTag> postTags = _postTagRepository.GetPostTag(postId);
            foreach (var item in postTags)
            {
                item.Tag = _TagRepository.Get(item.TagId);
            }
            return postTags;
        }
        public List<PostCategory> GetPostCategory(int postId)
        {
            List<PostCategory> postCategory = _postCategoryRepository.GetPostCategory(postId);
            foreach (var item in postCategory)
            {
                item.Category = _CategoryRepository.Get(item.CategoryId);
            }
            return postCategory;
        }
        public List<Comment> GetPostComment(int postId)
        {
            List<Comment> postComment = _commentRepository.getPostComment(postId);
            return postComment;
        }
        [HttpGet]
        [Route("api/userinterface/getlastposts/{count}")]
        public List<Post> getlastpost(int count)
        {
            List<Post> posts = new List<Post>();
            posts = _repo.lastCountPost(count);
            foreach (var item in posts)
            {
                item.PostTags = GetPostTagById(item.Id);
                item.PostCategories = GetPostCategory(item.Id);

            }
            return posts;
        }
        [HttpGet]
        [Route("api/userinterface/getmostposts/{count}")]
        public List<Post> getMostPopulerPost(int count)
        {
            List<Post> posts = new List<Post>();
            posts = _repo.mostPopulerPost(count);
            foreach (var item in posts)
            {
                item.PostContents = null;

            }
            return posts;
        }
        [HttpGet]
        [Route("api/userinterface/getlastpostsnull/{count}")]
        public List<Post> getlastpostNull(int count)
        {
            List<Post> posts = new List<Post>();
            posts = _repo.lastCountPost(count);
            foreach (var item in posts)
            {
                item.PostContents = null;
            }
            return posts;
        }

        [HttpGet]
        [Route("api/post/get/{id}")]
        public ResultClass GetPostById(int id)
        {
            Post post = _repo.Get(id);
            ResultClass result = new ResultClass();
            if (post != null)
            {
                result.Result = true;
                result.ResultMessages = new List<string>()
                {
                    "Post Loaded"
                };
                
                _repo.UpdateReadingCount(id);
                post.PostTags = GetPostTagById(post.Id);
                post.PostCategories = GetPostCategory(post.Id);
                post.Comments = GetPostComment(post.Id);
                result.ResultObject = post;

                return result;

            }
            else
            {
                result.Result = false;
                result.ResultMessages = new List<string>()
                {
                    "Post is not found"
                };
                result.ResultObject = null;
                return result;
            }
        }

        [HttpPost]
        [Route("api/post/addPost")]
        public ResultClass AddPost(Post item)
        {
            item.CreatedOn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            item.LastModifiedOn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            item.PublishedOn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            item.ReadingCount = 0;
            try
            {
                ResultClass response = new ResultClass();
                response = _repo.Add(item);
                return response;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [HttpPut]
        [Route("api/post/updatePost/{Id}")]
        public ResultClass UpdatePost(Post item, int id)
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
        [Route("api/post/removePost/{id}")]
        public ResultClass CommentPost(int id)
        {
            return _repo.Delete(id);
        }
    }
}