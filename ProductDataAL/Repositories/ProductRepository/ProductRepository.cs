using ProductDataAL.Data.Context;
using ProductDataAL.Data.DataBaseModels;
using ProductDataAL.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataAL.Repositories.ProductRepository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context) : base(context)
        {
            _context = context;
        }

    }
}
