using System;
using System.Collections.Generic;
using cs_blogger.Models;
using cs_blogger.Repositories;

namespace cs_blogger.Services
{
    public class CommentsService
    {
        private readonly CommentsRepository _repo;



        public CommentsService(CommentsRepository repo)
        {
            _repo = repo;
        }



        internal Comment GetById(int id)
        {
            Comment comment = _repo.GetById(id);
            if (comment == null)
            {
                throw new Exception("Invalid Comment Id");
            }
            return comment;
        }



        internal IEnumerable<Comment> GetCommentsByCreatorId(string id)
        {
            return _repo.GetCommentsByCreatorId(id);
        }



        internal Comment Create(Comment newComment)
        {
            return _repo.Create(newComment);
        }



        internal Comment Update(Comment edit, string creatorId)
        {
            Comment original = GetById(edit.Id);
            edit.CreatorId = creatorId;
            original.Body = edit.Body.Length > 0 ? edit.Body : original.Body;

            if (edit.CreatorId != creatorId)
            {
                throw new Exception("You cannot edit another users Comment");
            }
            if (_repo.Update(original))
            {
                return original;
            }
            throw new Exception("Something has gone wrong...");
        }



        internal void Delete(int id, string creatorId)
        {
            Comment comment = GetById(id);
            if (comment.CreatorId != creatorId)
            {
                throw new Exception("You cannot delete another users Comment");
            }
            if (!_repo.Delete(id))
            {
                throw new Exception("Something has gone wrong...");
            };
        }
    }
}