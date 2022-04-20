using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.UserInterface
{
    interface IUserRL
    {
        public void AddUser(UserPostModel user);
    }
}
