using System;
using System.Collections.Generic;
using cs_blogger.Models;
using cs_blogger.Repositories;

namespace cs_blogger.Services
{
    public class AccountsService
    {
        private readonly AccountsRepository _repo;



        public AccountsService(AccountsRepository repo)
        {
            _repo = repo;
        }



        internal Account GetOrCreateAccount(Account userInfo)
        {
            Account account = _repo.GetById(userInfo.Id);
            if (account == null)
            {
                return _repo.Create(userInfo);
            }
            return account;
        }



        internal IEnumerable<Blog> GetBlogsByAccountId(string id)
        {
            IEnumerable<Blog> blogs = _repo.GetBlogsByAccountId(id);
            if (blogs == null)
            {
                throw new Exception("No blogs available!");
            }
            return blogs;
        }



        internal IEnumerable<Comment> GetCommentsByAccountId(string id)
        {
            IEnumerable<Comment> comments = _repo.GetCommentsByAccountId(id);
            if (comments == null)
            {
                throw new Exception("No comments available!");
            }
            return comments;
        }



        internal Account Update(Account edit)
        {
            Account original = edit;
            original.Name = edit.Name.Length > 0 ? edit.Name : original.Name;
            original.Email = edit.Email.Length > 0 ? edit.Email : original.Email;
            original.Picture = edit.Picture.Length > 0 ? edit.Picture : original.Picture;

            if (_repo.Update(original))
            {
                return original;
            }
            throw new Exception("Something has gone wrong...");
        }
    }
}