﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Compuwreck_001.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CompuwreckEntities : DbContext
    {
        public CompuwreckEntities()
            : base("name=CompuwreckEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<County> Counties { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Shipwreck> Shipwrecks { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Reference> References { get; set; }
    }
}
