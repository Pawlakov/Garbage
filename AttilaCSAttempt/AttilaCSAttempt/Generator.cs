using System;
using System.Diagnostics;
static class Generator
{
	public static void GenerateCombinationInTermsOfWealth(BuildingSlot[][] slots, ProvinceData province, Faction faction, short minimalOrder)
	{
		ProvinceCombination currentBest = null;
		long whichCombination = 0;
		long whichLoop = 1;
		long time = 0;
		Stopwatch stopwatch = Stopwatch.StartNew();
		while (true)
		{
			if(whichLoop%16384 == 0)
			{
				stopwatch.Stop();
				time = stopwatch.ElapsedMilliseconds;
				stopwatch = Stopwatch.StartNew();
				faction.CurbUselessBuildings();
			}
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
				subject.RewardUsefulBuildings();
				whichCombination++;
			}
			whichLoop++;
			Console.WriteLine("Loop: {1} Found: {0} Time (16.484 loops): {2}", whichCombination, whichLoop, time);
			Console.CursorTop -= 1;
		}
	}
	public static void GenerateCombinationInTermsOfFood(BuildingSlot[][] slots, ProvinceData province, Faction faction, short minimalOrder)
	{
		ProvinceCombination currentBest = null;
		int whichCombination = 0;
		long whichLoop = 1;
		long time = 0;
		Stopwatch stopwatch = Stopwatch.StartNew();
		while (true)
		{
			if (whichLoop % 16384 == 0)
			{
				stopwatch.Stop();
				time = stopwatch.ElapsedMilliseconds;
				stopwatch = Stopwatch.StartNew();
				faction.CurbUselessBuildings();

			}
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
				subject.RewardUsefulBuildings();
				whichCombination++;
			}
			whichLoop++;
			Console.WriteLine("Loop: {1} Found: {0} Time (16.384 loops): {2}", whichCombination, whichLoop, time);
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