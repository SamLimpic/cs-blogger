using System.Collections.Generic;
using System.Data;
using cs_blogger.Models;
using Dapper;

namespace cs_blogger.Repositories
{
    public class AccountsRepository
    {
        private readonly IDbConnection _db;



        public AccountsRepository(IDbConnection db)
        {
            _db = db;
        }



        internal Account GetById(string id)
        {
            string sql = "SELECT * FROM profiles WHERE id = @id";
            return _db.QueryFirstOrDefault<Account>(sql, new { id });
        }



        internal Account Create(Account userInfo)
        {
            string sql = @"
            INSERT INTO profiles
            (id, name, picture, email)
            VALUES
            (@Id, @Name, @Picture, @Email)";
            _db.Execute(sql, userInfo);
            return userInfo;
        }



        internal bool Update(Account original)
        {
            string sql = @"
            UPDATE profiles
            SET
                name = @Name,
                picture = @Picture
            WHERE id = @id";
            int affectedRows = _db.Execute(sql, original);
            return affectedRows == 1;
        }

    }
}