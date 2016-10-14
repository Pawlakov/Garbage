using System;

class Elevator
{
    private int currentFloor;
    private int requestedFloor;
    private int totalFloorsTraveled;
    private Person passenger;

    public void LoadPassenger()
    {
        passenger = new Person();
    }

    public void InitiateNewFloorRequest()
    {
        requestedFloor = passenger.NewFloorRequest();
        Console.WriteLine("Odjazd z pietra " + currentFloor + " na pietro " + requestedFloor);
        totalFloorsTraveled = totalFloorsTraveled + Math.Abs(currentFloor - requestedFloor);
        currentFloor = requestedFloor;
    }

    public void ReportStatistic()
    {
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
        return randomNumberGenerator.Next(1, 30);
    }
}

class Program
{
    public static Elevator elevatorA;

    public static void Main()
    {
        elevatorA = new Elevator();
        elevatorA.LoadPassenger();
        elevatorA.InitiateNewFloorRequest();
        elevatorA.InitiateNewFloorRequest();
        elevatorA.InitiateNewFloorRequest();
        elevatorA.InitiateNewFloorRequest();
        elevatorA.InitiateNewFloorRequest();
        elevatorA.ReportStatistic();
    }
}
