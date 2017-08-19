using System;
static class Generator
{
	public static void GenerateCombinationInTermsOfWealth(BuildingSlot[][] slots, ProvinceData province, Faction faction, short minimalOrder)
	{
		ProvinceCombination currentBest = null;
		int whichCombination = 0;
		long whichLoop = 0;
		while (true)
		{
			ProvinceCombination subject = new ProvinceCombination(slots, province, faction);
			if (subject.Order > minimalOrder)
			{
				if (currentBest == null || subject.Wealth > currentBest.Wealth)
				{
					currentBest = subject;
					Console.Clear();
					Console.WriteLine("Best: ");
					currentBest.ShowContent();
				}
				whichCombination++;
			}
			whichLoop++;
			Console.WriteLine("Loop: {1} Found: {0}", whichCombination, whichLoop);
			Console.CursorTop -= 1;
		}
	}
	public static void GenerateCombinationInTermsOfFood(BuildingSlot[][] slots, ProvinceData province, Faction faction, short minimalOrder)
	{
		ProvinceCombination currentBest = null;
		int whichCombination = 0;
		long whichLoop = 0;
		while (true)
		{
			ProvinceCombination subject = new ProvinceCombination(slots, province, faction);
			if (subject.Order > minimalOrder)
			{
				if (currentBest == null || subject.Food > currentBest.Food)
				{
					currentBest = subject;
					Console.Clear();
					Console.WriteLine("Best: ");
					currentBest.ShowContent();
				}
				whichCombination++;
			}
			whichLoop++;
			Console.WriteLine("Loop: {1} Found: {0}", whichCombination, whichLoop);
			Console.CursorTop -= 1;
		}
	}
	public static BuildingSlot[][] GenerateSlotsArray(ProvinceData province)
	{
		BuildingSlot[][] slots;
		slots = new BuildingSlot[province.NumberOfRegions][];
		for (byte whichRegion = 0; whichRegion < slots.Length; whichRegion++)
		{
			slots[whichRegion] = new BuildingSlot[province[whichRegion].NumberOfSlots];
			for (byte whichSlot = 0; whichSlot < slots[whichRegion].Length; whichSlot++)
			{
				slots[whichRegion][whichSlot] = new BuildingSlot(province[whichRegion], whichSlot);
			}
		}
		return slots;
	}
	public static BuildingSlot[][] GenerateSlotsCopy(BuildingSlot[][] slots)
	{
		BuildingSlot[][] result = new BuildingSlot[slots.Length][];
		for (byte whichRegion = 0; whichRegion < slots.Length; whichRegion++)
		{
			result[whichRegion] = new BuildingSlot[slots[whichRegion].Length];
			for (byte whichSlot = 0; whichSlot < slots[whichRegion].Length; whichSlot++)
				result[whichRegion][whichSlot] = new BuildingSlot(slots[whichRegion][whichSlot]);
		}
		return result;
	}
}