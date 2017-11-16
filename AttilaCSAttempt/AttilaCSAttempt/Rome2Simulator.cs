// Zepchnij czynności na jak najniższy poziom.
// Budynki surowcowe mają być traktowane specjalnie i wymuszane w szablonie od początku.
using System;

enum Resource { AMBER, PURPLE_DYE, GLASS, GOLD, GRAIN, HORSE, IRON, LEAD, LEATHER, LUMBER, MARBLE, OLIVE, SILK, WINE, NONE };
enum BuildingType { TOWN, CENTER_TOWN, CITY, CENTER_CITY, COAST, RESOURCE };
enum BonusCategory { ALL, AGRICULTURE, CULTURE, INDUSTRY, LOCAL_COMMERCE, MARITIME_COMMERCE, SUBSISTENCE };
enum DifficultyLevel { EASY, NORMAL, HARD, VERY_HARD, LEGENDARY };

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
		ProvinceCombination template;
		sbyte minimalOrder;
		sbyte powerMax;
		sbyte powerMin;
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
		template = new ProvinceCombination(province, faction);
		Console.WriteLine("Generated template of slots.");
		ForceBuildings(template);
		Console.WriteLine("Constraints set.");
		//
		Console.WriteLine("Choose minimal public order.");
		minimalOrder = Convert.ToSByte(Console.ReadLine());
		//
		Console.WriteLine("Choose x in size of a round, which is 2^x.");
		powerMax = Convert.ToSByte(Console.ReadLine());
		//
		Console.WriteLine("Choose x in size of a smallest list, which is 2^x.");
		powerMin = Convert.ToSByte(Console.ReadLine());
		//
		Console.WriteLine("Which parameter do you want to maximize?");
		Console.WriteLine("0. Wealth");
		Console.WriteLine("1. Food");
		if (Convert.ToByte(Console.ReadLine()) == 0)
		{
			Generator.Generate(template, minimalOrder, BetterInWealth, powerMax, powerMin);
		}
		else
		{
			Generator.Generate(template, minimalOrder, BetterInFood, powerMax, powerMin);
		}
	}
	public static int BetterInWealth(ProvinceCombination left, ProvinceCombination right)
	{
		return (int)(left.Wealth - right.Wealth);
	}
	public static int BetterInFood(ProvinceCombination left, ProvinceCombination right)
	{
		if (left.Food == right.Food)
		{
			return (int)(left.Wealth - right.Wealth);
		}
		else
			return (int)(left.Food - right.Food);
	}
	public static void ForceBuildings(ProvinceCombination template)
	{
		Console.WriteLine("Now you can place some building constraints by yourself.");
		while (true)
		{
			template.ShowContent();
			Console.WriteLine("What would you like to do now?");
			Console.WriteLine("0. Finish placing constraints.");
			Console.WriteLine("1. Create new constraint.");
			if (Convert.ToByte(Console.ReadLine()) == 0)
				break;
			else
			{
				template.ForceConstraint();
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