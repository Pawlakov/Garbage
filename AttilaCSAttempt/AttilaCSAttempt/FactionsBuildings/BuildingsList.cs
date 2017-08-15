using System;
using System.Collections.Generic;
using System.Xml;
class BuildingsList
{
	private List<BuildingBranch>[][] trueBuildings;
	private List<BuildingBranch>[][] fakeBuildings;
	//
	public string FactionName
	{
		get
		{
			return factionName;
		}
	}
	public BuildingsList(string factionName)
	{
		this.factionName = factionName;
		Reload();
	}
	public void Renew()
	{
		// Wyczyść listy
		for (int whichBuildingType = 0; whichBuildingType < AttilaSimulator.constBuildingTypesNumber; whichBuildingType++)
		{
			for (int whichResourceType = 0; whichResourceType < AttilaSimulator.constResourceTypesNumber; whichResourceType++)
			{
				fakeBuildings[whichBuildingType][whichResourceType].Clear();
			}
		}
		//

		// Kopiuj referencje z prawdziwej listy
		for (int whichBuildingType = 0; whichBuildingType < AttilaSimulator.constBuildingTypesNumber; whichBuildingType++)
		{
			for (int whichResourceType = 0; whichResourceType < AttilaSimulator.constResourceTypesNumber; whichResourceType++)
			{
				for (int whichBuilding = 0; whichBuilding < trueBuildings[whichBuildingType][whichResourceType].Count; whichBuilding++)
				{
					fakeBuildings[whichBuildingType][whichResourceType].Add(trueBuildings[whichBuildingType][whichResourceType][whichBuilding]);
				}
			}
		}
		//
	}
	public void Reload()
	{
		// Wyczyść listy
		for (int whichBuildingType = 0; whichBuildingType < AttilaSimulator.constBuildingTypesNumber; whichBuildingType++)
		{
			for (int whichResourceType = 0; whichResourceType < AttilaSimulator.constResourceTypesNumber; whichResourceType++)
			{
				trueBuildings[whichBuildingType][whichResourceType].Clear();
			}
		}
		//
		// Wczytaj z pliku
		List<string> buildingLines = new List<string>();

		StreamReader reader = new StreamReader(factionName + ".txt");
		string line = reader.ReadLine();

		while (line != null)
		{
			buildingLines.Add(line);
			line = reader.ReadLine();
		}
		reader.Close();
		//
		// Twórz budynki
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
				trueBuildings[(int)tempBuilding.typeTag][(int)tempBuilding.resourceTag].Add(tempBuilding);

				beginLine = whichLine + 1;
			}
		}
		//
		Renew();
	}
	public void Remove()
	{

	}
}
struct BuildingLibrary
{
	private BuildingBranch[][] buildings;
	//
	public BuildingLibrary(string filename)
	{
		XmlDocument sourceFile = new XmlDocument();
		sourceFile.Load(filename);
		buildings = new BuildingBranch[AttilaSimulator.constBuildingTypesNumber][];
		// Something goes here.
	}
}