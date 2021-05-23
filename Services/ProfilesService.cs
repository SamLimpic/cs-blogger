using System;
using System.Collections.Generic;
using cs_blogger.Models;
using cs_blogger.Repositories;

namespace cs_blogger.Services
{
    public class ProfilesService
    {
        private readonly ProfilesRepository _repo;



        public ProfilesService(ProfilesRepository repo)
        {
            _repo = repo;
        }



        internal Account GetById(string id)
        {
            Account profile = _repo.GetById(id);
            if (profile == null)
            {
                throw new Exception("Invalid Profile Id");
            }
            return profile;
        }



        internal IEnumerable<Blog> GetBlogsByProfileId(string id)
        {
            IEnumerable<Blog> blogs = _repo.GetBlogsByProfileId(id);
            if (blogs == null)
            {
                throw new Exception("No blogs available!");
            }
            return blogs;
        }



        internal IEnumerable<Comment> GetCommentsByProfileId(string id)
        {
            IEnumerable<Comment> comments = _repo.GetCommentsByProfileId(id);
            if (comments == null)
            {
                throw new Exception("No comments available!");
            }
            return comments;
        }
    }
}