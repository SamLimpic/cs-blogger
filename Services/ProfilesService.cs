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
    }
}