using System;

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

        seconds -=50;
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

class Program
{
    public static void Main()
    {
        //ElevatorsSimulator();
        //BliposClock();
        //MetadataAccesor();
        //bliposTax();
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
        typeContainer =  casualNothing.GetType();
        Console.WriteLine(typeContainer.FullName);
    }

    static void bliposTax()
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
}
