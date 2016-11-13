using System;

// Tworzy i przechowuje utworzoną kombinację budynków.
class ProvinceCombination
{
	Building[][] buildings;
	int[][] levels;
	
	int food;
	int order;
	int[] sanitation;
	ProvinceWealth wealth;

	Random random;

	// Póki co działa OK
	public ProvinceCombination(ProvinceData data)
	{
		buildings = new Building[3][];
		levels = new int[3][];

		food = 0;
		order = 0;
		sanitation = new int[3];
		wealth = new ProvinceWealth();

		random = new Random();

		for (int i = 0; i < 3; i++)
		{
			sanitation[i] = 0;

			levels[i] = new int[6];
			for(int j = 0; j < 6; j++)
			{
				levels[i][j] = random.Next(0, 4);
			}
		}

		// Tu był znacznik że tu coś ma się coś nowego zjawić ale nie mam pojęcia co.

		for (int i = 0; i < 3; i++)
		{
			bool useResource = Convert.ToBoolean(random.Next(1));

			if (data.isBig[i] == true)
			{
				if (data.isCoastal[i] == true)
					buildings[i] = new Building[6];
				else
					buildings[i] = new Building[5];

				buildings[i][0] = Generator.GenerateBuilding(BuildingType.CENTER_CITY, Resource.NONE);
				buildings[i][1] = Generator.GenerateBuilding(BuildingType.CITY, Resource.NONE);
				buildings[i][2] = Generator.GenerateBuilding(BuildingType.CITY, Resource.NONE);
				buildings[i][3] = Generator.GenerateBuilding(BuildingType.CITY, Resource.NONE);

				if ((data.resources[i] != Resource.NONE) && (useResource == true))
				{
					buildings[i][4] = Generator.GenerateBuilding(BuildingType.RESOURCE, data.resources[i]);
				}
				else
				{
					buildings[i][4] = Generator.GenerateBuilding(BuildingType.CITY, Resource.NONE);
				}

				if (data.isCoastal[i] == true)
					buildings[i][5] = Generator.GenerateBuilding(BuildingType.COAST, Resource.NONE);
			}
			else
			{
				if (data.isCoastal[i] == true)
					buildings[i] = new Building[4];
				else
					buildings[i] = new Building[3];

				buildings[i][0] = Generator.GenerateBuilding(BuildingType.CENTER_TOWN, Resource.NONE);
				buildings[i][1] = Generator.GenerateBuilding(BuildingType.TOWN, Resource.NONE);

				if ((data.resources[i] != Resource.NONE) && (useResource == true))
				{
					buildings[i][2] = Generator.GenerateBuilding(BuildingType.RESOURCE, data.resources[i]);
				}
				else
				{
					buildings[i][2] = Generator.GenerateBuilding(BuildingType.TOWN, Resource.NONE);
				}

				if (data.isCoastal[i] == true)
					buildings[i][3] = Generator.GenerateBuilding(BuildingType.COAST, Resource.NONE);
			}
		}
	}

	// Na podstawie przydzielonych już budynków wyznacza parametry prowincji. DZIAŁA ALLELUJA
	public void Calculate()
	{
		// Pętla dla każdego regionu.
		for (int whichRegion = 0; whichRegion < 3; whichRegion++)
		{
			// Pętla dla każdego budynku.
			for(int whichBuilding = 0; whichBuilding < buildings[whichRegion].Length; whichBuilding++)
			{
				// Wciskanie bonusów z budynków do majatku prowincji
				for(int whichBonus = 0; whichBonus < buildings[whichRegion][whichBuilding].wealthBonuses[levels[whichRegion][whichBuilding]].Count; whichBonus++)
				{
					wealth.AddBonus(buildings[whichRegion][whichBuilding].wealthBonuses[levels[whichRegion][whichBuilding]][whichBonus], whichRegion);
				}
				//

				food += buildings[whichRegion][whichBuilding].food[levels[whichRegion][whichBuilding]];
				order += buildings[whichRegion][whichBuilding].order[levels[whichRegion][whichBuilding]];

				// Sanitacje
				sanitation[0] += buildings[whichRegion][whichBuilding].provinceSanitation[levels[whichRegion][whichBuilding]];
				sanitation[1] += buildings[whichRegion][whichBuilding].provinceSanitation[levels[whichRegion][whichBuilding]];
				sanitation[2] += buildings[whichRegion][whichBuilding].provinceSanitation[levels[whichRegion][whichBuilding]];
				sanitation[whichRegion] += buildings[whichRegion][whichBuilding].regionSanitation[levels[whichRegion][whichBuilding]];
				//
			}
			//
		}
		//

		wealth.ExecuteAllBonuses();
	}

	public decimal GetWeath(int whichRegion)
	{
		return wealth.GetWealth(whichRegion);
	}

	public decimal GetTotalWealth()
	{
		decimal result = 0;
		result += GetWeath(0);
		result += GetWeath(1);
		result += GetWeath(2);
		return result;
	}

	public bool FitsConditions()
	{
		if (food < 0)
			return false;
		else if (order < 0)
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
}