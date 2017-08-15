using System;
using System.Xml;
/// <summary>
/// Struct containing information about bonuses from single building level.
/// </summary>
struct BuildingLevel
{
	private ushort food;
	private ushort order;
	private WealthBonus[] wealthBonuses;
	//
	/// <summary>
	/// Creates instance of BuildingLevel struct.
	/// </summary>
	/// <param name="levelNode">
	/// XMLNode ceontaining required information.
	/// </param>
	public BuildingLevel(XmlNode levelNode)
	{
		XmlNodeList bonusNodeList = levelNode.ChildNodes;
		food = Convert.ToUInt16(levelNode.Attributes.GetNamedItem("food").InnerText);
		order = Convert.ToUInt16(levelNode.Attributes.GetNamedItem("order").InnerText);
		wealthBonuses = new WealthBonus[bonusNodeList.Count];
		for (byte whichBonus = 0; whichBonus < wealthBonuses.Length; whichBonus++)
		{
			if (bonusNodeList.Item(whichBonus).Attributes.GetNamedItem("category").InnerText != "ALL")
				wealthBonuses[whichBonus] = new WealthBonus(bonusNodeList.Item(whichBonus));
			else
			{
				wealthBonuses[whichBonus] = new WealthBonus(bonusNodeList.Item(whichBonus), BonusCategory.AGRICULTURE);
				wealthBonuses[whichBonus] = new WealthBonus(bonusNodeList.Item(whichBonus), BonusCategory.CULTURE);
				wealthBonuses[whichBonus] = new WealthBonus(bonusNodeList.Item(whichBonus), BonusCategory.INDUSTRY);
				wealthBonuses[whichBonus] = new WealthBonus(bonusNodeList.Item(whichBonus), BonusCategory.LOCAL_COMMERCE);
				wealthBonuses[whichBonus] = new WealthBonus(bonusNodeList.Item(whichBonus), BonusCategory.MARITIME_COMMERCE);
				wealthBonuses[whichBonus] = new WealthBonus(bonusNodeList.Item(whichBonus), BonusCategory.SUBSISTENCE);
			}
		}
	}
}