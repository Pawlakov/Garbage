using System;
using System.Collections.Generic;

enum Resource { NONE, MARBLE, OLIVE };
enum StatDesire { LESS_THAN, NO_MORE_THAN, EQUAL_AS, MORE_THAN, NO_LESS_THAN };
enum BuildingType { TOWN, CITY, COAST, RESOURCE };
enum BonusCategory { EXAMPLE0, EXAMPLE1 };

class AttilaSimulator
{
	public const int constWealthTypesNumber = 2;

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


		Console.ReadKey();
	}
}