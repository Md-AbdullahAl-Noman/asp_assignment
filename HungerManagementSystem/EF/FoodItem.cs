//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HungerManagementSystem.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class FoodItem
    {
        public int Item_Id { get; set; }
        public int Collect_Request_Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public System.DateTime ExpiryDate { get; set; }
    
        public virtual CollectRequest CollectRequest { get; set; }
    }
}
