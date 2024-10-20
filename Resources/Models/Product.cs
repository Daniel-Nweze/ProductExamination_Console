namespace Resources.Models;

// Denna klass genererar en produkt med ett unikt ID, namn och ett pris.
public class Product : IProduct
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}
