using System;
using System.Collections.Generic;

// zawiera statyczne generatory różnych rzeczy
class Generator
{
	List<ProvinceData> map;
	List<Building>[][] buildings; // I po co ja tworzę takie molochy? No po co?

	public Generator()
	{
		map = new List<ProvinceData>();
		// Czy ty to widzisz?
		buildings = new List<Building>[AttilaSimulator.constBuildingTypesNumber][];
		for(int whichBuildingType = 0; whichBuildingType < AttilaSimulator.constBuildingTypesNumber; whichBuildingType++)
		{
			buildings[whichBuildingType] = new List<Building>[AttilaSimulator.constResourceTypesNumber];
			for(int whichResourceType = 0; whichResourceType < AttilaSimulator.constResourceTypesNumber; whichResourceType++)
			{
				buildings[whichBuildingType][whichResourceType] = new List<Building>();
			}
		}
		// Koszmarne, czyż nie?

		LoadMap();
		LoadBuildings();
	}

	private void LoadMap()
	{
		// To będzie wymagało znajomości obsługi plików.
	}

	private void LoadBuildings()
	{
		// To będzie ładowało budynki jak tylko nauczę się obsługiwać pliki.
	}

	//public ProvinceCombination GenerateProvinceCombination(// Co tu miałoby się znaleźć?)
	//{
		
	//}
	
	// zwraca wylosowany budynek na podstawie przekazanych poleceń
	public Building GenerateBuilding(BuildingType type, Resource resource)
	{
		Random random = new Random();
		List<Building> poll = buildings[(int)type][(int)resource];
		if (poll.Count >= 1)
			return poll[random.Next(0, poll.Count)];
		else
			return null;
	}
}