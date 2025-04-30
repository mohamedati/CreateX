using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Createx.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public  interface IAppDbContext
    {
        DbSet<Branch> Branches { get; set; }

         DbSet<Category> Categories { get; set; }

         DbSet<City> Cities { get; set; }

         DbSet<Company> Companies { get; set; }

         DbSet<Currency> Currencies { get; set; }

         DbSet<District> Districts { get; set; }

         DbSet<Order> Orders { get; set; }

         DbSet<Product> Products { get; set; }

         DbSet<ProductDetail> ProductDetails { get; set; }


         DbSet<Customer> Customers { get; set; }
         DbSet<ProductType> ProductTypes { get; set; }

         DbSet<Store> Stores { get; set; }

        public  Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
    }
}
