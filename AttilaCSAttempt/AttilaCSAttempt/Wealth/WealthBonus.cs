﻿using System;
using System.Xml;
/// <summary>
/// Contains single income bonus (either multiplier, or value).
/// </summary>
struct WealthBonus
{
	private BonusCategory category;
	private double value;
	private bool isMultiplier;
	//
	/// <summary>
	/// Creates new instance of WealthBonus struct.
	/// </summary>
	/// <param name="wealthBonusNode">
	/// XMLNode containing all required inforamtion.
	/// </param>
	public WealthBonus(XmlNode wealthBonusNode)
	{
		value = Convert.ToDouble(wealthBonusNode.InnerText);
		isMultiplier = Convert.ToBoolean(wealthBonusNode.Attributes.GetNamedItem("is_multiplier").InnerText);
		Enum.TryParse(wealthBonusNode.Attributes.GetNamedItem("category").InnerText, out category);
	}
	/// <summary>
	/// Creates new instance of WealthBonus struct.
	/// </summary>
	/// <param name="wealthBonusNode">
	/// XMLNode containing all required inforamtion (except for category).
	/// </param>
	/// <param name="iniCategory">
	/// Bonus' income category.
	/// </param>
	public WealthBonus(XmlNode wealthBonusNode, BonusCategory iniCategory)
	{
		value = Convert.ToDouble(wealthBonusNode.InnerText);
		isMultiplier = Convert.ToBoolean(wealthBonusNode.Attributes.GetNamedItem("is_multiplier").InnerText);
		category = iniCategory;
	}
	/// <summary>
	/// Applies this bonus on either values array, or multipliers array.
	/// </summary>
	public void Execute(ref double[] values, ref double[] multipliers)
	{
		if (isMultiplier)
			multipliers[(int)category] += value;
		else
			values[(int)category] += value;
	}
}