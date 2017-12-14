using System;
using System.Xml;
namespace TWAssistant
{
	namespace Attila
	{
		class WealthBonus
		{
			BonusCategory category;
			bool isMultiplier;
			float value;
			float perFertilityValue;
			//
			public WealthBonus(XmlNode wealthBonusNode)
			{
				Enum.TryParse(wealthBonusNode.Attributes.GetNamedItem("c").InnerText, out category);
				//
				Setup(wealthBonusNode);
			}
			public WealthBonus(XmlNode wealthBonusNode, BonusCategory iniCategory)
			{
				category = iniCategory;
				//
				Setup(wealthBonusNode);
			}
			public WealthBonus(BonusCategory iniCategory, bool iniIsMultiplier, float iniValue, float iniPerFertilityValue)
			{
				category = iniCategory;
				isMultiplier = iniIsMultiplier;
				value = iniValue;
				perFertilityValue = iniPerFertilityValue;
			}
			//
			public void Execute(ref float[] values, ref float[] multipliers, uint fertility)
			{
				if (isMultiplier)
				{
					if (category != BonusCategory.ALL)
						multipliers[(int)category] += value;
					else
					{
						for (uint whichCategory = 0; whichCategory < Simulator.BonusCategoriesCount - 1; ++whichCategory)
						{
							multipliers[whichCategory] += value;
						}
					}
				}
				else
				{
					values[(int)category] += (value + (perFertilityValue * fertility));
				}
			}
			//
			void Setup(XmlNode wealthBonusNode)
			{
				isMultiplier = Convert.ToBoolean(wealthBonusNode.Attributes.GetNamedItem("m").InnerText);
				if (category == BonusCategory.ALL && isMultiplier == false)
					Console.WriteLine("ERROR: Non-percentage ALL bonus!");
				//
				string innerText = wealthBonusNode.InnerText;
				if (innerText.Contains("|"))
				{
					if ((category != BonusCategory.AGRICULTURE && category != BonusCategory.HUSBANDRY) || isMultiplier == true)
						Console.WriteLine("ERROR: Wrong fertility-based bonus!");
					int divisionPosition = innerText.IndexOf('|');
					string firstValue = innerText.Substring(0, divisionPosition);
					string secondValue = innerText.Substring(divisionPosition + 1);
					value = Convert.ToSingle(firstValue);
					perFertilityValue = Convert.ToSingle(secondValue);
				}
				else
				{
					value = Convert.ToSingle(innerText);
					perFertilityValue = 0.0f;
				}
			}
		}
	}
}