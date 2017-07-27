class ElectronicDevice
{
	private string brandName;
	private bool isOn;
	public string BrandName
	{
		get
		{
			return brandName;
		}
		set
		{
			brandName = value;
		}
	}
	public ElectronicDevice() : this("Nieznany")
	{
	}
	public ElectronicDevice(string newBrandName)
	{
		brandName = newBrandName;
		isOn = false;
	}
	public virtual void SwitchOn()
	{
		isOn = true;
		System.Console.WriteLine("Włączony");
	}
	public virtual void SwitchOff()
	{
		isOn = false;
		System.Console.WriteLine("Wyłączony");
	}
}

class Radio : ElectronicDevice
{
	private double currentFrequency;
	public Radio() : base()
	{
		currentFrequency = 0;
	}
	public Radio(string newBrandName, double newFrequency) : base(newBrandName)
	{
		currentFrequency = newFrequency;
	}
	public override void SwitchOn()
	{
		System.Console.Write("Radio ");
		base.SwitchOn();
	}
	public override void SwitchOff()
	{
		System.Console.Write("Radio ");
		base.SwitchOff();
	}
}

class Computer : ElectronicDevice
{
	private int internalMemory;
	public Computer() : base()
	{
		internalMemory = 0;
	}
	public Computer(string newBrandName, int newInternalMemory) : base(newBrandName)
	{
		internalMemory = newInternalMemory;
	}
	public override void SwitchOn()
	{
		System.Console.Write("Komputer ");
		base.SwitchOn();
	}
	public override void SwitchOff()
	{
		System.Console.Write("Komputer ");
		base.SwitchOff();
	}
}

class MobilePhone : ElectronicDevice
{
	private uint lastNumberDialed;
	public MobilePhone() : base()
	{
		lastNumberDialed = 0;
	}
	public MobilePhone(string newBrandName, uint newLastNumberDialed) : base(newBrandName)
	{
		lastNumberDialed = newLastNumberDialed;
	}
	public override void SwitchOn()
	{
		System.Console.Write("Telefon Komórkowy ");
		base.SwitchOn();
	}
	public override void SwitchOff()
	{
		System.Console.Write("Telefon Komórkowy ");
		base.SwitchOff();
	}
}

class LaptopComputer : Computer
{
	uint maxBatteryLife;
	public LaptopComputer() : base()
	{
		maxBatteryLife = 0;
	}
	public LaptopComputer(string newBrandName, int newInternalMemory, uint newMaxBatteryLife) : base(newBrandName, newInternalMemory)
	{
		maxBatteryLife = newMaxBatteryLife;
	}
	public override void SwitchOn()
	{
		System.Console.Write("Laptop ");
		base.SwitchOn();
	}
	public override void SwitchOff()
	{
		System.Console.Write("Lapotop ");
		base.SwitchOff();
	}
}

class Program
{
	static void Main(string[] args)
	{
		LaptopComputer myLaptop = new LaptopComputer("Szajsung", 1024, 2);
		myLaptop.SwitchOn();
		System.Console.WriteLine(myLaptop.BrandName);
		myLaptop.SwitchOff();
		System.Console.ReadKey();
	}
}
