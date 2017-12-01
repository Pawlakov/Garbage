using System.Collections.Generic;
namespace TWAssistant
{
	namespace Rome2
	{
		class ProvinceWealth
		{
			private double[] multipliers;
			private double[] values;
			private List<WealthBonus> bonuses;
			//
			public ProvinceWealth()
			{
				multipliers = new double[R2_Simulator.BonusCategoriesCount];
				values = new double[R2_Simulator.BonusCategoriesCount];
				bonuses = new List<WealthBonus>();
			}
			public double Wealth
			{
				get
				{
					double result = 0;
					ResetArrays();
					ExecuteBonuses();
					for (byte whichCategory = 0; whichCategory < R2_Simulator.BonusCategoriesCount; whichCategory++)
					{
						result += (multipliers[whichCategory] * values[whichCategory]);
					}
					return result;
				}
			}
			//
			public void AddBonus(WealthBonus bonus)
			{
				bonuses.Add(bonus);
			}
			public void ClearBonuses()
			{
				bonuses.Clear();
			}
			//
			private void ResetArrays()
			{
				for (byte whichCategory = 0; whichCategory < R2_Simulator.BonusCategoriesCount; whichCategory++)
				{
					multipliers[whichCategory] = 1;
					values[whichCategory] = 0;
				}
			}
			private void ExecuteBonuses()
			{
				for (byte whichBonus = 0; whichBonus < bonuses.Count; whichBonus++)
				{
					bonuses[whichBonus].Execute(ref values, ref multipliers);
				}
			}
		}
	}
}