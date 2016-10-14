using System;

class Program
{
    public static void Main()
    {
        int x;
        int y;

        Console.Write("Podaj x: ");
        x = Convert.ToInt32(Console.ReadLine());
        Console.Write("Podaj y: ");
        y = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Suma: " + (x + y));
        Console.WriteLine("Roznica: " + (x - y));
        Console.WriteLine("Iloczyn: " + (x * y));
        Console.WriteLine("Iloraz: " + (x / y));
        Console.WriteLine("Minimum: " + Math.Min(x, y));
        Console.WriteLine("Maksimum: " + Math.Max(x, y));
        Console.Read();
    }
}
