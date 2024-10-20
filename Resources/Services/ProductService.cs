using Newtonsoft.Json;
using Resources.Enums;
using Resources.Models;

namespace Resources.Services;

public class ProductService : IProductService

{
    private List<Product> _productList = [];
    private static readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "file.json");
    private readonly FileService _fileService = new(_filePath);



    #region CRUD
    public ResultStatus AddToList(Product product)
    {
        try
        {
            GetProductsFromFile();

            if (_productList.Any(p => p.Id == product.Id))
            {
                return ResultStatus.Exists;
            }

            _productList.Add(product);

            string json = JsonConvert.SerializeObject(_productList, Formatting.Indented);
            var result = _fileService.SaveToFile(json);
            if (result)
            {
                return ResultStatus.Success;
            }

            return ResultStatus.SuccessWithErrors;
        }
        catch
        {
            return ResultStatus.Failed;
        }
    }

    public IEnumerable<Product> GetAllProducts()
    {
        GetProductsFromFile();
        return _productList;
    }

    public ResultStatus DeleteProduct(string id)
    {
        try
        {
            GetProductsFromFile();

            var product = GetProductByID(id);
            if (product == null)
            {
                return ResultStatus.NotFound;
            }
            _productList.Remove(product);

            var json = JsonConvert.SerializeObject(_productList);
            var result = _fileService.SaveToFile(json);
            if (result)
            {
                return ResultStatus.Success;
            }
            return ResultStatus.SuccessWithErrors;

        }
        catch
        {
            return ResultStatus.Failed;
        }
    }

    public ResultStatus DeleteAllProducts()
    {
        GetProductsFromFile();

        if (_productList != null)
        {
            _productList.Clear();
            string json = JsonConvert.SerializeObject(_productList, Formatting.Indented);
            var result = _fileService.SaveToFile(json);

            if (result)
            {
                return ResultStatus.Success;
            }
            else
            {
                return ResultStatus.SuccessWithErrors;
            }
        }

        return ResultStatus.SuccessWithErrors;
    }

    public ResultStatus UpdateProduct(Product updatedProduct)
    {
        try
        {
            GetProductsFromFile();

            var initialProduct = GetProductByID(updatedProduct.Id);

            if (initialProduct == null)
            {
                return ResultStatus.NotFound;
            }

            initialProduct.Name = updatedProduct.Name;
            initialProduct.Price = updatedProduct.Price;

            string json = JsonConvert.SerializeObject(_productList, Formatting.Indented);
            var result = _fileService.SaveToFile(json);
            if (result)
            {
                return ResultStatus.Success;
            }
            return ResultStatus.SuccessWithErrors;
        }
        catch
        {
            return ResultStatus.Failed;
        }
    }

    #endregion

    #region GetProduct Methods
    public Product GetProductByName(string? name)
    {
        GetProductsFromFile();
        try
        {
            var product = _productList.FirstOrDefault(p => p.Name == name);
            return product ?? null!;
        }
        catch
        {
            return null!;
        }
    }
    public Product GetProductByID(string? id)
    {
        GetProductsFromFile();
        try
        {
            var product = _productList.FirstOrDefault(p => p.Id == id);
            return product ?? null!;
        }
        catch
        {
            return null!;
        }
    }
    public void GetProductsFromFile()
    {
        try
        {
            string content = _fileService.LoadFromFile();
            if (!string.IsNullOrEmpty(content))
            {
                _productList = JsonConvert.DeserializeObject<List<Product>>(content)!;
            }
        }
        catch
        {

        }
    }

    #endregion
  
  
    #region Service Controls
    public bool IsUniqueID(string id)
    {
        GetProductsFromFile();
        return !_productList.Any(p => p.Id == id);
    }

    #endregion
}



