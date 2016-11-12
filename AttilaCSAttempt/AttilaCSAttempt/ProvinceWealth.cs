using System;
using System.Collections.Generic;

// obiekt przechowujący, obliczający,... dochód regionu
class ProvinceWealth
{
	private decimal[][] multipliers;
	private decimal[][] numbers;
	private List<WealthBonus>[] bonuses;

	public ProvinceWealth()
	{
		// Mnożniki i liczby [dla każdego regionu][dla każdego typu przychodu].
		multipliers = new decimal[3][];
		numbers = new decimal[3][];
		//

		// Lista bonusów do wykonania.
		bonuses = new List<WealthBonus>[3];
		//

		// A tu się dzieją powalone rzeczy.
		for (int i = 0; i < 3; i++)
		{
			multipliers[i] = new decimal[AttilaSimulator.constWealthTypesNumber];
			numbers[i] = new decimal[AttilaSimulator.constWealthTypesNumber];
			bonuses[i] = new List<WealthBonus>();

			for (int j = 0; j < AttilaSimulator.constWealthTypesNumber; j++)
			{
				multipliers[i][j] = 0;
				numbers[i][j] = 0;
			}
		}
		//
	}

	// Zwraca przeliczony dochód dla danego rwgionu.
	public decimal GetWealth(int whichRegion)
	{
		ExecuteAllBonuses();

		decimal result = 0;

		for (int i = 0; i < AttilaSimulator.constWealthTypesNumber; i++)
		{
			result += ((1 + multipliers[whichRegion][i]) * numbers[whichRegion][i]);
		}

		return result;
	}

	// Dodaj bonus pochodzący z konkretnego regionu do listy.
	public void AddBonus(WealthBonus bonus, int whichRegion)
	{
		bonuses[whichRegion].Add(bonus);
	}

	//Wykonuje bonusy z listy zapełniając przy tym multipliers i numbers.
	private void ExecuteAllBonuses()
	{
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < bonuses[i].Count; j++)
			{
				ExecuteBonus(i, j);
			}
		}
	}

	// Pomocnicza dla powyższej.
	private void ExecuteBonus(int i, int j)
	{
		if (!bonuses[i][j].affectsWholeProvince)
		{
			if (bonuses[i][j].isMultiplier)
			{
				switch (bonuses[i][j].category)
				{
					case BonusCategory.EXAMPLE0:
						multipliers[i][0] += bonuses[i][j].number;
						break;
					case BonusCategory.EXAMPLE1:
						multipliers[i][1] += bonuses[i][j].number;
						break;
				}
			}
			else
			{
				switch (bonuses[i][j].category)
				{
					case BonusCategory.EXAMPLE0:
						numbers[i][0] += bonuses[i][j].number;
						break;
					case BonusCategory.EXAMPLE1:
						numbers[i][1] += bonuses[i][j].number;
						break;

				}
			}
		}
		else
		{
			for(int k = 0; k < 3; k++)
			{
				if (bonuses[i][j].isMultiplier)
				{
					switch (bonuses[i][j].category)
					{
						case BonusCategory.EXAMPLE0:
							multipliers[k][0] += bonuses[i][j].number;
							break;
						case BonusCategory.EXAMPLE1:
							multipliers[k][1] += bonuses[i][j].number;
							break;
					}
				}
				else
				{
					switch (bonuses[i][j].category)
					{
						case BonusCategory.EXAMPLE0:
							numbers[k][0] += bonuses[i][j].number;
							break;
						case BonusCategory.EXAMPLE1:
							numbers[k][1] += bonuses[i][j].number;
							break;

					}
				}
			}
		}
	}
}