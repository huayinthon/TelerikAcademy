//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Transactions
{
    using System;
    using System.Collections.Generic;
    
    public partial class CardAccount
    {
        public CardAccount()
        {
            this.TransactionsHistories = new HashSet<TransactionsHistory>();
        }
    
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CardPIN { get; set; }
        public decimal CardCash { get; set; }
    
        public virtual ICollection<TransactionsHistory> TransactionsHistories { get; set; }
    }
}
