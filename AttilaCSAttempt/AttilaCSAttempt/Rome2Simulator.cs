// Po usunięciu bezużytecznych budynków knoci się przydzielanie budynku zasobowego. NAPRAW.
using System;

enum Resource { AMBER, PURPLE_DYE, GLASS, GOLD, GRAIN, HORSE, IRON, LEAD, LEATHER, LUMBER, MARBLE, OLIVE, SILK, WINE, NONE };
enum BuildingType { TOWN, CENTER_TOWN, CITY, CENTER_CITY, COAST, RESOURCE };
enum BonusCategory { ALL, AGRICULTURE, CULTURE, INDUSTRY, LOCAL_COMMERCE, MARITIME_COMMERCE, SUBSISTENCE };
enum DifficultyLevel { EASY = 1, NORMAL, HARD, VERY_HARD, LEGENDARY };

class Rome2Simulator
{
	public const int constResourceTypesNumber = 15;
	public const int constBuildingTypesNumber = 6;
	public const int constBonusCategoriesNumber = 7;
	public static void Main()
	{
		Map map;
		ProvinceData province;
		FactionsList factions;
		Faction faction;
		BuildingSlot[][] slots;
		short minimalOrder;
		Console.WriteLine("Let's begin.");
		//
		Console.WriteLine("Loading map.");
		map = new Map("map.xml");
		Console.WriteLine("Map loadaded. List of provinces:");
		map.ShowList();
		Console.WriteLine("Pick province.");
		province = map[Convert.ToByte(Console.ReadLine())];
		Console.WriteLine("You picked: {0}", province.Name);
		//
		Console.WriteLine("List of difficulty levels:");
		Difficulty.ShowLevels();
		Console.WriteLine("Pick difficulty.");
		Difficulty.Level = (DifficultyLevel)Convert.ToInt32(Console.ReadLine());
		Console.WriteLine("You picked difficulty level {0}", Difficulty.Level);
		//
		Console.WriteLine("Loading factions.");
		factions = new FactionsList("factions.xml");
		Console.WriteLine("Factions loadaded. List of factions:");
		factions.ShowList();
		Console.WriteLine("Pick faction.");
		faction = factions[Convert.ToByte(Console.ReadLine())];
		Console.WriteLine("You picked: {0}", faction.Name);
		//
		slots = Generator.GenerateSlotsArray(province);
		Console.WriteLine("Generated template of slots.");
		ForceBuildings(slots, province, faction);
		Console.WriteLine("Constraints set.");
		//
		Console.WriteLine("Choose minimal public order.");
		minimalOrder = Convert.ToInt16(Console.ReadLine());
		//
		Console.WriteLine("Which parameter do you want to maximize?");
		Console.WriteLine("0. Wealth");
		Console.WriteLine("1. Food");
		if (Convert.ToByte(Console.ReadLine()) == 0)
		{
			Generator.GenerateCombinationInTermsOfWealth(slots, province, faction, minimalOrder);
		}
		else
		{
			Generator.GenerateCombinationInTermsOfFood(slots, province, faction, minimalOrder);
		}
	}
	public static void ShowSlots(BuildingSlot[][] slots, ProvinceData province)
	{
		Console.WriteLine("Province: {0}", province.Name);
		for (byte whichRegion = 0; whichRegion < slots.Length; whichRegion++)
		{
			Console.WriteLine("{0}. Region: {1}", whichRegion, province[whichRegion].Name);
			for (byte whichSlot = 0; whichSlot < slots[whichRegion].Length; whichSlot++)
			{
				Console.Write("{0}. ", whichSlot);
				slots[whichRegion][whichSlot].ShowContent();
			}
		}
	}
	public static void ForceBuildings(BuildingSlot[][] slots, ProvinceData province, Faction faction)
	{
		byte choice;
		byte whichRegion;
		byte whichSlot;
		//byte whichType;
		Console.WriteLine("Now you can place some building constraints by yourself.");
		while (true)
		{
			ShowSlots(slots, province);
			Console.WriteLine("What would you like to do now?");
			Console.WriteLine("0. Finish placing constraints.");
			Console.WriteLine("1. Create new constraint.");
			choice = Convert.ToByte(Console.ReadLine());
			if (choice == 0)
				break;
			else
			{
				Console.WriteLine("Which region?");
				whichRegion = Convert.ToByte(Console.ReadLine());
				Console.WriteLine("Which slot?");
				whichSlot = Convert.ToByte(Console.ReadLine());
				Console.WriteLine("which coinstraint type?");
				Console.WriteLine("0. Level.");
				Console.WriteLine("1. Building.");
				choice = Convert.ToByte(Console.ReadLine());
				if (choice == 0)
				{
					Console.WriteLine("Which level?");
					choice = Convert.ToByte(Console.ReadLine());
					slots[whichRegion][whichSlot].Level = choice;
				}
				else
				{
					faction.Buildings.ShowListOneType(slots[whichRegion][whichSlot].Type);
					Console.WriteLine("Which building?");
					choice = Convert.ToByte(Console.ReadLine());
					slots[whichRegion][whichSlot].BuildingBranch = faction.Buildings[slots[whichRegion][whichSlot].Type, choice];
				}
			}
		}
		//Console.WriteLine("Now you can remove some building from being randomly placed.");
		//while (true)
		//{
		//	Console.Clear();
		//	for (whichType = 0; whichType < constBuildingTypesNumber; whichType++)
		//		faction.Buildings.ShowListOneType((BuildingType)whichType);
		//	Console.WriteLine("What would you like to do now?");
		//	Console.WriteLine("0. Finish removing.");
		//	Console.WriteLine("1. Remove building.");
		//	choice = Convert.ToByte(Console.ReadLine());
		//	if (choice == 0)
		//		break;
		//	else
		//	{
		//		Console.WriteLine("Which building type?");
		//		whichType = Convert.ToByte(Console.ReadLine());
		//		Console.WriteLine("Which building?");
		//		choice = Convert.ToByte(Console.ReadLine());
		//		faction.Buildings[(BuildingType)whichType].RemoveAt(choice);
		//	}
		//}
	}
}