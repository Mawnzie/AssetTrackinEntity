// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using System.Linq;




AppDbContext assetContext = new AppDbContext();
List<Asset> list = new List<Asset>();





while (true)
{

    Console.WriteLine("\nPick an option:\n");
    Console.WriteLine("(1) Show assets (sort by date or project)");
    Console.WriteLine("(2) Add new asset.");
    Console.WriteLine("(3) Quit");




    char input = Console.ReadKey().KeyChar;




optionscreen:
    switch (input)
    {
        case '1':
            list = assetContext.Products.ToList();
            var sortedProducts = list.OrderBy(a => a.PurchaseDate).ThenBy(a => a.Brand).ToList();
            Console.WriteLine("\n");
            foreach (Asset x in sortedProducts)
            {
                if (x.PurchaseDate < DateTime.Now.AddYears(-3))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Id: {x.Id}, Type: {x.Type}, Brand: {x.Brand}, PurchaseDate: {x.PurchaseDate}");
                    Console.ResetColor();

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Id: {x.Id}, Type: {x.Type} Brand: {x.Brand}, PurchaseDate: {x.PurchaseDate}");
                    Console.ResetColor();
                }

            }
            break;
        case '2':
        enterAsset:
            Console.WriteLine("Enter asset type (Laptop/MobilePhone):");
        
            string stringAsset = Console.ReadLine();
            if (Enum.TryParse<AssetType>(stringAsset, out AssetType asset))
            {
                Console.WriteLine($"Parsed successfully: {asset}");
            }
            else
            {
                Console.WriteLine("Invalid input.");
                goto enterAsset;
            }



            Console.WriteLine("Enter brand:");
            string brand = Console.ReadLine();


            Console.WriteLine("Enter Purchase date (MM/dd/yyyy):");
            string enteredString = Console.ReadLine();
            DateTime purchaseDate = DateTime.Parse(enteredString);


            Asset product = new Asset(asset,brand,purchaseDate);

            
            assetContext.Add(product);
            assetContext.SaveChanges();
            
            break;

        case '3':

            break;

        default:
            break;
    }
        
}



Console.WriteLine("Thank you for using ToDoLy. Press any key to exit.");
Console.ReadKey();


public class AppDbContext : DbContext
{
    public DbSet<Asset> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=localhost,1433;Database=MyEFAppDb;User Id=sa;PASSWORD=Str0ngPassw0rd!;TrustServerCertificate=True;");
}
