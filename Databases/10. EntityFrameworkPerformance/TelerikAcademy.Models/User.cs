//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TelerikAcademy.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public int UserID { get; set; }
        public string Userame { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public Nullable<int> GroupId { get; set; }
    
        public virtual Group Group { get; set; }
    }
}
