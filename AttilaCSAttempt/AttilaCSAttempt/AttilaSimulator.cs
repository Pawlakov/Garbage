using System;

enum Resource { NONE, AMBER, PURPLE_DYE, GLASS, GOLD, GRAIN, HORSE, IRON, LEAD, LEATHER, LUMBER, MARBLE, OLIVE, SILK, WINE };
enum BuildingType { TOWN, CENTER_TOWN, CITY, CENTER_CITY, COAST, RESOURCE };
enum BonusCategory { AGRICULTURE, CULTURE, INDUSTRY, LOCAL_COMMERCE, MARITIME_COMMERCE, SUBSISTENCE };
enum DifficultyLevel { EASY = 1, NORMAL, HARD, VERY_HARD, LEGENDARY};

class AttilaSimulator
{
	public const int constResourceTypesNumber = 15;
	public const int constBuildingTypesNumber = 6;
	public const int constBonusCategoriesNumber = 6;
	public static void Main()
	{
		Map map;
		ProvinceData provinceData;
		Console.WriteLine("Let's begin.");
		Console.WriteLine("Loading map.");
		map = new Map("map.txt");
		Console.WriteLine("Map loadaded. List of provinces");
		map.ShowList();
		Console.WriteLine("Pick a province.");
		provinceData = map[Convert.ToByte(Console.ReadLine())];
		//
		//
		//
		int whichProvince = Convert.ToInt32(Console.ReadLine());
		SimData data = new SimData();
		Generator.GenerateProvinceCombination(data, whichProvince);
		Console.ReadKey();
	}
}