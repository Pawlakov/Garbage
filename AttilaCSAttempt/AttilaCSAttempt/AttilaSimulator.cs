using System;
using System.Collections.Generic;

enum Resource { NONE, MARBLE, OLIVE };
enum StatDesire { LESS_THAN, NO_MORE_THAN, EQUAL_AS, MORE_THAN, NO_LESS_THAN };
enum BuildingType { TOWN, CITY, COAST, RESOURCE };
enum BonusCategory { ALL, EXAMPLE0, EXAMPLE1 };

class AttilaSimulator
{
	//public const int constWealthTypesNumber = 2;

	public static void Main()
	{
		ProvinceData test = new ProvinceData("Tarraconensis.Tarraco,true,NONE,.Caesaraugusta,false,NONE,.Pompaelo,false,MARBLE,.");

		Console.ReadKey();
	}
}