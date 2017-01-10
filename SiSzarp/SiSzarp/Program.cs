using System;
using System.Timers;

class Elevator
{
	private int currentFloor = 0;
	private int requestedFloor = 0;
	private int totalFloorsTraveled = 0;
	private int totalTripsTraveled = 0;
	private string name;
	private Person passenger;

	public Elevator(string givenName)
	{
		name = givenName;
	}

	public void LoadPassenger()
	{
		passenger = new Person();
	}

	public void InitiateNewFloorRequest()
	{
		requestedFloor = passenger.NewFloorRequest();
		Console.WriteLine(name + ": Odjazd z pietra " + currentFloor + " na pietro " + requestedFloor);
		totalFloorsTraveled = totalFloorsTraveled + Math.Abs(currentFloor - requestedFloor);
		totalTripsTraveled = totalTripsTraveled + 1;
		currentFloor = requestedFloor;
		Console.Beep();
	}

	public void ReportStatistic()
	{
		Console.WriteLine("Ilosc zrobionych kursow: " + totalTripsTraveled);
		Console.WriteLine("Ilosc przebytych do pieter: " + totalFloorsTraveled);
	}
}

class Person
{
	private Random randomNumberGenerator;

	public Person()
	{
		randomNumberGenerator = new Random();
	}

	public int NewFloorRequest()
	{
		return randomNumberGenerator.Next(0, 50);
	}
}

class BliposClock
{
	private byte seconds;
	private short minutes;

	public BliposClock()
	{
		seconds = 0;
		minutes = 0;
	}

	public void OneForward()
	{
		byte originalSeconds = seconds;

		seconds++;
		if (originalSeconds > seconds)
			minutes++;
	}

	public void OneBackward()
	{
		byte originalSeconds = seconds;

		seconds--;
		if (originalSeconds < seconds)
			minutes--;
	}

	public void FastForward()
	{
		byte originalSeconds = seconds;

		seconds += 50;
		if (originalSeconds > seconds)
			minutes++;
	}

	public void FastBackward()
	{
		byte originalSeconds = seconds;

		seconds -= 50;
		if (originalSeconds < seconds)
			minutes--;
	}

	public void FasterForward()
	{
		byte originalSeconds = seconds;

		seconds += 100;
		if (originalSeconds > seconds)
			minutes++;
	}

	public void FasterBackward()
	{
		byte originalSeconds = seconds;

		seconds -= 100;
		if (originalSeconds < seconds)
			minutes--;
	}

	public void SetSeconds(byte sec)
	{
		seconds = sec;
	}

	public void SetMinutes(short min)
	{
		minutes = min;
	}

	public void ShowTime()
	{
		Console.WriteLine(minutes + ":" + seconds);
	}
}

class Account
{
	private decimal balance;
	private decimal currentInterestRate;
	private decimal totalInterestPaid;

	public Account()
	{
		balance = 0;
		currentInterestRate = 0;
		totalInterestPaid = 0;
	}

	public void SetInterestRate(decimal newInterestRate)
	{
		currentInterestRate = newInterestRate;
	}

	public decimal GetInterestRate()
	{
		return currentInterestRate;
	}

	public void updateInterest()
	{
		totalInterestPaid += balance * currentInterestRate;
		balance += balance * currentInterestRate;
	}

	public void Withdraw(decimal amount)
	{
		balance -= amount;
	}

	public void Deposit(decimal amount)
	{
		balance += amount;
	}

	public decimal GetBallance()
	{
		return balance;
	}

	public decimal GetTotalInterestPaid()
	{
		return totalInterestPaid;
	}
}

class Bank
{
	private Account[] accounts;

	public Bank()
	{
		Console.WriteLine("Gratulacje! Otworzyłeś nowy bank!");
		Console.Write("Podaj liczbę kont w banku: ");
		accounts = new Account[Convert.ToInt32(Console.ReadLine())];
		for (int i = 0; i < accounts.Length; i++)
		{
			accounts[i] = new Account();
		}
	}

	public void Deposit()
	{
		int accountNumber;
		decimal amount;
		Console.Write("Depozyt. Proszę podać numer konta: ");
		accountNumber = Convert.ToInt32(Console.ReadLine());
		Console.Write("Podaj kwotę do wpłaty: ");
		amount = Convert.ToDecimal(Console.ReadLine());
		accounts[accountNumber - 1].Deposit(amount);
		Console.WriteLine("Nowy stan konta {0}: {1:C}", accountNumber, accounts[accountNumber - 1].GetBallance());
	}

	public void Withdraw()
	{
		int accountNumber;
		decimal amount;
		Console.Write("Wypłata. Proszę podać numer konta: ");
		accountNumber = Convert.ToInt32(Console.ReadLine());
		Console.Write("Podaj kwotę do wypłaty: ");
		amount = Convert.ToInt32(Console.ReadLine());
		accounts[accountNumber - 1].Withdraw(amount);
		Console.WriteLine("Nowy stan konta {0}: {1:C}", accountNumber, accounts[accountNumber - 1].GetBallance());
	}

	public void SetInterestRate()
	{
		int accountNumber;
		decimal newInterestRate;
		Console.Write("Ustalania stopy procentowej. Proszę podać numer konta: ");
		accountNumber = Convert.ToInt32(Console.ReadLine());
		Console.Write("Podaj stopę procentową: ");
		newInterestRate = Convert.ToInt32(Console.ReadLine());
		accounts[accountNumber - 1].SetInterestRate(newInterestRate);
	}

	public void PrintAllInterestRates()
	{
		Console.WriteLine("Stopy procentowe wszystkich kont:");
		for (int i = 0; i < accounts.Length; i++)
		{
			Console.WriteLine("Konto {0,-3}: {1,-10}", (i + 1), accounts[i].GetInterestRate());
		}
	}

	public void PrintAllBallances()
	{
		Console.WriteLine("Stany wszystkich kont:");
		for (int i = 0; i < accounts.Length; i++)
		{
			Console.WriteLine("Konto {0,-3}: {1,12:C}", (i + 1), accounts[i].GetBallance());
		}
	}

	public void PrintTotalInterestPaidAllAccount()
	{
		Console.WriteLine("Całkowity przyrost z oprocentowania poszczególnych kont:");
		for (int i = 0; i < accounts.Length; i++)
		{
			Console.WriteLine("Konto {0,-3}: {1,12:C}", (i + 1), accounts[i].GetTotalInterestPaid());
		}
	}

	public void UpdateInterestAllAccounts()
	{
		for (int i = 0; i < accounts.Length; i++)
		{
			Console.WriteLine("Odsetki dodane do konta numer {0,-3}: {1,12:C}", (i + 1), accounts[i].GetBallance() * accounts[i].GetInterestRate());
			accounts[i].updateInterest();
		}
	}
}

class ArrayMath
{
	public static double ArrayAverage(double[] array)
	{
		double result = 0.0d;
		for (int i = 0; i < array.Length; i++)
		{
			result += array[i];
		}
		return result /= array.Length;
	}

	public static int[] ArraySum(int[] array1, int[] array2)
	{
		int[] array;

		if (array1.Length == array2.Length)
		{
			array = new int[array2.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array1[i] + array2[i];
			}
		}

		return array = null;
	}

	public static int ArrayMax(int[] array)
	{
		int max = array[0];

		for (int i = 1; i < array.Length; i++)
		{
			if (array[i] > max)
				max = array[i];
		}

		return max;
	}
}

class Car
{
	private int position;

	public Car(int newPosition)
	{
		position = newPosition;
	}

	public void MoveForward(int distance)
	{
		position += distance;
	}

	public void Reverse(int distance)
	{
		position -= distance;
	}

	public void SetPosition(int newPosition)
	{
		position = newPosition;
	}

	public int GetPosition()
	{
		return position;
	}
}

class CarGame
{
	private Car[] cars;

	public CarGame()
	{
		cars = new Car[5];
		for (int i = 0; i < 5; i++)
		{
			cars[i] = new Car(0);
		}
	}

	public void MoveCar()
	{
		int i;
		int n;

		Console.Write("Króry samochód?: ");
		i = Convert.ToInt32(Console.ReadLine());
		Console.Write("O ile chcesz poruszyć (ujemnie/dodatnie)?: ");
		n = Convert.ToInt32(Console.ReadLine());

		if (n > 0)
		{
			cars[i].MoveForward(n);
		}
		else
		{
			cars[i].Reverse(-n);
		}
	}

	public void SetCar()
	{
		int i;
		int n;

		Console.Write("Króry samochód?: ");
		i = Convert.ToInt32(Console.ReadLine());
		Console.Write("Na jaką pozycję przemieścić?: ");
		n = Convert.ToInt32(Console.ReadLine());

		cars[i].SetPosition(n);
	}

	public void PrintCars()
	{
		for (int i = 0; i < 5; i++)
		{
			Console.WriteLine("Samochód nr " + i + ": " + cars[i].GetPosition());
		}
	}
}

class WeatherForecaster
{
	public static double CalculateRainFallDay10RegionA()
	{
		//Wymaga 5000ms
		System.Threading.Thread.Sleep(5000);
		Random randomizer = new Random();
		return (double)randomizer.Next(40, 100);
	}
}

class CropForecast
{
	private double rainFallDay10RegionA;
	private bool rainFallDay10RegionANeedUpdate = true;

	public CropForecast()
	{
		Timer aTimer = new Timer();
		aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
		aTimer.Interval = 20000;
		aTimer.Enabled = true;
	}

	public double RainFallDay10RegionA
	{
		get
		{
			if (rainFallDay10RegionANeedUpdate)
			{
				rainFallDay10RegionA = WeatherForecaster.CalculateRainFallDay10RegionA();
				rainFallDay10RegionANeedUpdate = false;
				return rainFallDay10RegionA;
			}
			else
			{
				return rainFallDay10RegionA;
			}
		}
	}

	private double ComplexResultA()
	{
		// Tu jakieś super obliczenia
		return ((RainFallDay10RegionA / 2) + (rainFallDay10RegionA / 3) + (rainFallDay10RegionA / 4));
	}

	private double ComplexResultB()
	{
		// Tu jakieś super obliczenia
		return (RainFallDay10RegionA / 10 - 100) + (rainFallDay10RegionA / 100);
	}

	public double WheatCropSizeInTonsInRegionA()
	{
		// Hue hue i nic więcej
		Console.WriteLine("Czekajże bo liczę, no...");
		return (ComplexResultA() / 2 + ComplexResultB() / 4 + ComplexResultA()) * 100000;
	}

	public void OnTimedEvent(object source, ElapsedEventArgs e)
	{
		//Co 20 sekund (chyba)
		Console.WriteLine("\n\nPotrzebna nowa aktualizaja\nWyznaczyć inną prognozę?");
		rainFallDay10RegionANeedUpdate = true;
	}
}

class BirthList
{
	private uint[] births = new uint[4];

	public uint this[uint index]
	{
		get
		{
			return births[index];
		}
		set
		{
			births[index] = value;
		}
	}
}

class Program
{
	public static void Main()
	{
		//ElevatorsSimulator();
		//BliposClock();
		//MetadataAccesor();
		//BliposTax();
		//BankSimulation();
		//ForecastTester();
		BirthListTester();
		Console.ReadKey();
	}

	static void ElevatorsSimulator()
	{
		Elevator elevatorA;

		Console.WriteLine("Symulacja rozpoczeta.");
		elevatorA = new Elevator("Winda_A");
		elevatorA.LoadPassenger();
		elevatorA.InitiateNewFloorRequest();
		elevatorA.InitiateNewFloorRequest();
		elevatorA.InitiateNewFloorRequest();
		elevatorA.InitiateNewFloorRequest();
		elevatorA.InitiateNewFloorRequest();
		elevatorA.InitiateNewFloorRequest();
		elevatorA.InitiateNewFloorRequest();
		elevatorA.InitiateNewFloorRequest();
		elevatorA.InitiateNewFloorRequest();
		elevatorA.InitiateNewFloorRequest();
		elevatorA.ReportStatistic();
		Console.WriteLine("Symulacja zakonczona.");
	}

	static void BliposClock()
	{
		string command;

		Console.WriteLine("Witamy w zegarze Blipos. 256s/min 65536min/d");
		BliposClock myClock = new BliposClock();
		Console.WriteLine("Ustaw zegar.");
		Console.Write("Sekundy: ");
		myClock.SetSeconds(Convert.ToByte(Console.ReadLine()));
		Console.Write("Minuty: ");
		myClock.SetMinutes(Convert.ToInt16(Console.ReadLine()));
		Console.WriteLine("F = +1 :: B = -1 :: A = +50 :: D = -50 :: T = koniec");

		do
		{
			command = Console.ReadLine().ToUpper();
			if (command == "F")
				myClock.OneForward();
			if (command == "B")
				myClock.OneBackward();
			if (command == "A")
				myClock.FastForward();
			if (command == "D")
				myClock.FastBackward();
			if (command == "H")
				myClock.FasterForward();
			if (command == "M")
				myClock.FasterBackward();
			myClock.ShowTime();
		} while (command != "T");
	}

	static void MetadataAccesor()
	{
		Type typeContainer;

		double casualNothing = 22.0;
		typeContainer = casualNothing.GetType();
		Console.WriteLine(typeContainer.FullName);
	}

	static void BliposTax()
	{
		int amount;
		float tax;

		Console.Write("Podaj kwotę: ");
		amount = Convert.ToInt32(Console.ReadLine());

		tax = BliposTaxCalc(amount);

		Console.WriteLine("Podatek = " + tax);
	}

	static float BliposTaxCalc(int amount)
	{
		if (amount <= 10000)
		{
			return 0;
		}
		else if (amount <= 25000)
		{
			return 0.05f * (amount - 10000) + BliposTaxCalc(10000);
		}
		else if (amount <= 50000)
		{
			return 0.1f * (amount - 25000) + BliposTaxCalc(25000);
		}
		else if (amount <= 100000)
		{
			return 0.15f * (amount - 50000) + BliposTaxCalc(50000);
		}
		else
		{
			return 0.20f * (amount - 100000) + BliposTaxCalc(100000);
		}
	}

	static void GuessTwoLetters()
	{
		string letterCode;
		string letterGuess;
		string letters = "ABCD";
		int ccCounter;
		int cwCounter;
		int guessCounter = 0;
		string anotherGame;
		Random Randomizer = new Random();
		do
		{
			guessCounter = 0;
			letterCode = letters[Randomizer.Next(0, 4)].ToString() + letters[Randomizer.Next(0, 4)].ToString();
			Console.WriteLine("Wybierz 2 z 4 liter: A, B, C, D");

			do
			{
				ccCounter = 0;
				cwCounter = 0;
				letterGuess = Console.ReadLine().ToUpper();
				if (letterGuess[0] == letterCode[0])
					ccCounter++;
				if (letterGuess[1] == letterCode[1])
					ccCounter++;

				if (ccCounter == 0)
				{
					if (letterCode[0] == letterCode[1])
						cwCounter++;
					if (letterCode[1] == letterCode[0])
						cwCounter++;
				}
				Console.WriteLine("Dobre miejsca: {0} Dobre litery (złe miejsca): {1}", ccCounter, cwCounter);
				guessCounter++;
			} while (letterCode == letterGuess);

			Console.WriteLine((guessCounter < 8) ? "Dobra robota!" : "Nareszcie!");
			Console.WriteLine("Podałeś rozwiązanie po {0} próbach", guessCounter);
			Console.WriteLine("Czy jeszcze raz [T/N]?");
			anotherGame = Console.ReadLine().ToUpper();
		} while (anotherGame == "T");
		Console.WriteLine("Miło się grało, do zobaczenia!");
	}

	static void BankSimulation()
	{
		Bank bigBucksBank = new Bank();
		string command;

		do
		{
			PrintMenu();
			command = Console.ReadLine().ToUpper();
			switch (command)
			{
				case "D":
					bigBucksBank.Deposit();
					break;
				case "W":
					bigBucksBank.Withdraw();
					break;
				case "S":
					bigBucksBank.SetInterestRate();
					break;
				case "U":
					bigBucksBank.UpdateInterestAllAccounts();
					break;
				case "R":
					bigBucksBank.PrintAllBallances();
					break;
				case "T":
					bigBucksBank.PrintTotalInterestPaidAllAccount();
					break;
				case "O":
					bigBucksBank.PrintAllInterestRates();
					break;
				case "K":
					Console.WriteLine("Do widzenia!");
					break;
				default:
					Console.WriteLine("Niewłaściwy wybór");
					break;
			}
		} while (command != "K");
	}

	static void PrintMenu()
	{
		Console.WriteLine("Co chcesz uczynić?");
		Console.WriteLine("D)epozyt");
		Console.WriteLine("W)ypłata");
		Console.WriteLine("Ustalenie S)topy procentowej");
		Console.WriteLine("U)aktualnienie wszystkich kont o odsetki");
		Console.WriteLine("WydR)uk wszystkich stanów kont");
		Console.WriteLine("CałkowiT)e kwoty przyrostu z oprocentowania drukowane dla wszystkich kont");
		Console.WriteLine("Stopy O)procentowania dla wszystkich kont");
		Console.WriteLine("K)oniec sesji");
		Console.WriteLine("Uwaga: Pierwsze konto ma numer jeden");
	}

	static void ForecastTester()
	{
		string answer;
		CropForecast MyForecaster = new CropForecast();

		Console.Write("Czy chcesz wyznaczyć prognozę zbiorów? T/N");
		answer = Console.ReadLine().ToUpper();
		while (!(answer == "N"))
		{
			Console.WriteLine("Wielkość zbiorów w tonach: {0:N2}", MyForecaster.WheatCropSizeInTonsInRegionA());
			Console.Write("Czy jeszcze raz? T/N");
			answer = Console.ReadLine().ToUpper();
		}
	}

	static void BirthListTester()
	{
		BirthList blDenmark = new BirthList();
		uint sum;

		blDenmark[0] = 10200;
		blDenmark[1] = 20398;
		blDenmark[2] = 40938;
		blDenmark[3] = 6894;

		sum = blDenmark[0] + blDenmark[1] + blDenmark[2] + blDenmark[3];

		Console.WriteLine("Suma z 4 regionów: {0}", sum);
	}
}
