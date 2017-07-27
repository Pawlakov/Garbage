using System;

struct Fraction
{
	private int numerator;
	private int denominator;
	public Fraction(int initialNumerator, int initialDenominator)
	{
		numerator = initialNumerator;
		denominator = initialDenominator;
	}
	public int Numerator
	{
		get
		{
			return numerator;
		}
		set
		{
			numerator = value;
		}
	}
	public int Denominator
	{
		get
		{
			return denominator;
		}
		set
		{
			denominator = value;
		}
	}
	public float Value
	{
		get
		{
			return (float)numerator / (float)denominator;
		}
	}
	public override string ToString()
	{
		return "Wartość ułamka: " + Value;
	}
}

class Program
{
	static void Main(string[] args)
	{
		Fraction fraction = new Fraction(2, 3);
		Console.WriteLine(fraction);
		Console.ReadKey();
	}
}
