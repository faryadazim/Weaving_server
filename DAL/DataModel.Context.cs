﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class db_weavingEntities : DbContext
    {
        public db_weavingEntities()
            : base("name=db_weavingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<BorderQuality> BorderQuality { get; set; }
        public virtual DbSet<BorderSize> BorderSize { get; set; }
        public virtual DbSet<employeeDesignation> employeeDesignation { get; set; }
        public virtual DbSet<employeeList> employeeList { get; set; }
        public virtual DbSet<Modules> Modules { get; set; }
        public virtual DbSet<PagePermission> PagePermission { get; set; }
        public virtual DbSet<Pages> Pages { get; set; }
        public virtual DbSet<weavingUnit> weavingUnit { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<LoomList> LoomList { get; set; }
        public virtual DbSet<grayProductList> grayProductList { get; set; }
    }
}
