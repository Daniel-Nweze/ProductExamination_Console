namespace MainApp.Menus;
// när allt är klart kan du trycka ctrl. för att extrahera interfaces. Behöver inte skapa och skriva ut varenda. Det passar mer i gruppprojekt för att alla ska kunna se "mallen" innan.
internal class UserMenu
{

    private static readonly ProductMenu _interactionMenu = new();
    private static readonly Menu _menu = new Menu();
    public void MainMenu()
    {
        Console.Clear();
        _menu.DisplayMenuOptions();

        if (int.TryParse(Console.ReadLine(), out int option))
        {
            if (_menu.MenuActions.TryGetValue(option, out Action? action))
                action.Invoke();

            else
                Console.WriteLine("\nIncorrect input. Please try again... ");
        }

        else
        {
            Console.WriteLine("\nIncorrect input. Please try again... ");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private class Menu
    {
        public void DisplayMenuOptions()
        {
            Console.WriteLine("\nHuvudmeny\n");
            Console.WriteLine("[1] Lägg till produkt");
            Console.WriteLine("[2] Se alla produkter");
            Console.WriteLine("[3] Se en produkt");
            Console.WriteLine("[4] Uppdatera en produkt");
            Console.WriteLine("[5] Radera en produkt");
            Console.WriteLine("[6] Radera alla produkter");
            Console.WriteLine("[7] Avsluta program");
            Console.Write("\nVälj ett alternativ (1-5):  ");
        }

        public Dictionary<int, Action> MenuActions => new Dictionary<int, Action>
        {
            {1, _interactionMenu.AddProduct},
            {2, _interactionMenu.ViewAllProducts},
            {3, _interactionMenu.ViewSingleProduct},
            {4, _interactionMenu.UpdateProduct},
            {5, _interactionMenu.DeleteProduct},
            {6, _interactionMenu.DeleteAllProducts},
            {7, _interactionMenu.ExitApplication},
        };

    }


}
