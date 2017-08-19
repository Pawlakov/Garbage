using System;
using System.Collections.Generic;
using System.Xml;
//class BuildingsList
//{
//    private List<BuildingBranch>[][] trueBuildings;
//    private List<BuildingBranch>[][] fakeBuildings;
//    //
//    public string FactionName
//    {
//        get
//        {
//            return factionName;
//        }
//    }
//    public BuildingsList(string filename)
//    {
//        this.factionName = factionName;
//        Reload();
//    }
//    public void Renew()
//    {
//        // Wyczyść listy
//        for (int whichBuildingType = 0; whichBuildingType < Rome2Simulator.constBuildingTypesNumber; whichBuildingType++)
//        {
//            for (int whichResourceType = 0; whichResourceType < Rome2Simulator.constResourceTypesNumber; whichResourceType++)
//            {
//                fakeBuildings[whichBuildingType][whichResourceType].Clear();
//            }
//        }
//        //

//        // Kopiuj referencje z prawdziwej listy
//        for (int whichBuildingType = 0; whichBuildingType < Rome2Simulator.constBuildingTypesNumber; whichBuildingType++)
//        {
//            for (int whichResourceType = 0; whichResourceType < Rome2Simulator.constResourceTypesNumber; whichResourceType++)
//            {
//                for (int whichBuilding = 0; whichBuilding < trueBuildings[whichBuildingType][whichResourceType].Count; whichBuilding++)
//                {
//                    fakeBuildings[whichBuildingType][whichResourceType].Add(trueBuildings[whichBuildingType][whichResourceType][whichBuilding]);
//                }
//            }
//        }
//        //
//    }
//    public void Reload()
//    {
//        // Wyczyść listy
//        for (int whichBuildingType = 0; whichBuildingType < Rome2Simulator.constBuildingTypesNumber; whichBuildingType++)
//        {
//            for (int whichResourceType = 0; whichResourceType < Rome2Simulator.constResourceTypesNumber; whichResourceType++)
//            {
//                trueBuildings[whichBuildingType][whichResourceType].Clear();
//            }
//        }
//        //
//        // Wczytaj z pliku
//        List<string> buildingLines = new List<string>();

//        StreamReader reader = new StreamReader(factionName + ".txt");
//        string line = reader.ReadLine();

//        while (line != null)
//        {
//            buildingLines.Add(line);
//            line = reader.ReadLine();
//        }
//        reader.Close();
//        //
//        // Twórz budynki
//        int beginLine = 0;
//        int endLine = 0;
//        for (int whichLine = 0; whichLine < buildingLines.Count; whichLine++)
//        {
//            if (buildingLines[whichLine] == "X")
//            {
//                endLine = whichLine - 1;

//                string[] tempLines;
//                tempLines = new string[endLine - beginLine + 1];
//                for (int whichTempLine = 0; whichTempLine < tempLines.Length; whichTempLine++)
//                {
//                    tempLines[whichTempLine] = buildingLines[beginLine + whichTempLine];
//                }
//                Building tempBuilding = new Building(tempLines);
//                trueBuildings[(int)tempBuilding.typeTag][(int)tempBuilding.resourceTag].Add(tempBuilding);

//                beginLine = whichLine + 1;
//            }
//        }
//        //
//        Renew();
//    }
//    public void Remove()
//    {

//    }
//}
class BuildingLibrary
{
    private List<BuildingBranch>[] buildings;
    //
    public BuildingLibrary(string filename)
    {
        XmlNodeList[] nodeList;
        XmlDocument sourceFile = new XmlDocument();
        sourceFile.Load(filename);
        buildings = new List<BuildingBranch>[Rome2Simulator.constBuildingTypesNumber];
        nodeList = new XmlNodeList[Rome2Simulator.constBuildingTypesNumber];
        for(byte whichType = 0; whichType < buildings.Length; whichType++)
        {
            nodeList[whichType] = sourceFile.SelectNodes("//branch[@type=\"" + ((BuildingType)whichType).ToString() + "\"]");
            buildings[whichType] = new List<BuildingBranch>();
            for(byte whichBuilding = 0; whichBuilding < nodeList[whichType].Count; whichBuilding++)
                buildings[whichType].Add(new BuildingBranch(nodeList[whichType][whichBuilding]));
        }
    }
	public BuildingLibrary(BuildingLibrary source)
	{
		buildings = new List<BuildingBranch>[Rome2Simulator.constBuildingTypesNumber];
		for (byte whichType = 0; whichType < buildings.Length; whichType++)
		{
			buildings[whichType] = new List<BuildingBranch>();
			for (byte whichBuilding = 0; whichBuilding < source[(BuildingType)whichType].Count; whichBuilding++)
				buildings[whichType].Add(source[(BuildingType)whichType, whichBuilding]);
		}
	}
    public void ShowListOneType(BuildingType type)
    {
        for(byte whichBuilding = 0; whichBuilding < buildings[(byte)type].Count; whichBuilding++)
        {
            Console.WriteLine("{0}. {1}",whichBuilding, buildings[(byte)type][whichBuilding].Name);
        }
    }
    public BuildingBranch this[BuildingType type, byte whichBuilding]
    {
        get { return buildings[(byte)type][whichBuilding]; }
    }
	public List<BuildingBranch> this[BuildingType type]
	{
		get { return buildings[(byte)type]; }
	}
}