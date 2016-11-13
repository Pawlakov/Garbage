using System;
using System.Collections.Generic;

enum Resource { NONE, MARBLE, OLIVE, GOLD, IRON, WINE, WOOD, GEMSTONE, DYE, SALT, SILK, FUR, LEAD, SPICE };
enum BuildingType { TOWN, CENTER_TOWN, CITY, CENTER_CITY, COAST, RESOURCE };
enum BonusCategory { AGRICULTURE, CULTURE, INDUSTRY, LIVESTOCK, LOCAL_COMMERCE, MARITIME_COMMERCE, SUBSISTENCE };

class AttilaSimulator
{
	public const int constWealthTypesNumber = 7;
	public const int constBuildingTypesNumber = 6;
	public const int constResourceTypesNumber = 14;

	public static void Main()
	{
		//ProvinceData test = new ProvinceData("Tarraconensis.Tarraco,true,NONE,.Caesaraugusta,false,NONE,.Pompaelo,false,MARBLE,.");
		//WealthBonus test = new WealthBonus("EXAMPLE0.true.false.0,25.");

		//ProvinceWealth test = new ProvinceWealth();
		//test.AddBonus(new WealthBonus("EXAMPLE0.false.false.2."), 0);
		//test.AddBonus(new WealthBonus("EXAMPLE1.false.false.2."), 0);
		//test.AddBonus(new WealthBonus("EXAMPLE1.true.true.0,25."), 1);
		//decimal a0 = test.GetWealth(0);
		//decimal a1 = test.GetWealth(1);
		//decimal a2 = test.GetWealth(2);

		//string[] testString = new string[9];
		//testString[0] = "City_Major.CENTER_CITY";
		//testString[1] = "(-1.0.-30.2.";
		//testString[2] = "SUBSISTENCE.false.false.300.";
		//testString[3] = "(-2.0.-60.3.";
		//testString[4] = "SUBSISTENCE.false.false.400.";
		//testString[5] = "(-3.0.-100.4.";
		//testString[6] = "SUBSISTENCE.false.false.500.";
		//testString[7] = "(-4.0.-140.5.";
		//testString[8] = "SUBSISTENCE.false.false.600.";
		//Building test = new Building(testString);

		//ProvinceCombination test = Generator.GenerateProvinceCombination(new ProvinceData("Tarraconensis.Tarraco,true,NONE,.Caesaraugusta,false,NONE,.Pompaelo,false,MARBLE,."));
		//test.Calculate();
		//Console.WriteLine(Convert.ToString(test.GetWeath(0)));
		//Console.WriteLine(Convert.ToString(test.GetWeath(1)));
		//Console.WriteLine(Convert.ToString(test.GetWeath(2)));

		Console.ReadKey();
	}
}