using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETrading_MVC.Models
{
    public class TransactionVM
    {
        public short TransactionId { get; set; }
        public Nullable<short> NoOfShares { get; set; }
        public string TransactionType { get; set; }
        public Nullable<decimal> SharePrice { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<short> CustomerId { get; set; }
        public Nullable<short> CompanyId { get; set; }
    }
}