using DataBaseLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfases
{
    public interface IUserBL
    {
        public void AddUser(UserPostModel user);
        public string LoginUser(string Email, string Password);
       public bool ForgotPassword(string Email);
       public void ResetPassword(string Email, string Password, string cPassword);

        List<User> GetAllUsers();
    }
}
