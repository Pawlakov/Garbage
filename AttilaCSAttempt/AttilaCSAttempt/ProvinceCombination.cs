using System;
using System.Collections.Generic;

// Tworzy i przechowuje utworzoną kombinację budynków.
class ProvinceCombination
{
	List<string> listing;

	int food;
	int order;
	int[] sanitation;
	ProvinceWealth wealth;

	Random random;

	public decimal totalWealth;

	public ProvinceCombination(SimData data, int whichProvince)
	{
		listing = new List<string>();

		random = new Random();

		food = 0;
		order = 0;
		sanitation = new int[3];
		wealth = new ProvinceWealth();

		for (int i = 0; i < 3; i++)
		{
			sanitation[i] = 0;
		}

		listing.Add("Province " + data.map[whichProvince].provinceName + ": ");

		for (int i = 0; i < 3; i++)
		{
			listing.Add("Region " + data.map[whichProvince].regionNames[i] + ": ");

			if (data.map[whichProvince].isBig[i] == true)
			{
				ApplyBuilding(Generator.GenerateBuilding(BuildingType.CENTER_CITY, Resource.NONE, data), i);
				ApplyBuilding(Generator.GenerateBuilding(BuildingType.CITY, Resource.NONE, data), i);
				ApplyBuilding(Generator.GenerateBuilding(BuildingType.CITY, Resource.NONE, data), i);
				ApplyBuilding(Generator.GenerateBuilding(BuildingType.CITY, Resource.NONE, data), i);

				//if (data.map[whichProvince].resources[i] != Resource.NONE)
				//{
				//	ApplyBuilding(Generator.GenerateBuilding(BuildingType.RESOURCE, data.map[whichProvince].resources[i], data), i);
				//}
				//else
				//{
					ApplyBuilding(Generator.GenerateBuilding(BuildingType.CITY, Resource.NONE, data), i);
				//}

				if (data.map[whichProvince].isCoastal[i] == true)
					ApplyBuilding(Generator.GenerateBuilding(BuildingType.COAST, Resource.NONE, data), i);
			}
			else
			{
				ApplyBuilding(Generator.GenerateBuilding(BuildingType.CENTER_TOWN, Resource.NONE, data), i);
				ApplyBuilding(Generator.GenerateBuilding(BuildingType.TOWN, Resource.NONE, data), i);

				//if (data.map[whichProvince].resources[i] != Resource.NONE)
				//{
				//	ApplyBuilding(Generator.GenerateBuilding(BuildingType.RESOURCE, data.map[whichProvince].resources[i], data), i);
				//}
				//else
				//{
					ApplyBuilding(Generator.GenerateBuilding(BuildingType.TOWN, Resource.NONE, data), i);
				//}

				if (data.map[whichProvince].isCoastal[i] == true)
					ApplyBuilding(Generator.GenerateBuilding(BuildingType.COAST, Resource.NONE, data), i);
			}

			data.RefreshBuildings();
		}

		totalWealth = GetWeath(0) + GetWeath(1) + GetWeath(2);
		listing.Add("Wealth: " + totalWealth + ", Public order: " + order + ", Food: " + food + ", Regional sanitations: " + sanitation[0] + " " + sanitation[1] + " " + sanitation[2]);
	}

	public void ApplyBuilding(Building building, int whichRegion)
	{
		int level;
		if ((building.typeTag == BuildingType.CENTER_TOWN)||(building.typeTag == BuildingType.CENTER_CITY))
			level = 3;
		else
			level = random.Next(0, 4);

		// Wciskanie bonusów z budynków do majatku prowincji
		for (int whichBonus = 0; whichBonus < building.wealthBonuses[level].Count; whichBonus++)
		{
			wealth.AddBonus(building.wealthBonuses[level][whichBonus], whichRegion);
		}
		//

		food += building.food[level];
		order += building.order[level];

		// Sanitacje
		sanitation[0] += building.provinceSanitation[level];
		sanitation[1] += building.provinceSanitation[level];
		sanitation[2] += building.provinceSanitation[level];
		sanitation[whichRegion] += building.regionSanitation[level];
		//

		listing.Add("Building " + building.name + " " + (level + 1));

		wealth.ExecuteAllBonuses();
		wealth.ClearBonuses();
	}

	public decimal GetWeath(int whichRegion)
	{
		return wealth.GetWealth(whichRegion);
	}

	public bool FitsConditions()
	{
		if (food < 0)
			return false;
		else if (order < 3)
			return false;
		else if (sanitation[0] < 0)
			return false;
		else if (sanitation[1] < 0)
			return false;
		else if (sanitation[2] < 0)
			return false;
		else
			return true;
	}

	public void PrintListing()
	{
		for(int whichLine = 0; whichLine < listing.Count; whichLine++)
		{
			Console.WriteLine(listing[whichLine]);
		}
	}
}