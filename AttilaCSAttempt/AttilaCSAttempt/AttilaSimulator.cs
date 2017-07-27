using System;

enum Resource { NONE, MARBLE, OLIVE, GOLD, IRON, WINE, WOOD, GEMSTONE, DYE, SALT, SILK, FUR, LEAD, SPICE };
enum BuildingType { TOWN, CENTER_TOWN, CITY, CENTER_CITY, COAST, RESOURCE };
enum BonusCategory { AGRICULTURE, CULTURE, INDUSTRY, LIVESTOCK, LOCAL_COMMERCE, MARITIME_COMMERCE, SUBSISTENCE, MAINTENANCE };
enum Faction { ALAMANS, ALANS, ANTEANS, AKSUM, BURGUNDIANS, DANES, EASTERN_ROMAN_EMPIRE, EBDANIANS, FRANKS, GARAMANTIANS, GEATS, HIMYAR, HUNS, JUTES, LAKHMIDS, LANGOBARDS, OSTROGOTHS, PICTS, SASSANID_EMPIRE, SAXONS, SCLAVENIANS, SUEBIANS, TANUKHIDS, VANDALS, VENEDIANS, VISIGOTHS, CALEDONIANS, WESTERN_ROMAN_EMPIRE, WHITE_HUNS};

class AttilaSimulator
{
	public const int constWealthTypesNumber = 8;
	public const int constBuildingTypesNumber = 6;
	public const int constResourceTypesNumber = 14;
	public const int consFactionsNumber = 29;

	public static void Main()
	{
		int whichProvince = Convert.ToInt32(Console.ReadLine());

		SimData data = new SimData();
		Generator.GenerateProvinceCombination(data, whichProvince);

		Console.ReadKey();
	}
}