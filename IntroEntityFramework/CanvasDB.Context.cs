﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IntroEntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CanvasScriptsDBEntities : DbContext
    {
        public CanvasScriptsDBEntities()
            : base("name=CanvasScriptsDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ScriptsSet> ScriptsSet { get; set; }
        public virtual DbSet<UserNamesSet> UserNamesSet { get; set; }
        public virtual DbSet<UsersSet> UsersSet { get; set; }
    }
}
