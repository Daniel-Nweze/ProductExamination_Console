



using Resources.Enums;
using Resources.Models;
using Resources.Services;

namespace MainApp.Menus;

public class ProductMenu
{
    private readonly ProductService _productService = new ProductService();

    #region CRUD

    internal void AddProduct()
    {
        Product product = new Product();

        Console.Clear();
        Console.WriteLine("\nLägg Till Produkt\n");

        product.Name = GetProductName();
        product.Price = GetProductPrice();
        product.Id = GetProductId();

        var result = _productService.AddToList(product);
        HandleAddProductResult(result);
    }

    internal void ViewAllProducts()
    {
        Console.Clear();
        Console.WriteLine("\nProdukt Vy\n");
        if (ProductListIsEmpty())
        {
            return;
        }

        ListAllProducts();
    }

    internal void ViewSingleProduct()
    {
        Console.Clear();
        Console.WriteLine("\nProduktvy\n");

        if (ProductListIsEmpty())
        {
            return;
        }


        Console.Write("Ange produktens ID: ");
        var id = Console.ReadLine();

        var product = _productService.GetProductByID(id);

        DisplayProductDetails(product);
    }

    internal void UpdateProduct()
    {
        Console.Clear();
        Console.WriteLine("\nUppdatera produkt");
        ListAllProducts();

        if (ProductListIsEmpty())
        {
            return;
        }

        Console.WriteLine("\nUppdatera Produkt\n");
        Console.Write("Ange produktens ID: ");
        var currentId = Console.ReadLine() ?? "";

        if (!string.IsNullOrEmpty(currentId))
        {
            var product = _productService.GetProductByID(currentId);
            if (product is null)
            {
                ErrorMessage("Angivet ID existerar inte.");
                return;
            }

            UpdateProductName(product);
            UpdateProductPrice(product);

            var result = _productService.UpdateProduct(product);
            HandleUpdatedProductResult(result);
        }
        else
            ErrorMessage("Fältet får inte vara tomt.");
    }

    internal void DeleteProduct()
    {
        Console.Clear();
        ListAllProducts();

        if (ProductListIsEmpty())
        {
            return;
        }

        while (true)
        {
            Console.WriteLine("\nRadera Produkt\n");

            Console.Write("Ange produktens ID: ");
            var name = Console.ReadLine();

            if (!string.IsNullOrEmpty(name))
            {
                var result = _productService.DeleteProduct(name);
                HandleDeleteProductResult(result);
                return;
            }
            else
                ErrorMessage("\nFältet får inte vara tomt");
        }

    }

    internal void DeleteAllProducts()
    {
        Console.Clear();
        if (ProductListIsEmpty())
        {
            return;
        }

        Console.WriteLine("Vill du radera alla produkter? ja/nej");
        var delete = Console.ReadLine() ?? "".ToLower();
        if (delete is not "ja")
        {
            Console.WriteLine("Produkterna har inte raderats.");
            return;
        }
        var result = _productService.DeleteAllProducts();
        HandleDeleteAllProductsResult(result);
    }


    #endregion



    #region Result Handling
    private void HandleAddProductResult(ResultStatus result)
    {
        string message = result switch
        {
            ResultStatus.Success => "\nProdukten har registrerats.",
            ResultStatus.Exists => "\nProdukt med samma ID existerar redan.",
            ResultStatus.Failed => "\nDet uppstod ett fel vid skapandet av produkten.",
            ResultStatus.SuccessWithErrors => "\nProdukten har registrerats. Men produklistan sparades inte.",
            _ => "\nOkänt fel."
        };

        Console.WriteLine(message);

        if (result != ResultStatus.Exists)
            return;
    }

    private static void HandleDeleteProductResult(ResultStatus result)
    {
        string message = result switch
        {
            ResultStatus.Success => "Produkten har raderats.",

            ResultStatus.Failed => "Något gick fel när produkten skulle raderas.",

            ResultStatus.NotFound => "Det angivna ID:et kunde inte hittas.",

            ResultStatus.SuccessWithErrors => "Produkten har raderats. Men produktlistan sparades inte.",

            _ => "Okänt resultat."
        };

        Console.WriteLine(message);
    }

    private static void HandleDeleteAllProductsResult(ResultStatus result)
    {
        string message = result switch
        {
            ResultStatus.Success => "Alla produkter har raderats.",

            ResultStatus.Failed => "Något gick fel när produkterna skulle raderas.",

            ResultStatus.SuccessWithErrors => "Produkterna har raderats. Men produktlistan sparades inte.",

            _ => "Okänt fel."
        };

        Console.WriteLine(message);
    }

    private static void HandleUpdatedProductResult(ResultStatus result)
    {
        string message = result switch
        {
            ResultStatus.Success => "\nProdukten har uppdaterats.",

            ResultStatus.Failed => "\nUppdateringen misslyckades.",

            ResultStatus.NotFound => "\nDet angivna ID:et kunde inte hittas.",

            ResultStatus.SuccessWithErrors => "\nProdukten har uppdaterats. Men produktlistan sparades inte.",

            _ => "\nOkänt fel"
        };

        Console.WriteLine(message);
    }

    #endregion

    #region Product Input Methods
    private static string GetProductName()
    {
        while (true)
        {
            Console.Write("\nAnge produktens namn: ");
            string name = Console.ReadLine() ?? "";

            if (!string.IsNullOrEmpty(name))
                return name;

            ErrorMessage("Fältet får inte var tomt.");
        }
    }

    private static decimal GetProductPrice()
    {
        while (true)
        {
            Console.Write("Ange produktens pris: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal price) && price > 0)
            {
                return price;
            }
            ErrorMessage("\nDu måste ange ett positivt belopp.\n");
        }
    }

    private string GetProductId()
    {
        while (true)
        {
            Console.Write("Ange produktens ID: ");
            var id = Console.ReadLine() ?? "";
            if (!string.IsNullOrEmpty(id))
            {
                if (_productService.IsUniqueID(id))
                    return id;
                else
                    HandleAddProductResult(ResultStatus.Exists);
            }
            else
                ErrorMessage("Fältet får inte vara tomt.");
        }
    }

    private static void UpdateProductName(Product product)
    {
        while (true)
        {
            Console.Write($"\nAnge produktens namn: ");
            var newProductName = Console.ReadLine() ?? "";
            if (!string.IsNullOrEmpty(newProductName))
            {
                product.Name = newProductName;
                break;
            }
            ErrorMessage("Fältet får inte vara tomt.");
        }

    }

    private static void UpdateProductPrice(Product product)
    {
        while (true)
        {
            Console.Write($"\nAnge produktens pris: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal newPrice) && newPrice > 0)
            {
                product.Price = newPrice;
                break;
            }
            ErrorMessage("Du måste ange ett positivt belopp.");
        }
    }

    #endregion

    #region Product Utility Methods
    private void DisplayProductDetails(Product product)
    {
        if (product is not null)
        {
            Console.WriteLine($"\nVydetaljer för produkt: {product.Id}\n");
            Console.WriteLine($"Produkt ID: {product.Id}");
            Console.WriteLine($"Produktnamn: {product.Name}");
            Console.WriteLine($"Pris: {product.Price:C}");
            Console.WriteLine("-----------------------------------------------------------------------------------");
        }
        else
        {
            Console.WriteLine("\nIngen produkt hittades.");
        }
    }

    public static void ErrorMessage(string message)
    {
        Console.WriteLine("\n" + message);
    }

    private bool ProductListIsEmpty()
    {
        var productList = _productService.GetAllProducts();
        if (!productList.Any())
        {
            Console.WriteLine("Inga sparade produkter.");
            return true;
        }
        return false;
    }

    private void ListAllProducts()
    {
        var productList = _productService.GetAllProducts();

        foreach (Product product in productList)
        {
            DisplayProductDetails(product);
        }
    }

    internal void ExitApplication()
    {
        Environment.Exit(0);
    }

    #endregion

}
