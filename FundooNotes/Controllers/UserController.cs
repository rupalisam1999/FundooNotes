using BusinessLayer.Interfases;
using DataBaseLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.FundooNotesContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
   
    public class UserController : ControllerBase
    {
        IUserBL userBL;
        FundooContext fundoo;
        public UserController(IUserBL userBL, FundooContext fundoo)
        {
            this.userBL = userBL;
            this.fundoo = fundoo;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser(UserPostModel user)
        {
            try
            {
                var getUserData = fundoo.Users.FirstOrDefault(u=>u.Email == user.Email);
                if (getUserData != null)
                {
                    return this.Ok(new { success = false, message = $"{user.Email} is Already Exists" });
                }
                this.userBL.AddUser(user);
                return this.Ok(new { success = true, message = $"Registration Successfull { user.Email}" });

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost("Login")]
        public ActionResult LoginUser(string Email, string Password)
        {
            try
            {
                var getUserData = fundoo.Users.FirstOrDefault(u => u.Email == Email);
                if (getUserData != null)
                {
                    return this.Ok(new { success = false, message = $"{Email} LoginFailed" });
                }
                this.userBL.LoginUser(Email,Password);
                return this.Ok(new { success = true, message = $"Login Successfull { Email}" });

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
