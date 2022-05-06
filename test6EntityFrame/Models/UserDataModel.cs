using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class UserDataModel
    {


        //string email;
        //string password; string userName; string phoneNumber;



         

        public string userName { get; set; }
        public string password { get; set; }


        public string phoneNumber { get; set; }

        public string email { get; set; }
    }
}