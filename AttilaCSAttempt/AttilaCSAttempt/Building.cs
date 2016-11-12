using System;
using System.Collections.Generic;

// zawiera gałąź budynku z danymi dla każdego poziomu
class Building
{
	string name;
	// Sanitacja którą daje tylko regionowi w którym się znajduje.
	public int[] regionSanitation;
	//Sanitacja którą daje każdemu regionowi w prowincji łącznie z tym w którym się znajduje.
	public int[] provinceSanitation;
	public int[] food;
	public int[] order;
	public List<WealthBonus>[] wealthBonuses;

	public Building(string[] lines)
	{
		// Inicjalizacja.
		regionSanitation = new int[4];
		provinceSanitation = new int[4];
		food = new int[4];
		order = new int[4];
		wealthBonuses = new List<WealthBonus>[4];

		for(int i = 0; i < 4; i++)
		{
			regionSanitation[i] = 0;
			provinceSanitation[i] = 0;
			food[i] = 0;
			order[i] = 0;
			wealthBonuses[i] = new List<WealthBonus>();
		}
		//

		name = lines[0].Substring(0, lines[0].IndexOf('.'));

		int level = -1;
		for(int i = 1; i < lines.Length; i++)
		{
			// Jeśli natrafiono na nagłówek poziomu.
			if (lines[i][0] == '(')
			{
				level++;
				int justAfterDotPos = 1;
				int nextDotPos = lines[i].IndexOf('.', justAfterDotPos);
				regionSanitation[level] = Convert.ToInt32(lines[i].Substring(justAfterDotPos, nextDotPos - justAfterDotPos));
				justAfterDotPos = nextDotPos + 1;

				nextDotPos = lines[i].IndexOf('.', justAfterDotPos);
				provinceSanitation[level] = Convert.ToInt32(lines[i].Substring(justAfterDotPos, nextDotPos - justAfterDotPos));
				justAfterDotPos = nextDotPos + 1;

				nextDotPos = lines[i].IndexOf('.', justAfterDotPos);
				food[level] = Convert.ToInt32(lines[i].Substring(justAfterDotPos, nextDotPos - justAfterDotPos));
				justAfterDotPos = nextDotPos + 1;

				nextDotPos = lines[i].IndexOf('.', justAfterDotPos);
				order[level] = Convert.ToInt32(lines[i].Substring(justAfterDotPos, nextDotPos - justAfterDotPos));
			}
			//

			// Jeśli natrafiono na opis bonusu.
			else
			{
				wealthBonuses[level].Add(new WealthBonus(lines[i]));
			}
			//
		}
	}
}