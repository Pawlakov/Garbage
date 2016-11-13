using System;
using System.Collections.Generic;

// zawiera gałąź budynku z danymi dla każdego poziomu
class Building
{
	public string name;
	// Sanitacja którą daje tylko regionowi w którym się znajduje.
	public int[] regionSanitation;
	//Sanitacja którą daje każdemu regionowi w prowincji łącznie z tym w którym się znajduje.
	public int[] provinceSanitation;
	public int[] food;
	public int[] order;
	public List<WealthBonus>[] wealthBonuses;

	public BuildingType typeTag;
	public Resource resourceTag;

	public Building(string[] lines)
	{
		// Inicjalizacja.
		regionSanitation = new int[4];
		provinceSanitation = new int[4];
		food = new int[4];
		order = new int[4];
		wealthBonuses = new List<WealthBonus>[4];

		for(int whichLevel = 0; whichLevel < 4; whichLevel++)
		{
			regionSanitation[whichLevel] = 0;
			provinceSanitation[whichLevel] = 0;
			food[whichLevel] = 0;
			order[whichLevel] = 0;
			wealthBonuses[whichLevel] = new List<WealthBonus>();
		}
		//

		int justAfterDotPos = 0;
		int nextDotPos = lines[0].IndexOf('.', justAfterDotPos);
		name = lines[0].Substring(justAfterDotPos, nextDotPos - justAfterDotPos);
		justAfterDotPos = nextDotPos + 1;

		nextDotPos = lines[0].IndexOf('.', justAfterDotPos);
		Enum.TryParse(lines[0].Substring(justAfterDotPos, nextDotPos - justAfterDotPos), out typeTag);
		justAfterDotPos = nextDotPos + 1;

		nextDotPos = lines[0].IndexOf('.', justAfterDotPos);
		Enum.TryParse(lines[0].Substring(justAfterDotPos, nextDotPos - justAfterDotPos), out resourceTag);
		justAfterDotPos = nextDotPos + 1;

		int level = -1;
		for(int whichLine = 1; whichLine < lines.Length; whichLine++)
		{
			// Jeśli natrafiono na nagłówek poziomu.
			if (lines[whichLine][0] == '(')
			{
				level++;
				justAfterDotPos = 1;
				nextDotPos = lines[whichLine].IndexOf('.', justAfterDotPos);
				regionSanitation[level] = Convert.ToInt32(lines[whichLine].Substring(justAfterDotPos, nextDotPos - justAfterDotPos));
				justAfterDotPos = nextDotPos + 1;

				nextDotPos = lines[whichLine].IndexOf('.', justAfterDotPos);
				provinceSanitation[level] = Convert.ToInt32(lines[whichLine].Substring(justAfterDotPos, nextDotPos - justAfterDotPos));
				justAfterDotPos = nextDotPos + 1;

				nextDotPos = lines[whichLine].IndexOf('.', justAfterDotPos);
				food[level] = Convert.ToInt32(lines[whichLine].Substring(justAfterDotPos, nextDotPos - justAfterDotPos));
				justAfterDotPos = nextDotPos + 1;

				nextDotPos = lines[whichLine].IndexOf('.', justAfterDotPos);
				order[level] = Convert.ToInt32(lines[whichLine].Substring(justAfterDotPos, nextDotPos - justAfterDotPos));
			}
			//

			// Jeśli natrafiono na opis bonusu.
			else
			{
				wealthBonuses[level].Add(new WealthBonus(lines[whichLine]));
			}
			//
		}
	}
}