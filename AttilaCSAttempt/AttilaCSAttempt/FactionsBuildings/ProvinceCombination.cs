using System;
class ProvinceCombination
{
    private ProvinceData province;
    private Faction faction;
    private BuildingSlot[][] slots;
    private ProvinceWealth wealth;
    private short food;
    private short order;
    private double totalWealth;
    private bool isCurrent;
    //
    private void HarvestBuildings()
    {
        food = faction.Food;
        order = faction.Order;
        order += Difficulty.PublicOrderBonus;
        wealth = new ProvinceWealth();
        if (faction.WealthBonuses != null)
            for (byte whichBonus = 0; whichBonus < faction.WealthBonuses.Length; whichBonus++)
                wealth.AddBonus(faction.WealthBonuses[whichBonus]);
        for (byte whichRegion = 0; whichRegion < slots.Length; whichRegion++)
        {
            for (byte whichSlot = 0; whichSlot < slots[whichRegion].Length; whichSlot++)
            {
                food += slots[whichRegion][whichSlot].BuildingBranch.Value.GetFood(slots[whichRegion][whichSlot].Level.Value);
                order += slots[whichRegion][whichSlot].BuildingBranch.Value.GetOrder(slots[whichRegion][whichSlot].Level.Value);
                for (byte whichBonus = 0; whichBonus < slots[whichRegion][whichSlot].BuildingBranch.Value.GetWealthBonuses(slots[whichRegion][whichSlot].Level.Value).Length; whichBonus++)
                    wealth.AddBonus(slots[whichRegion][whichSlot].BuildingBranch.Value.GetWealthBonuses(slots[whichRegion][whichSlot].Level.Value)[whichBonus]);
            }
        }
        totalWealth = wealth.Wealth;
    }
    //
    public ProvinceCombination(BuildingSlot[][] iniSlots, ProvinceData iniProvince, Faction faction)
    {
        slots = Generator.GenerateSlotsCopy(iniSlots);
        province = iniProvince;
        isCurrent = false;
        Random random = new Random();
        for (byte whichRegion = 0; whichRegion < slots.Length; whichRegion++)
        {
            BuildingLibrary buildings = new BuildingLibrary(faction.Buildings);
            for (byte whichSlot = 0; whichSlot < slots[whichRegion].Length; whichSlot++)
            {
                if (slots[whichRegion][whichSlot].BuildingBranch != null)
                {
                    buildings[slots[whichRegion][whichSlot].Type].Remove(slots[whichRegion][whichSlot].BuildingBranch.Value);
                }
            }
            for (byte whichBuilding = 0; whichBuilding < slots[whichRegion].Length; whichBuilding++)
            {
                if (slots[whichRegion][whichBuilding].BuildingBranch == null)
                {
                    if (slots[whichRegion][whichBuilding].Type == BuildingType.RESOURCE)
                        slots[whichRegion][whichBuilding].BuildingBranch = buildings[BuildingType.RESOURCE][(int)province[whichRegion].Resource];
                    else
                    {
                        int randomPick = random.Next(0, buildings[slots[whichRegion][whichBuilding].Type].Count - 1);
                        slots[whichRegion][whichBuilding].BuildingBranch = buildings[slots[whichRegion][whichBuilding].Type][randomPick];
                        buildings[slots[whichRegion][whichBuilding].Type].RemoveAt(randomPick);
                    }
                }
                if (slots[whichRegion][whichBuilding].Level == null)
                    slots[whichRegion][whichBuilding].Level = (byte)random.Next(0, slots[whichRegion][whichBuilding].BuildingBranch.Value.NumberOfLevels);
            }
        }
    }
    public void ShowContent()
    {
        Rome2Simulator.ShowSlots(slots, province);
        Console.WriteLine("Wealth: {0} Food: {1} Order: {2}", Wealth, Food, Order);
    }
    public BuildingSlot this[byte whichRegion, byte whichSlot]
    {
        get
        {
            return slots[whichRegion][whichSlot];
        }
    }
    public short Food
    {
        get
        {
            if (!isCurrent)
                HarvestBuildings();
            return food;
        }
    }
    public short Order
    {
        get
        {
            if (!isCurrent)
                HarvestBuildings();
            return order;
        }
    }
    public double Wealth
    {
        get
        {
            if (!isCurrent)
                HarvestBuildings();
            return totalWealth;
        }
    }
	public void RewardUsefulBuildings()
	{
		for(byte whichRegion = 0; whichRegion < slots.Length; whichRegion++)
		{
			for(byte whichSlot = 0; whichSlot < slots[whichRegion].Length; whichSlot++)
			{
				slots[whichRegion][whichSlot].BuildingBranch.Value.Usefuliness += 1;
			}
		}
	}
}