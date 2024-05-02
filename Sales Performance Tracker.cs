using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Salesperson
{
    // Properties to store the salesperson's information
    public string Name { get; }
    public string SocialSecurityNumber { get; }
    public string District { get; }
    public int NumberSoldArticles { get; }

    // Constructor to initialize a salesperson with specified details
    public Salesperson(string name, string socialSecurityNumber, string district, int numberSoldArticles)
    {
        Name = name;
        SocialSecurityNumber = socialSecurityNumber;
        District = district;
        NumberSoldArticles = numberSoldArticles;
    }
}

class Program
{
    static void Main()
    {
        // Ask the user how many salespersons to register
        Console.Write("How many salespersons do you want to register? ");
        int numberOfSalespersons = int.Parse(Console.ReadLine());

        // List to hold all salespersons
        List<Salesperson> salespersonList = new List<Salesperson>();

        // Read details for each salesperson
        for (int i = 0; i < numberOfSalespersons; i++)
        {
            Console.WriteLine($"Information for salesperson {i + 1}:");
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Social Security Number: ");
            string socialSecurityNumber = Console.ReadLine();

            Console.Write("District: ");
            string district = Console.ReadLine();

            Console.Write("Number of sold articles: ");
            int numberSoldArticles = int.Parse(Console.ReadLine());

            // Create a new salesperson and add to the list
            salespersonList.Add(new Salesperson(name, socialSecurityNumber, district, numberSoldArticles));
        }

        // Sort the list using Bubblesort
        BubbleSort(salespersonList);

        // Print out and save the salespersons' information to a file
        using (StreamWriter file = new StreamWriter("salesperson_results.txt"))
        {
            int[] levels = new int[4]; // Number of salespersons at each level: index 0 for level 1, index 1 for level 2, and so on.
            foreach (var salesperson in salespersonList)
            {
                int level = GetSalesLevel(salesperson.NumberSoldArticles) - 1; // -1 to match indexing in levels array.
                levels[level]++;
            }
            
            for (int i = 0; i < salespersonList.Count; i++)
            {
                var salesperson = salespersonList[i];
                int level = GetSalesLevel(salesperson.NumberSoldArticles);
                
                // Print salesperson's information
                string salespersonInfo = $"Name: {salesperson.Name}, SSN: {salesperson.SocialSecurityNumber}, District: {salesperson.District}, Sold: {salesperson.NumberSoldArticles}";
                Console.WriteLine(salespersonInfo);
                file.WriteLine(salespersonInfo);

                // Check if the next salesperson has a different level or if it's the last salesperson, then print level information.
                if (i == salespersonList.Count - 1 || GetSalesLevel(salespersonList[i + 1].NumberSoldArticles) != level)
                {
                    // Use levels array to print out how many salespersons reached the current level.
                    string levelInfo = $"{levels[level - 1]} salespersons have reached level {level}: {GetLevelDescription(level)} articles";
                    Console.WriteLine(levelInfo);
                    file.WriteLine(levelInfo);
                    
                    // Reset the counter for the current level after it has been printed.
                    levels[level - 1] = 0;
                }
            }
        }
    }

    // Bubblesort algorithm to sort the list of salespersons
    static void BubbleSort(List<Salesperson> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            for (int j = 0; j < i; j++)
            {
                if (list[j].NumberSoldArticles < list[j + 1].NumberSoldArticles)
                {
                    // Swap salespersons if they are in the wrong order
                    var temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }
    }

    // Determine the sales level for a salesperson
    static int GetSalesLevel(int numberSoldArticles)
    {
        if (numberSoldArticles >= 200) return 4;
        if (numberSoldArticles >= 100) return 3;
        if (numberSoldArticles >= 50) return 2;
        return 1;
    }

    // Get a description of the sales level
    static string GetLevelDescription(int level)
    {
        switch (level)
        {
            case 1: return "under 50";
            case 2: return "50-99";
            case 3: return "100-199";
            case 4: return "over 199";
            default: return "unknown";
        }
    }
}
