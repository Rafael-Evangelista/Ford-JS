﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FordAPI.App_Data
{
    using FordAPI.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FordEntities : DbContext
    {
        public FordEntities()
            : base("name=FordEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FilesUpload> FilesUpload { get; set; }
        public virtual DbSet<Funcionarios> Funcionarios { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Sorteios> Sorteios { get; set; }
        public virtual DbSet<Veiculos> Veiculos { get; set; }
        public virtual DbSet<Sorteados_Bckp> Sorteados_Bckp { get; set; }
        public virtual DbSet<VeiculosSorteados_Bckp> VeiculosSorteados_Bckp { get; set; }
        public virtual DbSet<Eventos> Eventos { get; set; }
    }
}
