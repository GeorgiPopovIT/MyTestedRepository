using System;
using System.Linq;
using System.Collections.Generic;
namespace MyTestedRepo
{
    class Program
    {
        class Cars
        {
            public Cars(int mile, int fuel)
            {
                this.Mileage = mile;
                this.Fuel = fuel;
            }
            public int Mileage { get; set; }
            public int Fuel { get; set; }
        }
        static void Main(string[] args)
        {
            var dict = new Dictionary<string, Cars>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] cars = Console.ReadLine().Split("|");
                string name = cars[0];
                int mile = int.Parse(cars[1]);
                int fuel = int.Parse(cars[2]);
                Cars car = new Cars(mile, fuel);
                if (!dict.ContainsKey(name) && mile >= 0 && fuel >= 0)
                {
                    dict.Add(name, car);
                }
            }
            string[] command = Console.ReadLine().Split(" : ");
            while (command[0] != "Stop")
            {
                if (command.Contains("Drive"))
                {
                    string name = command[1];
                    int mile = int.Parse(command[2]);
                    int fuel = int.Parse(command[3]);
                    if (fuel >= dict[name].Fuel)
                    {
                        Console.WriteLine("Not enough fuel to make that ride");
                    }
                    else
                    {
                        dict[name].Mileage += mile;
                        dict[name].Fuel -= fuel;
                        Console.WriteLine($"{name} driven for {mile} kilometers. {fuel} liters of fuel consumed.");
                    }
                    if (dict[name].Mileage >= 100000)
                    {
                        dict.Remove(name);
                        Console.WriteLine($"Time to sell the {name}!");
                    }
                }
                if (command.Contains("Refuel"))
                {
                    string name = command[1];
                    int fuel = int.Parse(command[2]);
                    if (dict[name].Fuel + fuel >= 75)
                    {
                        fuel = 75 - dict[name].Fuel;
                    }
                    dict[name].Fuel += fuel;
                    Console.WriteLine($"{name} refueled with {fuel} liters");
                }
                if (command.Contains("Revert"))
                {
                    string name = command[1];
                    int mile = int.Parse(command[2]);
                    if (dict[name].Mileage - mile < 10000)
                    {
                        dict[name].Mileage = 10000;
                    }
                    else
                    {
                        dict[name].Mileage -= mile;
                        Console.WriteLine($"{name} mileage decreased by {mile} kilometers");
                    }
                }
                command = Console.ReadLine().Split(" : ");
            }
            var sortDict = dict.OrderByDescending(x => x.Value.Mileage)
                .ThenBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            foreach (var kvp in sortDict)
            {
                Console.WriteLine($"{kvp.Key} -> Mileage: {kvp.Value.Mileage} kms, Fuel in the tank: {kvp.Value.Fuel} lt.");
            }
        }
    }
}
