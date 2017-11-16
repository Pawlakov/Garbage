using System;
using System.Xml;
struct Faction
{
	private string name;
	private BuildingLibrary buildings;
	private short food;
	private short order;
	private WealthBonus[] wealthBonuses;
	//
	public Faction(XmlNode factionNode)
	{
		XmlNodeList bonusNodesList = factionNode.ChildNodes;
		name = factionNode.Attributes.GetNamedItem("name").InnerText;
		buildings = new BuildingLibrary(factionNode.Attributes.GetNamedItem("buildings").InnerText);
		food = Convert.ToInt16(factionNode.Attributes.GetNamedItem("food").InnerText);
		order = Convert.ToInt16(factionNode.Attributes.GetNamedItem("order").InnerText);
		wealthBonuses = new WealthBonus[bonusNodesList.Count];
		for (byte whichBonus = 0; whichBonus < wealthBonuses.Length; whichBonus++)
		{
			wealthBonuses[whichBonus] = new WealthBonus(bonusNodesList[whichBonus]);
		}
	}
	//
	public string Name
	{
		get { return name; }
	}
	public BuildingLibrary Buildings
	{
		get { return buildings; }
	}
	public short Food
	{
		get { return food; }
	}
	public short Order
	{
		get { return order; }
	}
	public WealthBonus[] WealthBonuses
	{
		get { return wealthBonuses; }
	}
	//
	public System.Collections.Generic.List<string> CurbUselessBuildings()
	{
		System.Collections.Generic.List<string> result = new System.Collections.Generic.List<string>();
		for (byte whichType = 0; whichType < Rome2Simulator.constBuildingTypesNumber; whichType++)
		{
			for (byte whichBuilding = 0; whichBuilding < buildings[(BuildingType)whichType].Count; whichBuilding++)
			{
				if (buildings[(BuildingType)whichType, whichBuilding].Usefuliness == 0)
				{
					result.Add(buildings[(BuildingType)whichType][whichBuilding].Name);
					buildings[(BuildingType)whichType].RemoveAt(whichBuilding);
					whichBuilding--;
				}
			}
		}
		buildings.ResetUsefuliness();
		return result;
	}
}