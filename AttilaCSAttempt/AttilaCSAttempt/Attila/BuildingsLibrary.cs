using System;
using System.Collections.Generic;
using System.Xml;
namespace TWAssistant
{
	namespace Attila
	{
		class BuildingLibrary
		{
			private BuildingBranch cityCivilbuilding;
			private BuildingBranch townCivilbuilding;
			private List<BuildingBranch>[] resourceBuildings;
			//
			private List<BuildingBranch> coastBuildings;
			private List<BuildingBranch> cityBuildings;
			private List<BuildingBranch> townBuildings;
			//
			public BuildingLibrary(string filename)
			{
				XmlDocument sourceFile = new XmlDocument();
				sourceFile.Load(filename);
				XmlNodeList[] nodeList = new XmlNodeList[Simulator.BuildingTypesCount];
				//
				coastBuildings = new List<BuildingBranch>();
				cityBuildings = new List<BuildingBranch>();
				townBuildings = new List<BuildingBranch>();
				resourceBuildings = new List<BuildingBranch>[Simulator.ResourceTypesCount];
				for (int whichResource = 0; whichResource < Simulator.ResourceTypesCount; ++whichResource)
					resourceBuildings[whichResource] = new List<BuildingBranch>();
				//
				for (int whichType = 0; whichType < nodeList.Length; ++whichType)
					nodeList[whichType] = sourceFile.SelectNodes("//branch[@t=\"" + ((BuildingType)whichType).ToString() + "\"]");
				if (nodeList[(int)BuildingType.CENTERCITY].Count > 1 || nodeList[(int)BuildingType.CENTERCITY].Count > 1)
					Console.WriteLine("ERROR: More than one civil building!");
				cityCivilbuilding = new BuildingBranch(nodeList[(int)BuildingType.CENTERCITY][0]);
				townCivilbuilding = new BuildingBranch(nodeList[(int)BuildingType.CENTERTOWN][0]);
				foreach (XmlNode node in nodeList[(int)BuildingType.RESOURCE])
				{
					Resource resource;
					Enum.TryParse(node.Attributes.GetNamedItem("r").InnerText, out resource);
					resourceBuildings[(int)resource].Add(new BuildingBranch(node));
				}
				foreach (XmlNode node in nodeList[(int)BuildingType.COAST])
					coastBuildings.Add(new BuildingBranch(node));
				foreach (XmlNode node in nodeList[(int)BuildingType.CITY])
					cityBuildings.Add(new BuildingBranch(node));
				foreach (XmlNode node in nodeList[(int)BuildingType.TOWN])
					townBuildings.Add(new BuildingBranch(node));
			}
			public BuildingLibrary(BuildingLibrary source)
			{
				cityCivilbuilding = source.cityCivilbuilding;
				townCivilbuilding = source.townCivilbuilding;
				resourceBuildings = source.resourceBuildings;
				//
				coastBuildings = new List<BuildingBranch>(source.coastBuildings);
				cityBuildings = new List<BuildingBranch>(source.cityBuildings);
				townBuildings = new List<BuildingBranch>(source.townBuildings);
			}
			//
			public void ShowListOneType(BuildingType type)
			{
				List<BuildingBranch> list;
				switch (type)
				{
					case BuildingType.COAST:
						list = coastBuildings;
						break;
					case BuildingType.CITY:
						list = cityBuildings;
						break;
					case BuildingType.TOWN:
						list = townBuildings;
						break;
					default:
						list = null;
						break;
				}
				if (list != null)
					for (int whichBuilding = 0; whichBuilding < list.Count; ++whichBuilding)
					{
						Console.WriteLine("{0}. {1}", whichBuilding, list[whichBuilding].Name);
					}
				else
					Console.WriteLine("No list for this building type!");
			}
			public int GetCountByType(BuildingType type)
			{
				List<BuildingBranch> list;
				switch (type)
				{
					case BuildingType.COAST:
						list = coastBuildings;
						break;
					case BuildingType.CITY:
						list = cityBuildings;
						break;
					case BuildingType.TOWN:
						list = townBuildings;
						break;
					default:
						list = null;
						break;
				}
				if (list != null)
					return list.Count;
				else
				{
					Console.WriteLine("No list for this building type!");
					return 0;
				}
			}
			public void RemoveUselessAndResetUsefuliness()
			{
				List<BuildingBranch> list;
				BuildingBranch building;
				//
				list = coastBuildings;
				for (int whichBuilding = 0; whichBuilding < list.Count; ++whichBuilding)
				{
					building = list[whichBuilding];
					if (building.Usefuliness == 0)
					{
						list.RemoveAt(whichBuilding);
						--whichBuilding;
					}
					else
						building.Usefuliness = 0;
				}
				list = cityBuildings;
				for (int whichBuilding = 0; whichBuilding < list.Count; ++whichBuilding)
				{
					building = list[whichBuilding];
					if (building.Usefuliness == 0)
					{
						list.RemoveAt(whichBuilding);
						--whichBuilding;
					}
					else
						building.Usefuliness = 0;
				}
				list = townBuildings;
				for (int whichBuilding = 0; whichBuilding < list.Count; ++whichBuilding)
				{
					building = list[whichBuilding];
					if (building.Usefuliness == 0)
					{
						list.RemoveAt(whichBuilding);
						--whichBuilding;
					}
					else
						building.Usefuliness = 0;
				}
			}
			public BuildingBranch GetBuilding(Random random, Resource resource)
			{
				int whichResource = (int)resource;
				int buildingsCount = resourceBuildings[(int)resource].Count;
				if (buildingsCount > 1)
				{
					return resourceBuildings[whichResource][random.Next(0, buildingsCount)];
				}
				else
					return resourceBuildings[whichResource][0];
			}
			public BuildingBranch GetBuilding(Random random, BuildingType type)
			{
				BuildingBranch result;
				switch (type)
				{
					case BuildingType.CENTERCITY:
						result = cityCivilbuilding;
						break;
					case BuildingType.CENTERTOWN:
						result = townCivilbuilding;
						break;
					case BuildingType.CITY:
						result = cityBuildings[random.Next(0, cityBuildings.Count)];
						cityBuildings.Remove(result);
						break;
					case BuildingType.TOWN:
						result = townBuildings[random.Next(0, townBuildings.Count)];
						townBuildings.Remove(result);
						break;
					case BuildingType.COAST:
						result = coastBuildings[random.Next(0, coastBuildings.Count)];
						break;
					default:
						Console.WriteLine("ERROR: GetBuilding function with BuildingType argument called with Resource type!");
						result = null;
						break;
				}
				return result;
			}
			public void Remove(BuildingBranch building)
			{
				switch (building.Type)
				{
					case BuildingType.COAST:
						coastBuildings.Remove(building);
						break;
					case BuildingType.CITY:
						cityBuildings.Remove(building);
						break;
					case BuildingType.TOWN:
						townBuildings.Remove(building);
						break;
				}
			}
		}
	}
}