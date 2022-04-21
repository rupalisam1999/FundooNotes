using BusinessLayer.Interfases;
using DataBaseLayer;
using RepositoryLayer.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Servises
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public void AddUser(UserPostModel user)
        {
           try
            {
                this.userRL.AddUser(user);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string LoginUser(string Email, string Password)
        {
            try
            {

               return this.userRL.LoginUser(Email,Password);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
