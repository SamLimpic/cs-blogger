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



        internal Account GetById(string Id)
        {
            string sql = "SELECT * FROM accounts WHERE id = @id";
            return _db.QueryFirstOrDefault<Account>(sql, new { Id });
        }



        internal Account Create(Account userInfo)
        {
            string sql = @"
            INSERT INTO accounts
            (id, name, picture, email)
            VALUES
            (@ID, @Name, @Picture, @Email)";
            _db.Execute(sql, userInfo);
            return userInfo;
        }



        internal bool Update(Account original)
        {
            string sql = @"
            UPDATE accounts
            SET
                name = @Name,
                email = @Email,
                picture = @Picture";
            int affectedRows = _db.Execute(sql, original);
            return affectedRows == 1;
        }

    }
}