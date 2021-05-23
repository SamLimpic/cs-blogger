using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using cs_blogger.Models;
using Dapper;

namespace cs_blogger.Repositories
{
    public class CommentsRepository
    {
        private readonly IDbConnection _db;



        public CommentsRepository(IDbConnection db)
        {
            _db = db;
        }



        public Comment GetById(int id)
        {
            string sql = @"
      SELECT 
        c.*,
        a.*,
        b.*
      FROM comments c
      JOIN accounts a ON c.creatorId = a.id
      JOIN blogs a ON c.blogId = b.id
      WHERE id = @id";
            return _db.Query<Comment, Account, Blog, Comment>(sql, (comment, account, blog) =>
            {
                comment.Creator = account;
                comment.Blog = blog;
                return comment;
            }
            , new { id }, splitOn: "id").FirstOrDefault();
        }



        public IEnumerable<Comment> GetCommentsByCreatorId(string id)
        {
            string sql = @"
      SELECT 
        c.*,
        a.* 
      FROM comments c
      JOIN accounts a ON c.creatorId = a.id
      WHERE creatorId = @id";
            return _db.Query<Comment, Account, Comment>(sql, (comment, account) =>
            {
                comment.Creator = account;
                return comment;
            }
            , new { id }, splitOn: "id");
        }




        public Comment Create(Comment newComment)
        {
            string sql = @"
      INSERT INTO comments
      (creatorId, blogId, body)
      VALUES
      (@CreatorId, @BlogId, @Body);
      SELECT LAST_INSERT_ID()";
            newComment.Id = _db.ExecuteScalar<int>(sql, newComment);
            return newComment;
        }



        internal bool Update(Comment original)
        {
            string sql = @"
            UPDATE comments
            SET body = @Body";
            int affectedRows = _db.Execute(sql, original);
            return affectedRows == 1;
        }



        public bool Delete(int id)
        {
            string sql = "DELETE FROM comments WHERE id = @id LIMIT 1";
            int affectedRows = _db.Execute(sql, new { id });
            return affectedRows == 1;
        }
    }
}