using BusinessLayer.Interfases;
using DataBaseLayer;
using Microsoft.AspNetCore.Authorization;
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
                var getUserData = fundoo.Users.Where(u=>u.Email == user.Email).FirstOrDefault();
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
        [HttpPost("Login/{Email}/{Password}")]
        public ActionResult LoginUser(string Email, string Password)
        {
            try
            {
                
                var result = this.userBL.LoginUser(Email,Password);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"Login Successfull {result}" });
                }
                return this.BadRequest(new { success = false, message = $"{result} LoginFailed" });
                

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        [HttpPost("ForgotPassword/{Email}")]
        public IActionResult ForgotPassword(string Email)
        {
            try
            {
                bool result = this.userBL.ForgotPassword(Email);
                if (result == true)
                    return this.Ok(new { success = true, message = $"Token generated.Please check your email" });
                else
                    return this.Ok(new { success = false, message = $"email not sent" });

            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, e.Message });
            }
        }

        [Authorize]
        [HttpPut("resetpassword")]
        public ActionResult ResetPassword(string Email, string Password, string cPassword)
        {
            try
            {
                if (Password == cPassword)
                {
                    return this.Ok(new { success = false, message = $"your old password is same as current password" });
                }
                this.userBL.ResetPassword(Email, Password, cPassword);
                return this.Ok(new { success = true, message = $"password changes successfully to {Email}" });
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, e.Message });
            }
        }


        [HttpGet("getallusers")]
        public ActionResult GetAllUsers()
        {
            try
            {
                var result = this.userBL.GetAllUsers();
                return this.Ok(new { success = true, message = $"Below are the User data", data = result });
            }
            catch (Exception e)
            {
                throw e;
            }

        }
       
    }
}
