using System.Collections.Generic;
using System.IO;

class SimData
{
	private Map map;
	private FactionsList factions;
	private BuildingsList buildings;
	List<string> buildingLines;

	public SimData()
	{
		map = new Map();

		buildings = new List<Building>[AttilaSimulator.constBuildingTypesNumber][];
		for (int whichBuildingType = 0; whichBuildingType < AttilaSimulator.constBuildingTypesNumber; whichBuildingType++)
		{
			buildings[whichBuildingType] = new List<Building>[AttilaSimulator.constResourceTypesNumber];
			for (int whichResourceType = 0; whichResourceType < AttilaSimulator.constResourceTypesNumber; whichResourceType++)
			{
				buildings[whichBuildingType][whichResourceType] = new List<Building>();
			}
		}

		LoadBuildings();
	}

	private void LoadBuildings()
	{
		buildingLines = new List<string>();

		StreamReader reader = new StreamReader("Buildings.txt");
		string line = reader.ReadLine();

		while (line != null)
		{
			buildingLines.Add(line);
			line = reader.ReadLine();
		}
		reader.Close();

		// SEPARATOR

		int beginLine = 0;
		int endLine = 0;
		for (int whichLine = 0; whichLine < buildingLines.Count; whichLine++)
		{
			if (buildingLines[whichLine] == "X")
			{
				endLine = whichLine - 1;

				string[] tempLines;
				tempLines = new string[endLine - beginLine + 1];
				for (int whichTempLine = 0; whichTempLine < tempLines.Length; whichTempLine++)
				{
					tempLines[whichTempLine] = buildingLines[beginLine + whichTempLine];
				}
				Building tempBuilding = new Building(tempLines);
				buildings[(int)tempBuilding.typeTag][(int)tempBuilding.resourceTag].Add(tempBuilding);

				beginLine = whichLine + 1;
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

		int beginLine = 0;
		int endLine = 0;
		for (int whichLine = 0; whichLine < buildingLines.Count; whichLine++)
		{
			if (buildingLines[whichLine] == "X")
			{
				endLine = whichLine - 1;

				string[] tempLines;
				tempLines = new string[endLine - beginLine + 1];
				for (int whichTempLine = 0; whichTempLine < tempLines.Length; whichTempLine++)
				{
					tempLines[whichTempLine] = buildingLines[beginLine + whichTempLine];
				}
				Building tempBuilding = new Building(tempLines);
				buildings[(int)tempBuilding.typeTag][(int)tempBuilding.resourceTag].Add(tempBuilding);

				beginLine = whichLine + 1;
			}
		}
	}
}

