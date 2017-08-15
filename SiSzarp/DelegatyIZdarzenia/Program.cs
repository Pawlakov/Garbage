using System;
enum MoveRequestType { FastForward, SlowForward, Reverse };
class MoveRequestEventArgs : EventArgs
{
	private MoveRequestType request;
	public MoveRequestEventArgs(MoveRequestType iniRequest) : base()
	{
		request = iniRequest;
	}
	public MoveRequestType Request
	{
		get
		{
			return request;
		}
	}
}
class Car
{
	private int distance;
	private int speedParam;
	private string name;
	public Car(int initSpeedParam, string initName)
	{
		speedParam = initSpeedParam;
		distance = 0;
		name = initName;
	}
	public void MoveRequestHandler(object sender, MoveRequestEventArgs e)
	{
		switch (e.Request)
		{
			case MoveRequestType.SlowForward:
				distance += speedParam;
				Console.WriteLine("Nazwa samochodu: " + name + ". Wolna jazda. Odległość: " + distance);
				break;
			case MoveRequestType.FastForward:
				distance += speedParam * 2;
				Console.WriteLine("Nazwa samochodu: " + name + ". Szybka jazda. Odległość: " + distance);
				break;
			case MoveRequestType.Reverse:
				distance -= 5;
				Console.WriteLine("Nazwa samochodu: " + name + ". Jazda wstecz. Odległość: " + distance);
				break;
		}
	}
	public void Subscribe(GameController controller)
	{
		controller.OnMoveRequest += new GameController.MoveRequest(MoveRequestHandler);
	}
	public void Unsubscribe(GameController controller)
	{
		controller.OnMoveRequest -= new GameController.MoveRequest(MoveRequestHandler);
	}
	public override string ToString()
	{
		return name;
	}
}
class Motorbike
{
	private int distance;
	private string name;
	public Motorbike(string initialName)
	{
		distance = 0;
		name = initialName;
	}
	public void MoveRequestHandler(object sender, MoveRequestEventArgs e)
	{
		switch (e.Request)
		{
			case MoveRequestType.SlowForward:
				distance += 30;
				Console.WriteLine("Nazwa motoru: " + name + ". Wolna jazda. Odległość: " + distance);
				break;
			case MoveRequestType.FastForward:
				distance += 30;
				Console.WriteLine("Nazwa motoru: " + name + ". Szybka jazda. Odległość: " + distance);
				break;
			case MoveRequestType.Reverse:
				distance -= 3;
				Console.WriteLine("Nazwa motoru: " + name + ". Jazda wstecz. Odległość: " + distance);
				break;
		}
	}
	public void Subscribe(GameController controller)
	{
		controller.OnMoveRequest += new GameController.MoveRequest(MoveRequestHandler);
	}
	public void Unsubscribe(GameController controller)
	{
		controller.OnMoveRequest -= new GameController.MoveRequest(MoveRequestHandler);
	}
	public override string ToString()
	{
		return name;
	}
}
class GameController
{
	public delegate void MoveRequest(object sender, MoveRequestEventArgs e);
	public event MoveRequest OnMoveRequest;
	Car[] gameCars = new Car[10];
	string carName;
	int speedParam = 0;
	int carCounter = 0;
	int carNumber = 0;
	public void Run()
	{
		string answer;
		Console.WriteLine("Dokonaj wyboru z podanego menu: ");
		Console.WriteLine("A) Dodaj nowy samochód");
		Console.WriteLine("C) Zapisz zdarzenie");
		Console.WriteLine("U) Wypisz się z listy zdarzeń");
		Console.WriteLine("L) Wypisz samochody z danej gry");
		Console.WriteLine("F) Szybko do przodu");
		Console.WriteLine("S) Powoli do przodu");
		Console.WriteLine("R) Wstecz");
		Console.WriteLine("T) Zakończ");
		do
		{
			Console.WriteLine("Wybierz nową opcję:");
			answer = Console.ReadLine().ToUpper();
			switch (answer)
			{
				case "A":
					Console.Write("Podaj nazwę nowego samochodu: ");
					carName = Console.ReadLine();
					Console.Write("Podaj parametr prędkości nowego samochodu: ");
					speedParam = Convert.ToInt32(Console.ReadLine());
					gameCars[carCounter] = new Car(speedParam, carName);
					carCounter++;
					break;
				case "C":
					Console.Write("Podaj indeks w tablicy dotyczącej samochodu, którego zdarzenie wpisujemy: ");
					carNumber = Convert.ToInt32(Console.ReadLine());
					gameCars[carNumber].Subscribe(this);
					break;
				case "U":
					Console.Write("Podaj indeks w tablicy dotyczący samochodu, którego chcemy wypisać: ");
					carNumber = Convert.ToInt32(Console.ReadLine());
					gameCars[carNumber].Unsubscribe(this);
					break;
				case "L":
					for (int i = 0; i < carCounter; i++)
					{
						Console.WriteLine(gameCars[i]);
					}
					break;
				case "F":
					if (OnMoveRequest != null)
						OnMoveRequest(this, new MoveRequestEventArgs(MoveRequestType.FastForward));
					break;
				case "S":
					if (OnMoveRequest != null)
						OnMoveRequest(this, new MoveRequestEventArgs(MoveRequestType.SlowForward));
					break;
				case "R":
					if (OnMoveRequest != null)
						OnMoveRequest(this, new MoveRequestEventArgs(MoveRequestType.Reverse));
					break;
				case "T":
					break;
				default:
					Console.WriteLine("Niepoprawny wybór. Spróbuj ponownie.");
					break;
			}
		} while (answer != "T");
	}
}
//
class MyMath
{
	public double Average(int number1, int number2)
	{
		return (number1 + number2) / 2;
	}
	public double Product(int number1, int number2)
	{
		return number1 * number2;
	}
}
//
class Program
{
	public static void Main()
	{
		//CarGameEvents();
		Calculator();
		Console.ReadKey();
	}
	//
	public static void CarGameEvents()
	{
		GameController controller = new GameController();
		controller.Run();
	}
	//
	public delegate double Calculation(int x, int y);
	public static double Sum(int num1, int num2)
	{
		return num1 + num2;
	}
	public static void Calculator()
	{
		double result;
		MyMath myMath = new MyMath();
		Calculation myCalculation = new Calculation(myMath.Average);
		result = myCalculation(10, 20);
		Console.WriteLine("Wynik przekazania 10, 20 do myCalculation: {0}", result);
		myCalculation = new Calculation(Sum);
		result = myCalculation(10, 20);
		Console.WriteLine("Wynik przekazania 10, 20 do myCalculation: {0}", result);
		myCalculation = new Calculation(myMath.Product);
		result = myCalculation(10, 20);
		Console.WriteLine("Wynik przekazania 10, 20 do myCalculation: {0}", result);
	}
}