//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Yazley_watch_boutique
{
    using System;
    using System.Collections.Generic;
    
    public partial class calibre
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public calibre()
        {
            this.products = new HashSet<product>();
        }
    
        public decimal calibreId { get; set; }
        public string brand { get; set; }
        public string image { get; set; }
        public string reference { get; set; }
        public string movement { get; set; }
        public string display { get; set; }
        public string @base { get; set; }
        public string power_reserve { get; set; }
        public Nullable<decimal> jewells { get; set; }
        public string frequency { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product> products { get; set; }
    }
}
