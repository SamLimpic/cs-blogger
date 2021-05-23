using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using cs_blogger.Models;
using Dapper;

namespace cs_blogger.Repositories
{
    public class ProfilesRepository
    {
        private readonly IDbConnection _db;



        public ProfilesRepository(IDbConnection db)
        {
            _db = db;
        }



        internal Account GetById(string Id)
        {
            string sql = "SELECT * FROM profiles WHERE id = @id";
            return _db.QueryFirstOrDefault<Account>(sql, new { Id });
        }



        public IEnumerable<Comment> GetCommentsByProfileId(string id)
        {
            string sql = @"
      SELECT 
        c.*,
        p.* 
      FROM comments c
      JOIN profiles p ON c.creatorId = p.id
      WHERE id = @id";
            return _db.Query<Comment, Account, Comment>(sql, (comment, account) =>
            {
                return comment;
            }
            , new { id }, splitOn: "id");
        }



        public IEnumerable<Blog> GetBlogsByProfileId(string id)
        {
            string sql = @"
      SELECT 
        b.*,
        p.* 
      FROM blogs b
      JOIN profiles p ON b.creatorId = p.id
      WHERE id = @id";
            return _db.Query<Blog, Account, Blog>(sql, (blog, account) =>
            {
                return blog;
            }
            , new { id }, splitOn: "id");
        }
    }
}