//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BankingApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public int transID { get; set; }
        public Nullable<System.DateTime> transDate { get; set; }
        public Nullable<decimal> moneyIn { get; set; }
        public Nullable<decimal> moneyOut { get; set; }
        public Nullable<decimal> balance { get; set; }
        public Nullable<int> transcard { get; set; }
    
        public virtual Card Card { get; set; }
    }
}
