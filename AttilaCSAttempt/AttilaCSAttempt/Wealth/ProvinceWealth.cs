using System.Collections.Generic;
class ProvinceWealth
{
	private double[] multipliers;
	private double[] values;
	private List<WealthBonus> bonuses;
	//
	private void ResetArrays()
	{
		for (byte whichCategory = 0; whichCategory < AttilaSimulator.constBonusCategoriesNumber; whichCategory++)
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
	//
	/// <summary>
	/// Creates new instance of ProvinceWealth class.
	/// </summary>
	public ProvinceWealth()
	{
		multipliers = new double[AttilaSimulator.constBonusCategoriesNumber];
		values = new double[AttilaSimulator.constBonusCategoriesNumber];
		bonuses = new List<WealthBonus>();
	}
	/// <summary>
	/// Calculated total province's wealth using stored bonuses.
	/// </summary>
	public double Wealth
	{
		get
		{
			double result = 0;
			ResetArrays();
			ExecuteBonuses();
			for (byte whichCategory = 0; whichCategory < AttilaSimulator.constBonusCategoriesNumber; whichCategory++)
			{
				result += (multipliers[whichCategory] * values[whichCategory]);
			}
			return result;
		}
	}
	/// <summary>
	/// Adds bonus, which can later be executed.
	/// </summary>
	/// <param name="bonus">
	/// Added new bonus.
	/// </param>
	public void AddBonus(WealthBonus bonus)
	{
		bonuses.Add(bonus);
	}
	/// <summary>
	/// Removes all bonuses.
	/// </summary>
	public void ClearBonuses()
	{
		bonuses.Clear();
	}
}