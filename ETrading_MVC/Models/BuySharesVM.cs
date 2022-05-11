using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETrading_MVC.Models
{
    public class BuySharesVM
    {
        public string CompanyName { get; set; }
        public short companyId { set; get; }

        [Required(ErrorMessage = "Enter Number of shares you want to buy")]
        [Range(1, 20000, ErrorMessage = "Value must be greater than zero")]
        public int shareQuantity { set; get; }
    }
}