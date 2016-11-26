using System;
using System.Linq;

//wyrazenia lambda
class Lam
{
	Predicate<int> p1 = value => value > 0;
	Predicate<int> p2 = (value) => value > 0;
	Predicate<int> p3 = (int value) => value > 0;
	Predicate<int> p4 = value => { return value > 0; };
	Predicate<int> p5 = value => value > 0;
	Predicate<int> p6 = value => value > 0;
}

class Lambda
{
	public event Action<string> Announcement;
	Lambda()
	{
		Announcement += Shout;
		Announcement -= Shout;
		//usunie sie

		Announcement += (x) => { Console.WriteLine($"Hellooo: {x}!"); };
		Announcement -= (x) => { Console.WriteLine($"Hellooo: {x}!"); };
		//!!!!!!!! Nie usunie sie bo jest anonimowa
		Announcement -= (x) => { Console.WriteLine($"See ya: {x}!"); };


	}
	
	public void Write(string name)
	{
		Announcement.Invoke(name);
	}

	public void Shout(string s)
	{
		Console.WriteLine(s + "!!!");
	}
}

namespace Net.Wyklad.Examples
{
	class Arrays
	{
		Arrays()
		{
			Func<int, bool> findFirst = IsLessThan10;
			Predicate<int> findFirst2 = delegate (int x) { return x > 10; };

			
		}
		public bool IsLessThan10(int x)
		{
			return x < 10;
		}
	}
}

class Program
{
	//public static void Main()
	//{

	//	Console.ReadKey();
	//}
}