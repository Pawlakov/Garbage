using System.Collections.Generic;

class FactionsList
{
	private List<BuildingsList> factions;

	public BuildingsList this[int whichFaction]
	{
		get
		{
			return factions[whichFaction];
		}
	}

	public FactionsList()
	{
		for(Faction whichFaction = 0; (int)whichFaction < AttilaSimulator.consFactionsNumber; whichFaction++)
		{
			//
		}
	}

	public void PrintList()
	{

	}
}