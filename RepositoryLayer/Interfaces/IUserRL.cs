using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.UserInterface
{
    public interface IUserRL
    {
        public void AddUser(UserPostModel user);
        public string LoginUser(string Email, string Password);

        public bool ForgetPassword(string Email);

        public void ResetPassword(string Email, string Password, string cPassword);



    }
}
