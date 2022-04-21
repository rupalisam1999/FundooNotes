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
    }
}
