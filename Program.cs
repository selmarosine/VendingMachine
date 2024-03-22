using System.Security;

class User
{
    public string userName;

    public void GetUserName()
    {
        Console.WriteLine("Please enter your name: ");
        
        var userInput = Console.ReadLine();
        userName = userInput;
        Console.WriteLine($"Welcome {userName} !");
    }
}
class Bank
{
    public int userMoney;
    public void GetUserMoney()
    {
        bool validInput = false;
        Console.WriteLine("Enter money: ");
        while (!validInput)
        {
            var userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int money))
            {
                userMoney = money;
                break;
            }
            else
            {
                Console.WriteLine($"{userInput} is not a valid number. Try again: ");
            }   
        }
    }
    public void Transaction(decimal amount)
    {
        userMoney -= (int)amount;
    }
}

class Inventory
{
    private Dictionary<string, decimal> items;
    private List<string> UserSelected;
    public decimal Total = 0;
    public Inventory()
    {
        UserSelected = new List<string>();
    }
    public void VendingItems()
    {
        items = new Dictionary<string, decimal>();

        items["Fanta"] = 10;
        items["Sprite"] = 10;
        items["Hallonsoda"] = 8;
        
        items["Chocolate"] = 11;
        items["Peanuts"] = 12;
        items["Cookies"] = 14;
        
        items["Onion chips"] = 13;
        items["Salty chips"] = 12;
        items["Cheesy chips"] = 14;
    }

    public void SelectItems()
    {
        Console.WriteLine("Select what you want (for multiple choices, please separate with space):");

        int itemNumber = 1;
        foreach (var item in items)
        {
            Console.WriteLine($"Type {itemNumber} for {item.Key} - Price: {item.Value}");
            itemNumber++;
        }
        
        Console.WriteLine("Your choices: ");
        
        bool validInput = false;
        while (!validInput)
        {
            string userInput = Console.ReadLine();
            string[] choices = userInput.Split(' ');
        foreach (string choice in choices)
        {
            if (int.TryParse(choice.Trim(), out int itemIndex) && itemIndex >= 1 && itemIndex <= items.Count)
            {
                string selectedKey = items.Keys.ElementAt(itemIndex - 1);
                UserSelected.Add(selectedKey);
                validInput = true;
            }
            else
            {
                Console.WriteLine($"Invalid choice: {choice.Trim()} Try again (for multiple choices, use space between)");
            }
        }
        }
    }

    public void ShowUserSelected()
    {
        Console.WriteLine("Your selected items:");
        foreach (var item in UserSelected)
        {
            Console.WriteLine(item);
            Total += items[item];
        }
    }

    public void ClearSelection()
    {
        UserSelected.Clear();
    }
}

class Program
{
    static void Main(string[] args)
    {
        //Skapa användare
        User user = new User();
        user.GetUserName();
        
        //Lägga in pengar
        Bank bank = new Bank();
        bank.GetUserMoney();

        //Välja varor
        Inventory inventory = new Inventory();
        inventory.VendingItems();
        bool makeNewChoice = true;

        while (makeNewChoice)
        { 
            inventory.SelectItems();
            inventory.ShowUserSelected(); 
            
            Console.WriteLine($"Your total is: {inventory.Total}, and your current bank balance is: {bank.userMoney}.");

        if (inventory.Total <= bank.userMoney)
        {
            Console.WriteLine("Do you wish to proceed? Type YES or NO");
            string checkout = Console.ReadLine().ToUpper();
            string yes = "YES";
            string no = "NO";
            
            if (checkout == yes)
            {
                bank.Transaction(inventory.Total);
                Console.WriteLine($"Transaction successful. Your new bank balance is: {bank.userMoney}");
                Console.WriteLine($"Goodbye, {user.userName}.");
                makeNewChoice = false;
            }
            if (checkout == no)
            {
                Console.WriteLine("Do you wish to make a new choice? Type YES or NO");
                string askNewChoice = Console.ReadLine().ToUpper();

                if (askNewChoice == yes)
                {
                    Console.WriteLine("new choice");
                    inventory.ClearSelection();
                }
                else
                {
                    Console.WriteLine("no to new choice");
                    makeNewChoice = false;
                }
            }
            if (checkout != yes && checkout != no)
            {
                Console.WriteLine($"Invalid input: {checkout}. Please try again");
            }
        }
        else
        {
            Console.WriteLine("You don't have enough money... Bye.");
            makeNewChoice = false;
        }
        }
    }
}