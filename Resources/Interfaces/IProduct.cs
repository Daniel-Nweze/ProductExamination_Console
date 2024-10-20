namespace Resources.Models
{
    public interface IProduct
    {
        string Id { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
    }
}