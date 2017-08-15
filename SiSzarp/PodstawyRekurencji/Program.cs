using System;

class Program
{
	static byte DigitsCount(ulong number)
	{
		if (number < 10)
			return 1;
		else
			return (byte)(1 + DigitsCount(number / 10));
	}
	static void SpellSingleDigit(byte digit)
	{
		switch(digit)
		{
			case 0:
				Console.Write("zero ");
				break;
			case 1:
				Console.Write("jeden ");
				break;
			case 2:
				Console.Write("dwa ");
				break;
			case 3:
				Console.Write("trzy ");
				break;
			case 4:
				Console.Write("cztery ");
				break;
			case 5:
				Console.Write("pięć ");
				break;
			case 6:
				Console.Write("sześć ");
				break;
			case 7:
				Console.Write("siedem ");
				break;
			case 8:
				Console.Write("osiem ");
				break;
			case 9:
				Console.Write("dziewięć ");
				break;
			default:
				Console.Write("??? ");
				break;
		}
	}
	static void SpellDigits(ulong number)
	{
		if (number < 10)
			SpellSingleDigit((byte)number);
		else
		{
			SpellDigits(number / 10);
			SpellSingleDigit((byte)(number % 10));
		}
	}
	static void Main(string[] args)
	{
		Console.WriteLine("Liczba cyfr liczby 18446744073709551615: {0}", DigitsCount(18446744073709551615));
		Console.Write("Cyfry liczby 18446744073709551615: ");
		SpellDigits(18446744073709551615);
		Console.Write("\n");
		Console.ReadKey();
	}
}

