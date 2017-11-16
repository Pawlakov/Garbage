using System;
using System.Collections.Generic;
static class Generator
{
	public static void Generate(ProvinceCombination template, short minimalOrder, Comparison<ProvinceCombination> condition, sbyte powerMax, sbyte powerMin)
	{
		int roundSize = (int)Math.Pow(2, powerMax);
		int lastListSize = (int)Math.Pow(2, powerMin);
		List<ProvinceCombination> valid = new List<ProvinceCombination>(roundSize);
		ProvinceCombination bestValid = null;
		int currentCapacity = roundSize;
		uint doneRounds = 0;
		uint doneCombinations = 0;
		uint doneValid = 0;
		bool test = true;
		Console.Clear();
		while (true)
		{
			while (doneValid % roundSize != 0 || test == true)
			{
				ProvinceCombination subject = new ProvinceCombination(template);
				subject.Fill();
				if (subject.Order >= minimalOrder)
				{
					valid.Add(subject);
					valid.Sort(condition);
					{
						if (valid.Count > currentCapacity)
							valid.RemoveAt(0);
					}
					doneValid++;
					test = false;
				}
				doneCombinations++;
				Console.WriteLine("Rounds: {0} | Combinations: {1} | Valid C.: {2}/{3} | Best List: {4}/{5}", doneRounds, doneCombinations, doneValid, roundSize, valid.Count, currentCapacity);
				Console.CursorTop -= 1;
			}
			bestValid = valid[currentCapacity - 1];
			for (int whichCombination = 0; whichCombination < currentCapacity; whichCombination++)
			{
				valid[whichCombination].RewardUsefulBuildings();
			}
			bestValid.CurbUselessBuildings();
			Console.Clear();
			Console.WriteLine("Best after last round: ");
			bestValid.ShowContent();
			if (currentCapacity > lastListSize)
			{
				currentCapacity /= 2;
				for (int whichCombination = 0; whichCombination < currentCapacity; whichCombination++)
				{
					valid.RemoveAt(0);
				}
				valid.Capacity = currentCapacity;
			}
			doneCombinations = 0;
			doneValid = 0;
			doneRounds++;
			test = true;
		}
	}
}
