using DataBaseLayer;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entity;
using RepositoryLayer.FundooNotesContext;
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
        public UserRL(FundooContext fundoo)
        {
            this.fundoo = fundoo;
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
                user1.Password = user.Password;
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
                var result = fundoo.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);
                if(result == null)
                {
                    return null;
                }
                return Email;
                //string Password = Password;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        // Generate JWT Token
        public static string GetJWTToken(string Email,string UserId)
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
    }
}

