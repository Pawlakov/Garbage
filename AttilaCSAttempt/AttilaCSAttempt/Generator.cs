using System;
using System.Collections.Generic;
using System.IO;

// zawiera generatory różnych rzeczy
class Generator
{
	// Zwraca najlepszy budynek. ZROBIONE!
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

	// Zwraca wylosowany budynek na podstawie przekazanych poleceń. ZROBIONE!
	public static Building GenerateBuilding(BuildingType type, Resource resource, SimData data)
	{
		Random random = new Random();
		if (data.buildings[(int)type][(int)resource].Count >= 1)
		{
			int pick = random.Next(data.buildings[(int)type][(int)resource].Count);
			Building result = data.buildings[(int)type][(int)resource][pick];
			data.buildings[(int)type][(int)resource].RemoveAt(pick);
			return result; // Tu coś się wali bo wywala budynki jeden po drugim jak w liście siedzą.
		}
		else
			return null;
	}
}