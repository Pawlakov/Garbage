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
		for (int whichRegion = 0; whichRegion < 3; whichRegion++)
		{
			multipliers[whichRegion] = new decimal[AttilaSimulator.constWealthTypesNumber];
			numbers[whichRegion] = new decimal[AttilaSimulator.constWealthTypesNumber];
			bonuses[whichRegion] = new List<WealthBonus>();

			for (int whichCategory = 0; whichCategory < AttilaSimulator.constWealthTypesNumber; whichCategory++)
			{
				multipliers[whichRegion][whichCategory] = 0;
				numbers[whichRegion][whichCategory] = 0;
			}
		}
		//
	}

	// Zwraca przeliczony dochód dla danego rwgionu.
	public decimal GetWealth(int whichRegion)
	{
		decimal result = 0;

		for (int whichCategory = 0; whichCategory < AttilaSimulator.constWealthTypesNumber; whichCategory++)
		{
			result += ((1 + multipliers[whichRegion][whichCategory]) * numbers[whichRegion][whichCategory]);
		}

		return result;
	}

	// Dodaj bonus pochodzący z konkretnego regionu do listy.
	public void AddBonus(WealthBonus bonus, int whichRegion)
	{
		bonuses[whichRegion].Add(bonus);
	}

	//Wykonuje bonusy z listy zapełniając przy tym multipliers i numbers.
	public void ExecuteAllBonuses()
	{
		for (int whichRegion = 0; whichRegion < 3; whichRegion++)
		{
			for (int whichBonus = 0; whichBonus < bonuses[whichRegion].Count; whichBonus++)
			{
				ExecuteBonus(whichRegion, whichBonus);
			}
		}
	}

	// Pomocnicza dla powyższej.
	private void ExecuteBonus(int whichRegion, int whichBonus)
	{
		if (!bonuses[whichRegion][whichBonus].affectsWholeProvince)
		{
			if (bonuses[whichRegion][whichBonus].isMultiplier)
			{
				//switch (bonuses[whichRegion][j].category)
				//{
				//	case BonusCategory.AGRICULTURE:
				//		multipliers[whichRegion][0] += bonuses[whichRegion][j].number;
				//		break;
				//	case BonusCategory.CULTURE:
				//		multipliers[whichRegion][1] += bonuses[whichRegion][j].number;
				//		break;
				//	case BonusCategory.INDUSTRY:
				//		multipliers[whichRegion][2] += bonuses[whichRegion][j].number;
				//		break;
				//	case BonusCategory.LIVESTOCK:
				//		multipliers[whichRegion][3] += bonuses[whichRegion][j].number;
				//		break;
				//	and do on, and so on, and so on...
				//}
				multipliers[whichRegion][(int)bonuses[whichRegion][whichBonus].category] += bonuses[whichRegion][whichBonus].number;
			}
			else
			{
				numbers[whichRegion][(int)bonuses[whichRegion][whichBonus].category] += bonuses[whichRegion][whichBonus].number;
			}
		}
		else
		{
			for (int whichRegionOther = 0; whichRegionOther < 3; whichRegionOther++)
			{
				if (bonuses[whichRegion][whichBonus].isMultiplier)
				{
					multipliers[whichRegionOther][(int)bonuses[whichRegion][whichBonus].category] += bonuses[whichRegion][whichBonus].number;
				}
				else
				{
					numbers[whichRegionOther][(int)bonuses[whichRegion][whichBonus].category] += bonuses[whichRegion][whichBonus].number;
				}
			}
		}
	}
}