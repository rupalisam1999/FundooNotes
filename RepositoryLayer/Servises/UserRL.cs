using DataBaseLayer;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entity;
using RepositoryLayer.FundooNotesContext;
using RepositoryLayer.Servises;
using RepositoryLayer.UserInterface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.UserClass
{
    public class UserRL : IUserRL
    {
        FundooContext fundoo;
        public IConfiguration configuration { get; }
        public UserRL(FundooContext fundoo, IConfiguration configuration)
        {
            this.fundoo = fundoo;
            this.configuration = configuration;
        }
        public void AddUser(UserPostModel user)
        {
            try
            {
                Entity.User user1 = new Entity.User();
                user1.UserId = new Entity.User().UserId;
                user1.FirstName = user.FirstName;
                user1.LastName = user.LastName;
                user1.Email = user.Email;
                user1.Adress = user.Adress;
                user1.Password = EncryptPassword(user.Password);
                user1.registerdDate = DateTime.Now;
                fundoo.Users.Add(user1);
                fundoo.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string LoginUser(string Email, string Password)
        {

            try
            {
                var result = fundoo.Users.Where(u => u.Email == Email && u.Password == Password).FirstOrDefault();

                if (result == null)
                {
                    return null;
                }
                return GetJWTToken(Email, result.UserId);
                 string password = password;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
            // Generate JWT Token
        private static string GetJWTToken(string Email, int UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", Email),
                    new Claim("UserId",UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string EncryptPassword(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return null;
                }
                else
                {
                    byte[] b = Encoding.ASCII.GetBytes(password);
                    string encrypted = Convert.ToBase64String(b);
                    return encrypted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ForgetPassword(string Email)
        {
            try
            {
                var result = fundoo.Users.FirstOrDefault(u => u.Email == Email);
                if (result == null)
                {
                    return false;
                }
                //Add message Queue
                MessageQueue queue;
                if (MessageQueue.Exists(@".\Private$\FundooQueue"))
                {
                    queue = new MessageQueue(@".\Private$\FundooQueue");
                }
                else
                {
                    queue = MessageQueue.Create(@".\Private$\FundooQueue");
                }
                Message myMessage = new Message();
                myMessage.Formatter = new BinaryMessageFormatter();
                myMessage.Body = GetJWTToken(Email, result.UserId);
                queue.Send(myMessage);
                Message message = queue.Receive();
                message.Formatter = new BinaryMessageFormatter();
                EmailService.SendMail(Email, myMessage.Body.ToString());
                queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);
                queue.BeginReceive();
                queue.Close();


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==
                    MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
                // Handle other sources of MessageQueueException.
            }

        }
        //GENERATE TOKEN WITH EMAIL
        public string GenerateToken(string email)
        {
            if (email == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    
                      new Claim("Email",email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public void ResetPassword(string Email, string Password, string cPassword)
        {
            try
            {
                User user = new User();
                var result = fundoo.Users.FirstOrDefault(a => a.Email == Email);
                if (result != null)
                {
                    result.Password = Password;
                    result.cPassword = cPassword;
                    fundoo.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }


        }



    }
}
   
    

    




