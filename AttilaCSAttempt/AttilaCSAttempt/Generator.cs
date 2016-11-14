using System;
using System.Collections.Generic;
using System.IO;

// zawiera generatory różnych rzeczy
class Generator
{
	// Zwraca najlepszy budynek. ZROBIONE!
	public static ProvinceCombination GenerateProvinceCombination(SimData data, int whichProvince)
	{
		// Tylko kombinacje spełniające podstawowe warunki i nadające się do porównania.
		ProvinceCombination[] combinations = new ProvinceCombination[8];
		//

		// Zapełnianie tablicy kandydatów.
		int whichCombination = 0;
		long whichLoop = 0;
		while(combinations[7] == null)
		{
			Console.Clear();
			Console.WriteLine("Znaleziono: " + whichCombination);
			Console.WriteLine("Przebieg: " + whichLoop);
			ProvinceCombination subject = new ProvinceCombination(data, whichProvince);
			subject.Calculate();
			if(subject.FitsConditions())
			{
				combinations[whichCombination] = subject;
				whichCombination++;
			}
			whichLoop++;
		}
		//

		// Zwróć najlepszego.
		ProvinceCombination bestOne = combinations[0];
		for(whichCombination = 1; whichCombination < combinations.Length; whichCombination++)
		{
			if (combinations[whichCombination].totalWealth > bestOne.totalWealth)
				bestOne = combinations[whichCombination];
		}
		return bestOne;
		//
	}

	// Zwraca wylosowany budynek na podstawie przekazanych poleceń. ZROBIONE!
	public static Building GenerateBuilding(BuildingType type, Resource resource, SimData data)
	{
		Random random = new Random();
		List<Building> poll = data.buildings[(int)type][(int)resource];
		if (poll.Count >= 1)
		{
			int position = random.Next(poll.Count);
			Building result =  poll[position];
			poll.RemoveAt(position);
			return result; // Miesza się prawdopodobnie dlatego że odświeżając budynki gmatwają się referencje.
		}
		else
			return null;
	}
}