using BusinessLayer.Interfases;
using DataBaseLayer;
using RepositoryLayer.Entity;
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

        public bool ForgotPassword(string Email)
        {
            
            {
                try
                {
                    return this.userRL.ForgetPassword(Email);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string LoginUser(string Email, string Password)
        {
            try
            {

               return userRL.LoginUser(Email,Password);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ResetPassword(string Email, string Password, string cPassword)
        {
            try
            {
                userRL.ResetPassword(Email, Password, cPassword);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                return userRL.GetAllUsers();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
