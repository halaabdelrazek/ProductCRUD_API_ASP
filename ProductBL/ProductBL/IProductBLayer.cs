using Microsoft.AspNetCore.Mvc;
using ProductBL.DTOs.Product;

namespace ProductBL.ProductBL
{
    public interface IProductBLayer
    {
        void AsssignImageToProduct(Guid id, string imagePath);
        ProductReadDTO DeleteProduct(Guid id);
        ProductReadDTO GetProductById(Guid id);
        ActionResult<IEnumerable<ProductReadDTO>> GetProducts();
        ProductReadDTO PostProduct(ProductWriteDTO _product);
        int PutProduct(Guid id, ProductWriteDTO _product);
    }
}