using System;
using System.Xml;
namespace TWAssistant
{
	namespace Rome2
	{
		class WealthBonus
		{
			private BonusCategory category;
			private double value;
			private bool isMultiplier;
			//
			public WealthBonus(XmlNode wealthBonusNode)
			{
				value = Convert.ToDouble(wealthBonusNode.InnerText);
				isMultiplier = Convert.ToBoolean(wealthBonusNode.Attributes.GetNamedItem("is_multiplier").InnerText);
				Enum.TryParse(wealthBonusNode.Attributes.GetNamedItem("category").InnerText, out category);
			}
			public WealthBonus(XmlNode wealthBonusNode, BonusCategory iniCategory)
			{
				value = Convert.ToDouble(wealthBonusNode.InnerText);
				isMultiplier = Convert.ToBoolean(wealthBonusNode.Attributes.GetNamedItem("is_multiplier").InnerText);
				category = iniCategory;
			}
			//
			public void Execute(ref double[] values, ref double[] multipliers)
			{
				if (isMultiplier)
				{
					if (category != BonusCategory.ALL)
						multipliers[(int)category] += value;
					else
					{
						for (byte whichCategory = 0; whichCategory < R2_Simulator.BonusCategoriesCount; whichCategory++)
						{
							multipliers[whichCategory] += value;
						}
					}
				}
				else
				{
					if (category != BonusCategory.ALL)
						values[(int)category] += value;
					else
						throw new Exception("You cannot have value bonus in ALL category.");
				}
			}
		}
	}
}