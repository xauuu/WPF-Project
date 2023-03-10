//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StoreManagement.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            this.BillDetails = new HashSet<BillDetail>();
        }
    
        public int BillID { get; set; }
        public System.DateTime BillDate { get; set; }
        public int CashierID { get; set; }
        public double TotalPrice { get; set; }
        public string DiscountCode { get; set; }
        public double LastTotalPrice { get; set; }
    
        public virtual User User { get; set; }
        public virtual CodePromotion CodePromotion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
