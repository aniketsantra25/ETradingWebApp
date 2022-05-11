//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ETradingEF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public short TransactionId { get; set; }
        public Nullable<short> NoOfShares { get; set; }
        public string TransactionType { get; set; }
        public Nullable<decimal> SharePrice { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<short> CustomerId { get; set; }
        public Nullable<short> CompanyId { get; set; }
    
        public virtual BusinessOwner BusinessOwner { get; set; }
        public virtual CustomerDetail CustomerDetail { get; set; }
    }
}
