using Microsoft.EntityFrameworkCore;
using ProductDataAL.Data.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataAL.Data.Context
{
    public class ProductContext:DbContext
    {
        public DbSet<Product> products { get; set; }
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }

    }
}
