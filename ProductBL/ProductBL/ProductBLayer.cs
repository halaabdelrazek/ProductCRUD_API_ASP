using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductBL.DTOs.Product;
using ProductDataAL.Data.Context;
using ProductDataAL.Data.DataBaseModels;
using ProductDataAL.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBL.ProductBL
{
    public class ProductBLayer :  IProductBLayer
    {
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;
        private readonly ProductContext context;

        public ProductBLayer(IProductRepository productRepo, IMapper mapper, ProductContext _context)
        {
            this._productRepo = productRepo;
            _mapper = mapper;
            context = _context;
        }

        public ActionResult<IEnumerable<ProductReadDTO>> GetProducts()
        {
            var productFromDB = _productRepo.GetAll();
            return _mapper.Map<List<ProductReadDTO>>(productFromDB);

        }


        public ProductReadDTO PostProduct(ProductWriteDTO _product)
        {
            var productToAdd = _mapper.Map<Product>(_product);

            productToAdd.ProductId = Guid.NewGuid();
            context.products.Add(productToAdd);
            context.SaveChanges();


            return _mapper.Map<ProductReadDTO>(productToAdd);
        }


        public ProductReadDTO GetProductById(Guid id)
        {
            var productFromDB = context.products.FirstOrDefault(p => p.ProductId == id);

            return _mapper.Map<ProductReadDTO>(productFromDB);
        }

        public ProductReadDTO DeleteProduct(Guid id)
        {
            var productDeleted = _productRepo.GetById(id);

            _productRepo.Delete(id);
            _productRepo.SaveChanges();

            return _mapper.Map<ProductReadDTO>(productDeleted);

        }

        public int PutProduct(Guid id, ProductWriteDTO _product)
        {

            var productToEdit = _productRepo.GetById(id);
            if (productToEdit is null)
            {
                // retun 0 to product not found

                return 0;
            }


            _mapper.Map(_product, productToEdit);


            _productRepo.Update(productToEdit);
            _productRepo.SaveChanges();

            // retun 1 to update Product done

            return 1;
        }

        public void AsssignImageToProduct(Guid id, string imagePath)
        {
            var product = _productRepo.GetById(id);

            product.Image = imagePath;

            _productRepo.SaveChanges();




        }
    }
}
