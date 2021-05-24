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



        internal Account Update(Account edit, Account currentUser)
        {
            currentUser.Name = edit.Name.Length > 0 ? edit.Name : currentUser.Name;
            currentUser.Picture = edit.Picture.Length > 0 ? edit.Picture : currentUser.Picture;

            if (edit.Id != currentUser.Id)
            {
                throw new Exception("You cannot edit another users Account");
            }
            if (_repo.Update(currentUser))
            {
                return currentUser;
            }
            throw new Exception("Something has gone wrong...");
        }
    }
}