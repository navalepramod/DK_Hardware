using DK_Hardware.DAL;
using NLog;

class Program
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
    static void Main(string[] args)
    {

        Logger.Info("Application started...");
        Console.WriteLine("Application started...");
        
        // Call Method for Data Mapping
        ProductDetails Prod_Matched = new ProductDetails();
        Prod_Matched.GetProductDetails(Logger);

        Logger.Info("Application Ended...");
        Console.WriteLine("Application Ended...");
    }



}