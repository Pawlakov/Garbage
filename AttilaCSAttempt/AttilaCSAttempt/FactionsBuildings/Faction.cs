using System;
using System.Collections.Generic;
/// <summary>
/// Contains information concerning a single faction (available buildings and bonuses).
/// </summary>
struct Faction
{
	private BuildingsList buildings;
	private List<WealthBonus> wealthBonuses;
	private byte foodBonus;
	private byte publicOrderBonus;
	//
	public Faction(string[] lines)
	{
		
	}
}