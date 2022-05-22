using ProductDataAL.Data.DataBaseModels;
using ProductDataAL.Repositories.GenericRepository;

namespace ProductDataAL.Repositories.ProductRepository
{
    public interface IProductRepository: IGenericRepository<Product>
    {
    }
}