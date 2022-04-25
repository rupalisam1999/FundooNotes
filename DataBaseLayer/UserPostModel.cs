using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBaseLayer
{
    public class UserPostModel
    {
       

        //[Required]
        //[RegularExpression("^[A-Z]{1}[a-z]{5,}$, ErrorMessage = name start with cap and has minimum 5 charactor")]
        public string FirstName { get; set; }

        //[Required]
        //[RegularExpression("^[A-Z]{1}[a-z]{5,}$, ErrorMessage = name start with cap and has minimum 5 charactor")]
        public string LastName { get; set; }

        public string Adress { get; set; }

        //[Required]
        //[RegularExpression("[a-z]{3,}[1-9]{1,4}[@][a-z]{4,}[.][a-z]{3,}$, ErrorMessage = email must be in small latter and has minimum 1 special charactor")]
        public string Email { get; set; }

        //[Required]
        //[RegularExpression("^[A-Z]{1}[a-z]{4,}[@]{1}[1-9]{4}$, ErrorMessage = password must contain minimun 8 charactor and having minimum 1 special charactor")]
        public string Password { get; set; }

    }
}
