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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class yazleyEntities : DbContext
    {
        public yazleyEntities()
            : base("name=yazleyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<boutique> boutiques { get; set; }
        public virtual DbSet<brand> brands { get; set; }
        public virtual DbSet<calibre> calibres { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<thewatchguide> thewatchguides { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Inquiry> Inquiries { get; set; }
    }
}
