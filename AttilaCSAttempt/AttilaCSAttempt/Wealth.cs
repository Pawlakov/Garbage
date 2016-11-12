//// obiekt przechowujący, obliczający,... dochód regionu
//class Wealth
//{
//	float[] multipliers;
//	float[] numbers;
//	List<WealthBonus> bonuses;

//	public Wealth()
//	{
//		multipliers = new float[AttilaSimulator.constWealthTypesNumber];
//		numbers = new float[AttilaSimulator.constWealthTypesNumber];

//		for (int i = 0; i < AttilaSimulator.constWealthTypesNumber; i++)
//		{
//			multipliers[i] = 0;
//			numbers[i] = 0;
//		}

//		bonuses = new List<WealthBonus>();
//	}

//	float getWealth()
//	{
//		float result = 0;

//		for (int i = 0; i < AttilaSimulator.constWealthTypesNumber; i++)
//		{
//			result += (multipliers[i] * numbers[i]);
//		}

//		return result;
//	}

//	void addBonus(WealthBonus bonus)
//	{
//		bonuses.Add(bonus);
//	}

//	void executeAllBonuses()
//	{
//		for (int i = 0; i < bonuses.Count; i++)
//		{
//			executeBonus(i);
//		}
//	}

//	void executeBonus(int i)
//	{
//		if (bonuses[i].isMultiplier)
//		{
//			switch (bonuses[1].category)
//			{
//				case BonusCategory.ALL:
//					multipliers[0] += bonuses[i].number;
//					multipliers[1] += bonuses[i].number;
//					break;
//				case BonusCategory.EXAMPLE0:
//					multipliers[0] += bonuses[i].number;
//					break;
//				case BonusCategory.EXAMPLE1:
//					multipliers[1] += bonuses[i].number;
//					break;
//			}
//		}
//		else
//		{
//			switch (bonuses[i].category)
//			{
//				case BonusCategory.EXAMPLE0:
//					numbers[0] += bonuses[i].number;
//					break;
//				case BonusCategory.EXAMPLE1:
//					numbers[1] += bonuses[i].number;
//					break;

//			}
//		}
//	}
//}