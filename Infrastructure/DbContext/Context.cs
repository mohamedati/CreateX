
using Application.Common.Interfaces;
using Createx.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.DbContext
{
    public  class Context:IdentityDbContext<ApplicationUser>,IAppDbContext
    {


        public Context(DbContextOptions<Context> options):base(options) 
        {
        }

     
        public virtual DbSet<Branch> Branches { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<Currency> Currencies { get; set; }

        public virtual DbSet<District> Districts { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductDetail> ProductDetails { get; set; }


        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }

        public virtual DbSet<Store> Stores { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
       => base.SaveChangesAsync(cancellationToken);

     
    }
}
