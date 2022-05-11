using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ETrading_MVC.Models
{
    public class CustomerVM
    {
        public short CustomerId { get; set; }
        [Required(ErrorMessage = "Enter CustomerName")]
        [RegularExpression("[a-zA-Z\\s.]{5,20}", ErrorMessage = "Name can contain alphabets only (minimun length 5 ,maximum length:20) ")]
        public string CustomerName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter Customer Password")]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Enter Customer Email")]
        public string CustomerEmail { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Format")]
        public Nullable<System.DateTime> DOB { get; set; }
        [RegularExpression("\\d{10}", ErrorMessage = "Invalid Mobile Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Enter SharePrice")]
        [Range(0, 100000000)]
        public Nullable<decimal> AccountBalance { get; set; }
        [Required(ErrorMessage = "Enter Customer Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
    }
}