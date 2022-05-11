using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ETrading_MVC.Models
{
    public class BusinessOwnerLoginVM
    {
        [Required(ErrorMessage = "Enter Company Id")]
        public short id { get; set; }
        
        [Required(ErrorMessage = "Enter Email Id")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}