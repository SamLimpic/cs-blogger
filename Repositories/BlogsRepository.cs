using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using cs_blogger.Models;
using Dapper;

namespace cs_blogger.Repositories
{
    public class BlogsRepository
    {
        private readonly IDbConnection _db;



        public BlogsRepository(IDbConnection db)
        {
            _db = db;
        }



        public IEnumerable<Blog> GetAll()
        {
            string sql = @"
      SELECT
       b.*,
       p.*
      FROM blogs b
      JOIN profiles p ON b.creatorId = p.id
      WHERE b.published = 1";
            return _db.Query<Blog, Account, Blog>(sql, (blog, account) =>
            {
                blog.Creator = account;
                return blog;
            }, splitOn: "id");
        }



        public Blog GetById(int id)
        {
            string sql = @"
      SELECT 
        b.*,
        p.* 
      FROM blogs b
      JOIN profiles p ON b.creatorId = p.id
      WHERE b.id = @id";
            return _db.Query<Blog, Account, Blog>(sql, (blog, account) =>
            {
                blog.Creator = account;
                return blog;
            }
            , new { id }, splitOn: "id").FirstOrDefault();
        }



        public IEnumerable<Blog> GetBlogsByCreatorId(string id)
        {
            string sql = @"
      SELECT 
        b.*,
        p.* 
      FROM blogs b
      JOIN profiles p ON b.creatorId = p.id
      WHERE 
        b.published = 1";
            return _db.Query<Blog, Account, Blog>(sql, (blog, account) =>
            {
                blog.Creator = account;
                return blog;
            }
            , new { id }, splitOn: "id");
        }



        public IEnumerable<Comment> GetCommentsByBlogId(int id)
        {
            string sql = @"
      SELECT 
        c.*,
        b.* 
      FROM comments c
      JOIN blogs b ON c.blog = b.id
      WHERE c.blog = @id";
            return _db.Query<Comment, Blog, Comment>(sql, (comment, blog) =>
            {
                return comment;
            }
            , new { id }, splitOn: "id");
        }



        public Blog Create(Blog newBlog)
        {
            string sql = @"
      INSERT INTO blogs
      (creatorId, title, body, imgUrl, published)
      VALUES
      (@CreatorId, @Title, @Body, @ImgUrl, @Published);
      SELECT LAST_INSERT_ID()";
            newBlog.Id = _db.ExecuteScalar<int>(sql, newBlog);
            return newBlog;
        }



        internal bool Update(Blog original)
        {
            string sql = @"
            UPDATE blogs
            SET
                title = @Title,
                body = @Body,
                imgUrl = @ImgUrl,
                published = @Published
            WHERE id = @id";
            int affectedRows = _db.Execute(sql, original);
            return affectedRows == 1;
        }



        public bool Delete(int id)
        {
            string sql = "DELETE FROM blogs WHERE id = @id LIMIT 1";
            int affectedRows = _db.Execute(sql, new { id });
            return affectedRows == 1;
        }
    }
}