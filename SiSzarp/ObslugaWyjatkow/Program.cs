using System;

class ExampleClass
{
	public void ExampleMethod1()
	{
		ExampleMethod2();
	}

	public void ExampleMethod2()
	{
		int exampleInt1 = 0;
		int exampleInt2;
		try
		{
			exampleInt2 = 10 / exampleInt1;
		}
		catch
		{
			Console.Write("Nastąpił wyjątek. Podaj liczbę: ");
			exampleInt2 = Convert.ToInt32(Console.ReadLine());
		}
	}
}
class Meteorologist
{
	int[] rainFall = new int[12];
	int[] pollution = new int[12];
	public Meteorologist()
	{
		for(int i = 0; i < 12; i++)
		{
			rainFall[i] = 2 * i;
			pollution[i] = 12 - (2 * i);
		}
	}
	public int GetRainFall(int index)
	{
		try
		{
			return rainFall[index];
		}
		catch (IndexOutOfRangeException exceptionObject)
		{
			throw exceptionObject;
		}
	}
	public int GetAveragePollution(int index)
	{
		try
		{
			Console.WriteLine("Otwieram plik.");
			return pollution[index]/rainFall[index];
		}
		catch (IndexOutOfRangeException exceptionObject)
		{
			throw exceptionObject;
		}
		catch (DivideByZeroException exceptionObject)
		{
			throw exceptionObject;
		}
		finally
		{
			Console.WriteLine("Zamykam plik");
		}
	}
}
class Program
{
	static void Main(string[] args)
	{
		Meteorologist jaroslawKret = new Meteorologist();
		try
		{
			Console.WriteLine("Średnie zanieczyszczenie w styczniu: {0}", jaroslawKret.GetAveragePollution(0));
		}
		catch
		{
			Console.WriteLine("Coś się zepsuło.");
		}
		Console.ReadKey();
	}

}

