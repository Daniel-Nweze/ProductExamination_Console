namespace Resources.Tests
{
    public interface IProductServiceTests
    {
        void AddToList__Should__ReturnSuccess__WhenProductIsAddedToList();

        void DeleteAllProducts__Should__ReturnSuccess__WhenAllProductsAreDeletedFromList();
    }
}