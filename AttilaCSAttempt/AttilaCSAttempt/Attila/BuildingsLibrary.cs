using System;
using System.Collections.Generic;
using System.Xml;
namespace TWAssistant
{
	namespace Attila
	{
		public class BuildingLibrary
		{
			BuildingBranch cityCivilBuilding;
			BuildingBranch townCivilBuilding;
			BuildingBranch coastBuilding;
			readonly BuildingBranch[] resourceBuildings;
			//
			readonly List<BuildingBranch> cityBuildings;
			readonly List<BuildingBranch> townBuildings;
			//
			public BuildingLibrary(string filename, Religion stateReligion, bool useLegacy)
			{
				cityBuildings = new List<BuildingBranch>();
				townBuildings = new List<BuildingBranch>();
				resourceBuildings = new BuildingBranch[Simulator.ResourceTypesCount];
				//
				XmlDocument sourceFile = new XmlDocument();
				sourceFile.Load(filename);
				XmlNodeList[] nodeList = new XmlNodeList[Simulator.BuildingTypesCount];
				for (int whichType = 0; whichType < nodeList.Length; ++whichType)
					nodeList[whichType] = sourceFile.SelectNodes("//branch[@t=\"" + ((BuildingType)whichType).ToString() + "\"]");
				//
				if (nodeList[(int)BuildingType.CENTERCITY].Count > 1 || nodeList[(int)BuildingType.CENTERCITY].Count > 1 || nodeList[(int)BuildingType.COAST].Count > 1)
					throw new Exception("More than one civil or coast building.");
				cityCivilBuilding = new BuildingBranch(nodeList[(int)BuildingType.CENTERCITY][0], stateReligion, useLegacy);
				townCivilBuilding = new BuildingBranch(nodeList[(int)BuildingType.CENTERTOWN][0], stateReligion, useLegacy);
				coastBuilding = new BuildingBranch(nodeList[(int)BuildingType.COAST][0], stateReligion, useLegacy);
				foreach (XmlNode node in nodeList[(int)BuildingType.RESOURCE])
				{
					Resource resource;
					Enum.TryParse(node.Attributes.GetNamedItem("r").InnerText, out resource);
					if (resourceBuildings[(int)resource] != null)
						throw new Exception("Two building for one resource.");
					resourceBuildings[(int)resource] = new BuildingBranch(node, stateReligion, useLegacy);
				}
				BuildingBranch temporaryBranch;
				foreach (XmlNode node in nodeList[(int)BuildingType.CITY])
				{
					temporaryBranch = new BuildingBranch(node, stateReligion, useLegacy);
					if (!temporaryBranch.isReligionExclusive || temporaryBranch.religion == stateReligion)
						cityBuildings.Add(temporaryBranch);
				}
				foreach (XmlNode node in nodeList[(int)BuildingType.TOWN])
				{
					temporaryBranch = new BuildingBranch(node, stateReligion, useLegacy);
					if (!temporaryBranch.isReligionExclusive || temporaryBranch.religion == stateReligion)
						townBuildings.Add(temporaryBranch);
				}
			}
			public BuildingLibrary(BuildingLibrary source)
			{
				cityCivilBuilding = source.cityCivilBuilding;
				townCivilBuilding = source.townCivilBuilding;
				coastBuilding = source.coastBuilding;
				resourceBuildings = source.resourceBuildings;
				//
				cityBuildings = new List<BuildingBranch>(source.cityBuildings);
				townBuildings = new List<BuildingBranch>(source.townBuildings);
			}
			//
			public void ShowListOneType(BuildingType type)
			{
				List<BuildingBranch> list;
				switch (type)
				{
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
						Console.WriteLine("{0}. {1}", whichBuilding, list[whichBuilding].name);
					}
				else
					Console.WriteLine("No list for this type.");
			}
			public BuildingBranch GetExactBuilding(BuildingType type, int choice)
			{
				List<BuildingBranch> list;
				switch (type)
				{
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
					return list[choice];
				Console.WriteLine("What do you think you're doing?");
				return null;
			}
			public int GetCountByType(BuildingType type)
			{
				List<BuildingBranch> list;
				switch (type)
				{
					case BuildingType.CITY:
						list = cityBuildings;
						break;
					case BuildingType.TOWN:
						list = townBuildings;
						break;
					case BuildingType.COAST:
						return coastBuilding.NonVoidCount;
					default:
						return 0;
				}
				int result = 0;
				foreach (BuildingBranch building in list)
				{
					result += building.NonVoidCount;
				}
				return result;
			}
			public void EvaluateBuildings()
			{
				List<BuildingBranch> list;
				coastBuilding.EvalueateLevels();
				list = cityBuildings;
				EvaluationHelper(list);
				list = townBuildings;
				EvaluationHelper(list);
			}
			void EvaluationHelper(List<BuildingBranch> list)
			{
				BuildingBranch building;
				for (int whichBuilding = 0; whichBuilding < list.Count; ++whichBuilding)
				{
					building = list[whichBuilding];
					building.EvalueateLevels();
					if (building.NonVoidCount == 0)
					{
						list.RemoveAt(whichBuilding);
						--whichBuilding;
					}
				}
			}
			public BuildingBranch GetBuilding(Resource resource)
			{
				return resourceBuildings[(int)resource];
			}
			public BuildingBranch GetBuilding(Random random, BuildingType type)
			{
				BuildingBranch result;
				switch (type)
				{
					case BuildingType.CENTERCITY:
						result = cityCivilBuilding;
						break;
					case BuildingType.CENTERTOWN:
						result = townCivilBuilding;
						break;
					case BuildingType.COAST:
						result = coastBuilding;
						break;
					case BuildingType.CITY:
						result = cityBuildings[random.Next(0, cityBuildings.Count)];
						cityBuildings.Remove(result);
						break;
					case BuildingType.TOWN:
						result = townBuildings[random.Next(0, townBuildings.Count)];
						townBuildings.Remove(result);
						break;
					default:
						throw new Exception("Here BuildingType should not be RESOURCE.");
				}
				return result;
			}
			public void Remove(BuildingBranch building)
			{
				switch (building.type)
				{
					case BuildingType.CITY:
						cityBuildings.Remove(building);
						break;
					case BuildingType.TOWN:
						townBuildings.Remove(building);
						break;
					default:
						throw new Exception("What are you doing? You can't remove this building.");
				}
			}
		}
	}
}