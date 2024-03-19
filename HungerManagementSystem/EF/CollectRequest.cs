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
    
    public partial class CollectRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CollectRequest()
        {
            this.Distributions = new HashSet<Distribution>();
            this.FoodItems = new HashSet<FoodItem>();
        }
    
        public int Request_Id { get; set; }
        public int Restaurant_Id { get; set; }
        public System.DateTime Requested_Time { get; set; }
        public System.DateTime Preserve_Time { get; set; }
        public string Status { get; set; }
        public Nullable<int> EmployeeID { get; set; }
    
        public virtual Restaurant Restaurant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Distribution> Distributions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodItem> FoodItems { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
