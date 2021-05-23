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
    }
}