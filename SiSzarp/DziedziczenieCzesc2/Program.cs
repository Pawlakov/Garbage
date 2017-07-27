abstract class Car
{
	private uint odometer;
	protected uint Odometer
	{
		set
		{
			odometer = value;
		}
		get
		{
			return odometer;
		}
	}
	public abstract void MoveForward();
}
class RacingCar : Car
{
	public override void MoveForward()
	{
		System.Console.WriteLine("Bardzo szybka jazda...");
		Odometer += 30;
		System.Console.WriteLine("Licznik przejechanych kilometrów w samochodzie wyścigowym: {0}", Odometer);
	}
}
class FamilyCar : Car
{
	public override void MoveForward()
	{
		System.Console.WriteLine("Jazda powolna, ale bezpieczna...");
		Odometer += 5;
		System.Console.WriteLine("Licznik przejechanych kilometrów w samochodzie rodzinnym {0}", Odometer);
	}
}
//
interface IComparable
{
	int CompareTo(IComparable comp);
}
class TimeSpan : IComparable
{
	private uint totalSeconds;
	public TimeSpan() : this(0)
	{
	}
	public TimeSpan(uint initialSeconds)
	{
		totalSeconds = initialSeconds;
	}
	public uint Seconds
	{
		get
		{
			return totalSeconds;
		}
		set
		{
			totalSeconds = value;
		}
	}
	public virtual int CompareTo(IComparable comp)
	{
		TimeSpan compareTime = (TimeSpan)comp;
		if (totalSeconds > compareTime.Seconds)
			return 1;
		else if (compareTime.Seconds == totalSeconds)
			return 0;
		else
			return -1;
	}
}
class Sorter
{
	public static void BubbleSortAscending(IComparable[] bubbles)
	{
		bool swapped = true;
		for(int i =0; swapped; i++)
		{
			swapped = false;
			for(int j = 0; j < (bubbles.Length - (i+1)); j++)
			{
				if(bubbles[j].CompareTo(bubbles[j+1]) > 0)
				{
					Swap(j, j + 1, bubbles);
					swapped = true;
				}
			}
		}
	}
	public static void Swap(int first, int second, IComparable[] arr)
	{
		IComparable temp;
		temp = arr[first];
		arr[first] = arr[second];
		arr[second] = temp;
	}
}
//
//interface IComparable
//class Sorter
class Account : IComparable
{
	private uint balance;
	public uint Balance
	{
		get
		{
			return balance;
		}
		set
		{
			balance = value;
		}
	}
	public int CompareTo(IComparable comp)
	{
		Account compareAccount = (Account)comp;
		if (balance > compareAccount.Balance)
			return 1;
		else if (compareAccount.Balance == balance)
			return 0;
		else
			return -1;
	}
	public Account(uint newBalance)
	{
		balance = newBalance;
	}
}
//
interface IFileable
{
	void Read();
	void Write();
}
abstract class Employee
{
	public abstract void CalculateSalary();
}
class Secretary : Employee, IFileable
{
	public override void CalculateSalary()
	{
		System.Console.WriteLine("Teraz wyliczam pobory dla Sekretarza.");
	}
	public void Write()
	{
		System.Console.WriteLine("Teraz piszę Sekretarza");
	}
	public void Read()
	{
		System.Console.WriteLine("Teraz czytam Sekretarza");
	}
}
class Director : Employee
{
	public override void CalculateSalary()
	{
		System.Console.WriteLine("Teraz wyliczam pobory dla Dyrektora.");
	}
}
class Programmer : Employee
{
	public override void CalculateSalary()
	{
		System.Console.WriteLine("Teraz wyliczam pobory dla Programisty.");
	}
}
class Building
{
	private int age;
	private decimal currentValue;
}
class House : Building, IFileable
{
	private ushort numberOfBedrooms;
	public void Write()
	{
		System.Console.WriteLine("Teraz piszę Dom");
	}
	public void Read()
	{
		System.Console.WriteLine("Teraz czytam Dom");
	}
}
class OfficeBuilding : Building
{
	private uint floorSpace;
}
//
class Program
{
	public static void Main(string[] args)
	{
		//CarTest();
		//GenericBubbleSort();
		//AccountsTest();
		EmployeesTest();
		System.Console.ReadKey();
	}
	public static void CarTest()
	{
		RacingCar myRacingCar = new RacingCar();
		FamilyCar myFamilyCar = new FamilyCar();
		myRacingCar.MoveForward();
		myFamilyCar.MoveForward();
	}
	public static void GenericBubbleSort()
	{
		TimeSpan[] raceTimes = new TimeSpan[4];
		raceTimes[0] = new TimeSpan(153);
		raceTimes[1] = new TimeSpan(165);
		raceTimes[2] = new TimeSpan(108);
		raceTimes[3] = new TimeSpan(142);
		Sorter.BubbleSortAscending(raceTimes);
		System.Console.WriteLine("Lista posortowanych czasów: ");
		foreach(TimeSpan time in raceTimes)
		{
			System.Console.WriteLine(time.Seconds);
		}
	}
	public static void AccountsTest()
	{
		Account[] accounts = new Account[4];
		accounts[0] = new Account(500);
		accounts[1] = new Account(20999);
		accounts[2] = new Account(17);
		accounts[3] = new Account(3001);
		Sorter.BubbleSortAscending(accounts);
		System.Console.WriteLine("Lista posortowanych kont: ");
		foreach (Account balance in accounts)
		{
			System.Console.WriteLine(balance.Balance);
		}
	}
	public static void EmployeesTest()
	{
		int randomChoice;
		Employee[] employees = new Employee[100];
		System.Random rng = new System.Random();
		for (int i = 0; i < 100; i++)
		{
			randomChoice = rng.Next(0, 2);
			switch(randomChoice)
			{
				case 0:
					employees[i] = new Secretary();
					break;
				case 1:
					employees[i] = new Director();
					break;
				case 2:
					employees[i] = new Programmer();
					break;
			}
		}
		for (int i = 0; i < 100; i++)
		{
			employees[i].CalculateSalary();
		}
	}
}