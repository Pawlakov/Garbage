using System;
using System.Collections.Generic;
using System.Xml;
namespace TWAssistant
{
	namespace Rome2
	{
		class BuildingLibrary
		{
			private List<BuildingBranch>[] buildings;
			//
			public BuildingLibrary(string filename)
			{
				XmlNodeList[] nodeList;
				XmlDocument sourceFile = new XmlDocument();
				sourceFile.Load(filename);
				buildings = (new List<BuildingBranch>[R2_Simulator.BuildingTypesCount]);
				nodeList = new XmlNodeList[R2_Simulator.BuildingTypesCount];
				for (byte whichType = 0; whichType < buildings.Length; whichType++)
				{
					nodeList[whichType] = sourceFile.SelectNodes("//branch[@type=\"" + ((BuildingType)whichType).ToString() + "\"]");
					buildings[whichType] = new List<BuildingBranch>();
					for (byte whichBuilding = 0; whichBuilding < nodeList[whichType].Count; whichBuilding++)
						buildings[whichType].Add(new BuildingBranch(nodeList[whichType][whichBuilding]));
				}
			}
			public BuildingLibrary(BuildingLibrary source)
			{
				buildings = new List<BuildingBranch>[R2_Simulator.BuildingTypesCount];
				for (byte whichType = 0; whichType < buildings.Length; whichType++)
				{
					buildings[whichType] = new List<BuildingBranch>();
					for (byte whichBuilding = 0; whichBuilding < source[(BuildingType)whichType].Count; whichBuilding++)
						buildings[whichType].Add(source[(BuildingType)whichType, whichBuilding]);
				}
			}
			//
			public void ShowListOneType(BuildingType type)
			{
				for (byte whichBuilding = 0; whichBuilding < buildings[(byte)type].Count; whichBuilding++)
				{
					Console.WriteLine("{0}. {1}", whichBuilding, buildings[(byte)type][whichBuilding].Name);
				}
			}
			public void ResetUsefuliness()
			{
				for (byte whichType = 0; whichType < buildings.Length; whichType++)
				{
					for (byte whichBuilding = 0; whichBuilding < buildings[whichType].Count; whichBuilding++)
					{
						buildings[whichType][whichBuilding].Usefuliness = 0;
					}
				}
			}
			//
			public BuildingBranch this[BuildingType type, byte whichBuilding]
			{
				get { return buildings[(byte)type][whichBuilding]; }
			}
			public List<BuildingBranch> this[BuildingType type]
			{
				get { return buildings[(byte)type]; }
			}
		}
	}
}