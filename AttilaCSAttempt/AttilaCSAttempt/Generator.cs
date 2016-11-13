using System;
using System.Collections.Generic;

// zawiera generatory różnych rzeczy
class Generator
{
	private static List<ProvinceData> map;
	private static List<Building>[][] buildings; // I po co ja tworzę takie molochy? No po co?

	// ZROBIONE!
	public Generator()
	{
		map = new List<ProvinceData>();
		// Czy ty to widzisz?
		buildings = new List<Building>[AttilaSimulator.constBuildingTypesNumber][];
		for (int whichBuildingType = 0; whichBuildingType < AttilaSimulator.constBuildingTypesNumber; whichBuildingType++)
		{
			buildings[whichBuildingType] = new List<Building>[AttilaSimulator.constResourceTypesNumber];
			for (int whichResourceType = 0; whichResourceType < AttilaSimulator.constResourceTypesNumber; whichResourceType++)
			{
				buildings[whichBuildingType][whichResourceType] = new List<Building>();
			}
		}
		// Koszmarne, czyż nie?

		LoadMap();
		LoadBuildings();
	}

	private void LoadMap() // To jest niegotowe. Nic nowego.
	{
		List<string> lines = new List<string>();

		//
		// To będzie wymagało znajomości obsługi plików.
		//

		for(int whichLine = 0; whichLine < lines.Count; whichLine++)
		{
			map.Add(new ProvinceData(lines[whichLine]));
		}
	}

	private void LoadBuildings() // DO SPRAWDZENIA NA DZIAŁANIE (Oczywiśie kiedy będzie gotowo no bo TERAZ JESZCZE NIE JEST).
	{
		List<string> lines = new List<string>();

		//
		// To będzie ładowało linijki jak tylko nauczę się obsługiwać pliki, a to niedługo będzie.
		//

		int beginLine = 0;
		int endLine = 0;
		for (int whichLine = 0; whichLine < lines.Count; whichLine++)
		{
			if (lines[whichLine] == "X")
			{
				endLine = whichLine - 1;

				string[] tempLines;
				tempLines = new string[endLine - beginLine + 1];
				for (int whichTempLine = 0; whichTempLine > tempLines.Length; whichTempLine++)
				{
					tempLines[whichTempLine] = lines[beginLine + whichTempLine];
				}
				Building tempBuilding = new Building(tempLines);
				buildings[(int)tempBuilding.typeTag][(int)tempBuilding.resourceTag].Add(tempBuilding);

				beginLine = whichLine + 1;
				// Jak to będzie działać to ja jestem miszcz.
			}
		}
	}

	// Zwraca najlepszy budynek. ZROBIONE!
	public static ProvinceCombination GenerateProvinceCombination(ProvinceData data)
	{
		// Tylko kombinacje spełniające podstawowe warunki i nadające się do porównania.
		ProvinceCombination[] combinations = new ProvinceCombination[1024];
		//

		// Zapełnianie tablicy kandydatów.
		int whichCombination = 0;
		while(combinations[1023] == null)
		{
			ProvinceCombination subject = new ProvinceCombination(data);
			if(subject.FitsConditions())
			{
				combinations[whichCombination] = subject;
				whichCombination++;
			}
		}
		//

		// Zwróć najlepszego.
		ProvinceCombination bestOne = combinations[0];
		for(whichCombination = 1; whichCombination < combinations.Length; whichCombination++)
		{
			if (combinations[whichCombination].GetTotalWealth() > bestOne.GetTotalWealth())
				bestOne = combinations[whichCombination];
		}
		return bestOne;
		//
	}

	// Zwraca wylosowany budynek na podstawie przekazanych poleceń. ZROBIONE!
	public static Building GenerateBuilding(BuildingType type, Resource resource)
	{
		Random random = new Random();
		List<Building> poll = buildings[(int)type][(int)resource];
		if (poll.Count >= 1)
			return poll[random.Next(0, poll.Count)];
		else
			return null;
	}
}