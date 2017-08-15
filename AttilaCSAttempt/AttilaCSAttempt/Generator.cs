using System;
static class Generator
{
	public static ProvinceCombination GenerateProvinceCombination(SimData data, int whichProvince)
	{
		ProvinceCombination currentBest = null;

		int whichCombination = 0;
		long whichLoop = 0;
		while (true)
		{
			Console.Clear();
			Console.WriteLine("Found: " + whichCombination);
			Console.WriteLine("Loop: " + whichLoop);
			if(whichCombination != 0)
				Console.WriteLine("Loop/Found: " + whichLoop / whichCombination);
			if (currentBest != null)
			{
				Console.WriteLine("Best: ");
				currentBest.PrintListing();
			}

			ProvinceCombination subject = new ProvinceCombination(data, whichProvince);
			Console.WriteLine("Contestant: ");
			subject.PrintListing();
			if (subject.FitsConditions())
			{
				if (currentBest == null)
					currentBest = subject;
				else
				{
					if (subject.totalWealth > currentBest.totalWealth)
						currentBest = subject;
				}
				whichCombination++;
			}
			whichLoop++;
		}
	}
	public static Building GenerateBuilding(BuildingSlotMask mask)
	{
		if (mask.ForcedLevel.HasValue)
		{

		}
		else
		{
			Random random = new Random();
		}
	}
}