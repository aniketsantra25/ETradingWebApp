using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ETrading_MVC.Models
{
    public class BusinessOwnerVM
    {

        public short CompanyId { get; set; }
        [Required(ErrorMessage = "Enter Company Name")]
        [RegularExpression("[a-zA-Z\\s.]{5,20}", ErrorMessage = "Company Name can contain alphabets only (minimun length 5 ,maximum length:20) ")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Enter OwnerName")]
        public string OwnerName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Enter OwnerEmail")]
        public string OwnerEmail { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter CompanyPassCode")]
        public string CompanyPasscode { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Format")]
        public System.DateTime DateOfIncorporation { get; set; }
        [RegularExpression("\\d{10}", ErrorMessage = "Invalid Mobile Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Choose CategoryId")]
        public Nullable<short> CategoryId { get; set; }
        [Required(ErrorMessage = "Enter CompanyAddress")]
        public string CompanyAddress { get; set; }
        [Required(ErrorMessage = "Enter SharePrice")]
        [Range(0, 1000000)]
        public int SharePrice { get; set; }
        [Required(ErrorMessage = "Enter NoOfShares")]
        [Range(0,1000000)]
        public short NoOfShares { get; set; }
    }
}