using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETrading_MVC.Models
{
    public class SellSharesVM
    {
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Enter Number of shares you want to sell")]
        [Range(1, 20000, ErrorMessage = "Value must be greater than zero")]
        public short numberofShares { set; get; }
        public short companyid { set; get; }
    }
}