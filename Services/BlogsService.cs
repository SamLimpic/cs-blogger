using System;
using System.Collections.Generic;
using cs_blogger.Models;
using cs_blogger.Repositories;

namespace cs_blogger.Services
{
    public class BlogsService
    {
        private readonly BlogsRepository _repo;



        public BlogsService(BlogsRepository repo)
        {
            _repo = repo;
        }



        internal IEnumerable<Blog> GetAll()
        {
            return _repo.GetAll();
        }



        internal Blog GetById(int id)
        {
            Blog blog = _repo.GetById(id);
            if (blog == null)
            {
                throw new Exception("Invalid Blog Id");
            }
            return blog;
        }



        internal IEnumerable<Blog> GetBlogsByCreatorId(string id)
        {
            return _repo.GetBlogsByCreatorId(id);
        }



        internal IEnumerable<Comment> GetCommentsByBlogId(int id)
        {
            IEnumerable<Comment> comments = _repo.GetCommentsByBlogId(id);
            if (comments == null)
            {
                throw new Exception("No comments available!");
            }
            return comments;
        }



        internal Blog Create(Blog newBlog)
        {
            return _repo.Create(newBlog);
        }



        internal Blog Update(Blog edit, string creatorId)
        {
            Blog original = GetById(edit.Id);
            edit.CreatorId = original.CreatorId;
            original.Title = edit.Title.Length > 0 ? edit.Title : original.Title;
            original.Body = edit.Body.Length > 0 ? edit.Body : original.Body;
            original.ImgUrl = edit.ImgUrl.Length > 0 ? edit.ImgUrl : original.ImgUrl;


            if (_repo.Update(original))
            {
                return original;
            }
            throw new Exception("Something has gone wrong...");
        }



        internal void Delete(int id, string creatorId)
        {
            Blog blog = GetById(id);
            if (blog.CreatorId != creatorId)
            {
                throw new Exception("You cannot delete another users Blog");
            }
            if (!_repo.Delete(id))
            {
                throw new Exception("Something has gone wrong...");
            };
        }
    }
}