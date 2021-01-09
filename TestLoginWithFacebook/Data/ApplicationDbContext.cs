//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestLoginWithFacebook.Data.ModelsData;

namespace TestLoginWithFacebook.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CictyWorks>().HasKey(cw => new { cw.IdArtis, cw.IdCity });
            base.OnModelCreating(builder);
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Citys> Citys { get; set; }
        public DbSet<CictyWorks>  CictyWorks { get; set; }
        public DbSet<DaysWork>   DaysWorks { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<ProfileArtist> ProfileArtists { get; set; }
     
    }

    //public class MyIdentityUserClaim<TKey> : IdentityUserClaim<TKey> where TKey : IEquatable<TKey>
    //{
    //    public  Enums.Roles ClaimType
    //    {
    //        get => ClaimType;
    //        set => ClaimType = value;
    //    }
    //}
}
