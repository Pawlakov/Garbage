using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace omg
{
	abstract class Ssak : ISsak
	{
		public int age;

		public abstract string LastNam { get; set; }

		protected string FirstName;
		private string LastName;
		public abstract void Name();

		public Ssak(string FirstName)
		{
			this.FirstName = FirstName;
		}

		public Ssak(string FirstName, string LastName)
		{
			this.FirstName = FirstName;
			this.LastName = LastName;
		}

		public override string ToString()
		{
			return this.FirstName + " " + this.LastName;
		}
	}

	class Kot : Ssak
	{
		public Kot(string FirstName, string LastName) : base(FirstName, LastName)
		{

		}

		public override string LastNam
		{
			get
			{
				return FirstName;
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public void WypiszImie()
		{
			Console.WriteLine(this.FirstName);
		}
		//public this test()
		//{
		//	return this;
		//}
		public override void Name()
		{
			Console.WriteLine(this.FirstName);
		}
		
	}
	public static class Extension
	{
		public static void MyExtenstion(this string word, int number)
		{
			Console.WriteLine(word + " " + number);
		}
	}
	class Dog : Ssak
	{
		public Dog(string one, string two) : base(one, two)
		{

		}

		public override string LastNam
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}
		public override void Name()
		{
			Console.WriteLine("I'm tha Doge" + FirstName);
		}
	}
	//OVER 9000 MISTAKES MADE DYRING WRITING THIS CODE
	interface ISsak
	{
		string LastNam { get; set; }

		void Name();
	}

	class Program
	{
		public static void Main()
		{
			//Ssak[] ssaki = new Ssak[10];
			//nie mozna tworzyc pojedynczych obiektow typu interfejsu/klasy abstrakcyjnej
			ISsak[] SsakArray = new Ssak[2];

			Kot kotek = new Kot("Andrzej", "Trump");
			kotek.WypiszImie();

			Ssak ssak = new Kot("Andrzej", "Lepper");

			Dog dog = new Dog("Pies Maciej", "Piotrek Pietrek");

			ssak.Name();
			dog.Name();
			Console.WriteLine(ssak.ToString());

			Console.WriteLine("Ghandi NOOOOO");

			string first = "it's";
			string second = "";

			first.MyExtenstion(11);
			second.MyExtenstion(15);

			Console.ReadKey();
		}
	}
}
