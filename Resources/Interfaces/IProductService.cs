using Resources.Enums;
using Resources.Models;

namespace Resources.Services
{
    public interface IProductService
    {
        ResultStatus AddToList(Product product);
        ResultStatus DeleteAllProducts();
        ResultStatus DeleteProduct(string id);
        IEnumerable<Product> GetAllProducts();
        Product GetProductByID(string? id);
        Product GetProductByName(string? name);
        void GetProductsFromFile();
        bool IsUniqueID(string id);
        ResultStatus UpdateProduct(Product updatedProduct);
    }
}