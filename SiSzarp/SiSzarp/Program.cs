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
    }

    public void ReportStatistic()
    {
        Console.WriteLine("Ilosc zrobionych kursow: " + totalTripsTraveled);
        Console.WriteLine("Ilosc przebytych do pieter: " + totalFloorsTraveled);
        Console.ReadKey();
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

class Program
{
    public static Elevator elevatorA;

    public static void Main()
    {
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
}
