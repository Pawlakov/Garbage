using System.Collections.Generic;
namespace TWAssistant
{
	namespace Attila
	{
		class ProvinceWealth
		{
			readonly uint fertility;
			float[] multipliers;
			float[] values;
			readonly List<WealthBonus> bonuses;
			bool isCurrent;
			float wealth;
			//
			public ProvinceWealth(uint iniFertility)
			{
				fertility = iniFertility;
				multipliers = new float[Simulator.BonusCategoriesCount];
				values = new float[Simulator.BonusCategoriesCount];
				bonuses = new List<WealthBonus>();
				isCurrent = false;
				wealth = 0;
			}
			public float Wealth
			{
				get
				{
					if (isCurrent)
					{
						return wealth;
					}
					wealth = 0;
					ResetArrays();
					ExecuteBonuses();
					for (uint whichCategory = 0; whichCategory < Simulator.BonusCategoriesCount; ++whichCategory)
					{
						wealth += (multipliers[whichCategory] * values[whichCategory]);
					}
					return wealth;
				}
			}
			//
			public void AddBonus(WealthBonus bonus)
			{
				bonuses.Add(bonus);
				isCurrent = false;
			}
			//
			void ResetArrays()
			{
				for (uint whichCategory = 0; whichCategory < Simulator.BonusCategoriesCount; ++whichCategory)
				{
					multipliers[whichCategory] = 1;
					values[whichCategory] = 0;
				}
				isCurrent = false;
			}
			void ExecuteBonuses()
			{
				for (int whichBonus = 0; whichBonus < bonuses.Count; ++whichBonus)
				{
					bonuses[whichBonus].Execute(ref values, ref multipliers, fertility);
				}
				isCurrent = true;
			}
		}
	}
}