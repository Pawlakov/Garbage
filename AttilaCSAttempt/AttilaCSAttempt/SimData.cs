using System;
using System.Collections.Generic;
using System.IO;

class SimData
{
	public List<ProvinceData> map;
	public List<Building>[][] buildings; // I po co ja tworzę takie molochy? No po co?

	// ZROBIONE!
	public SimData()
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

	private void LoadMap() //Wydaje się gotowe (To coś nowego).
	{
		List<string> lines = new List<string>();

		StreamReader reader = new StreamReader("Map.txt");
		string line = reader.ReadLine();

		while (line != null)
		{
			lines.Add(line);
			line = reader.ReadLine();
		}
		reader.Close();

		for (int whichLine = 0; whichLine < lines.Count; whichLine++)
		{
			map.Add(new ProvinceData(lines[whichLine]));
		}
	}

	private void LoadBuildings()
	{
		List<string> lines = new List<string>();

		StreamReader reader = new StreamReader("Buildings.txt");
		string line = reader.ReadLine();

		while (line != null)
		{
			lines.Add(line);
			line = reader.ReadLine();
		}
		reader.Close();

		int beginLine = 0;
		int endLine = 0;
		for (int whichLine = 0; whichLine < lines.Count; whichLine++)
		{
			if (lines[whichLine] == "X")
			{
				endLine = whichLine - 1;

				string[] tempLines;
				tempLines = new string[endLine - beginLine + 1];
				for (int whichTempLine = 0; whichTempLine < tempLines.Length; whichTempLine++)
				{
					tempLines[whichTempLine] = lines[beginLine + whichTempLine];
				}
				Building tempBuilding = new Building(tempLines);
				buildings[(int)tempBuilding.typeTag][(int)tempBuilding.resourceTag].Add(tempBuilding);

				beginLine = whichLine + 1;
				// Jak to będzie działać to ja jestem miszcz.
				// EDIT: Not to jestem miscz.
			}
		}
	}

	public void RefreshBuildings()
	{
		for (int whichBuildingType = 0; whichBuildingType < AttilaSimulator.constBuildingTypesNumber; whichBuildingType++)
		{
			for (int whichResourceType = 0; whichResourceType < AttilaSimulator.constResourceTypesNumber; whichResourceType++)
			{
				buildings[whichBuildingType][whichResourceType] = new List<Building>();
			}
		}

		LoadBuildings();
	}
}

