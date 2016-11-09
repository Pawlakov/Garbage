using System;
using System.Collections.Generic;

enum Resource { NONE, EXAMPLE };
enum StatDesire { LESS_THAN, NO_MORE_THAN, EQUAL_AS, MORE_THAN, NO_LESS_THAN };
enum BuildingType { TOWN, CITY, COAST, RESOURCE };
enum BuildingBranch { };
enum BonusCategory { ALL, EXAMPLE0, EXAMPLE1 };

// obiekt przechowuje dane regionu (służy jedynie do prostego ich przekazywania)
class RegionData
{
	public Resource resource;
	public bool coast;
	public bool big;

	public RegionData(string regionDataLine)
	{

	}
};

// obiekt przechowuje dane prowincji (służy jedynie do prostego ich przekazywania)
class ProvinceData
{
	public RegionData[] regionsData;

	public ProvinceData(string provinceDataLine)
	{
		regionsData = new RegionData[3];
		string[] lines = new string[3];

		// Przetwórz line

		for(int i = 0; i < 3; i++)
		{
			regionsData[i] = new RegionData(lines[i]);
		}
	}
};

//obiekt przechowuje kombinacje budynków regionu (DONE)
class RegionCombination
{
	List<Building> buildings;
	Random random;

	public RegionCombination(RegionData data)
	{
		buildings = new List<Building>();
		bool useResource = Convert.ToBoolean(random.Next(1));

		if (data.coast == true)
			buildings.Add(Generator.generateBuilding(BuildingType.COAST));


		if (data.big == true)
		{
			buildings.Add(Generator.generateBuilding(BuildingType.CITY));
			buildings.Add(Generator.generateBuilding(BuildingType.CITY));
			buildings.Add(Generator.generateBuilding(BuildingType.CITY));
			buildings.Add(Generator.generateBuilding(BuildingType.CITY));

			if ((data.resource != Resource.NONE) && (useResource == true))
			{
				buildings.Add(Generator.generateBuilding(BuildingType.RESOURCE, data.resource));
			}
			else
			{
				buildings.Add(Generator.generateBuilding(BuildingType.CITY));
			}
		}
		else
		{
			buildings.Add(Generator.generateBuilding(BuildingType.TOWN));
			buildings.Add(Generator.generateBuilding(BuildingType.TOWN));

			if ((data.resource != Resource.NONE) && (useResource == true))
			{
				buildings.Add(Generator.generateBuilding(BuildingType.RESOURCE, data.resource));
			}
			else
			{
				buildings.Add(Generator.generateBuilding(BuildingType.TOWN));
			}
		}
	}

};

// obiekt przechowujący, obliczający,... dochód regionu
class Wealth
{
	float[] multipliers;
	float[] numbers;
	List<WealthBonus> bonuses;

	public Wealth()
	{
		multipliers = new float[AttilaSimulator.constWealthTypesNumber];
		numbers = new float[AttilaSimulator.constWealthTypesNumber];

		for (int i = 0; i < AttilaSimulator.constWealthTypesNumber; i++)
		{
			multipliers[i] = 0;
			numbers[i] = 0;
		}

		bonuses = new List<WealthBonus>();
	}

	float getWealth()
	{
		float result = 0;

		for (int i = 0; i < AttilaSimulator.constWealthTypesNumber; i++)
		{
			result += (multipliers[i] * numbers[i]);
		}

		return result;
	}

	void addBonus(WealthBonus bonus)
	{
		bonuses.Add(bonus);
	}

	void executeAllBonuses()
	{
		for (int i = 0; i < bonuses.Count; i++)
		{
			executeBonus(i);
		}
	}

	void executeBonus(int i)
	{
		if (bonuses[i].isMultiplier)
		{
			switch (bonuses[1].category)
			{
				case BonusCategory.ALL:
					multipliers[0] += bonuses[i].number;
					multipliers[1] += bonuses[i].number;
					break;
				case BonusCategory.EXAMPLE0:
					multipliers[0] += bonuses[i].number;
					break;
				case BonusCategory.EXAMPLE1:
					multipliers[1] += bonuses[i].number;
					break;
			}
		}
		else
		{
			switch (bonuses[i].category)
			{
				case BonusCategory.EXAMPLE0:
					numbers[0] += bonuses[i].number;
					break;
				case BonusCategory.EXAMPLE1:
					numbers[1] += bonuses[i].number;
					break;

			}
		}
	}
};

// object zawiera jeden konkret bonus danego budynku
class WealthBonus
{
	public BonusCategory category;
	public bool isMultiplier;
	public float number;

	public WealthBonus(string wealthBonusLine)
	{

	}
};

class ProvinceCombination
{
	List<Building>[] buildings;
	Random random;

	int food;
	int order;
	int[] sanitation;
	Wealth[] wealth;

	ProvinceCombination(ProvinceData data)
	{
		buildings = new List<Building>[3];
		sanitation = new int[3];
		wealth = new Wealth[3];

		for(int i = 0; i < 3; i++)
		{
			sanitation[i] = 0;
			wealth[i] = new Wealth();
		}

		//

		for (int i = 0; i < 3; i++)
		{
			bool useResource = Convert.ToBoolean(random.Next(1));

			if (data.regionsData[i].coast == true)
				buildings[i].Add(Generator.generateBuilding(BuildingType.COAST));


			if (data.regionsData[i].big == true)
			{
				buildings[i].Add(Generator.generateBuilding(BuildingType.CITY));
				buildings[i].Add(Generator.generateBuilding(BuildingType.CITY));
				buildings[i].Add(Generator.generateBuilding(BuildingType.CITY));
				buildings[i].Add(Generator.generateBuilding(BuildingType.CITY));

				if ((data.regionsData[i].resource != Resource.NONE) && (useResource == true))
				{
					buildings[i].Add(Generator.generateBuilding(BuildingType.RESOURCE, data.regionsData[i].resource));
				}
				else
				{
					buildings[i].Add(Generator.generateBuilding(BuildingType.CITY));
				}
			}
			else
			{
				buildings[i].Add(Generator.generateBuilding(BuildingType.TOWN));
				buildings[i].Add(Generator.generateBuilding(BuildingType.TOWN));

				if ((data.regionsData[i].resource != Resource.NONE) && (useResource == true))
				{
					buildings[i].Add(Generator.generateBuilding(BuildingType.RESOURCE, data.regionsData[i].resource));
				}
				else
				{
					buildings[i].Add(Generator.generateBuilding(BuildingType.TOWN));
				}
			}
		}
	}

	public void makeCombination()
	{

	}

	void calculate()
	{

	}

	bool checkConditions()
	{

	}
};

// zawiera konkretny poziom budynku wraz z jego bonusami
class Building
{
	public BuildingBranch branch;
	public int regionSanitation;
	public int provinceSanitation;
	public int food;
	public List<WealthBonus> wealthBonuses;

	Building(string[] buildingLines)
	{
		wealthBonuses = new List<WealthBonus>();

		//
	}
};

// zawiera statyczne generatory różnych rzeczy
class Generator
{
	// zwraca gotową kombinację budynków w całej prowincji
	public static ProvinceCombination generateProvince(ProvinceData data)
	{

	}

	// zwraca wylosowany budynek na podstawie przekazanych poleceń
	public static Building generateBuilding(BuildingType type)
	{

	}

	// to w sumie też
	public static Building generateBuilding(BuildingType type, Resource resource)
	{

	}
};

class AttilaSimulator
{
	public const int constWealthTypesNumber = 2;

	int main()
	{

		return 0;
	}
}