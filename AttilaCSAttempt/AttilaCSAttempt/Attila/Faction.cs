using System;
using System.Xml;
namespace TWAssistant
{
	namespace Attila
	{
		class Faction
		{
			private string name;
			private BuildingLibrary buildings;
			private int food;
			private int order;
			private int sanitation;
			private int religiousInfluence;
			private WealthBonus[] wealthBonuses;
			//
			public Faction(XmlNode factionNode)
			{
				food = 0;
				order = 0;
				sanitation = 0;
				religiousInfluence = 0;
				//
				name = factionNode.Attributes.GetNamedItem("n").InnerText;
				//
				buildings = new BuildingLibrary(factionNode.Attributes.GetNamedItem("b").InnerText);
				//
				if (factionNode.Attributes != null)
				{
					XmlNode temporary;
					temporary = factionNode.Attributes.GetNamedItem("f");
					if (temporary != null)
						food = Convert.ToInt32(temporary.InnerText);
					//
					temporary = factionNode.Attributes.GetNamedItem("o");
					if (temporary != null)
						order = Convert.ToInt32(temporary.InnerText);
					//
					temporary = factionNode.Attributes.GetNamedItem("s");
					if (temporary != null)
						sanitation = Convert.ToInt32(temporary.InnerText);
					//
					temporary = factionNode.Attributes.GetNamedItem("r");
					if (temporary != null)
						religiousInfluence = Convert.ToInt32(temporary.InnerText);
				}
				XmlNodeList bonusNodesList = factionNode.ChildNodes;
				wealthBonuses = new WealthBonus[bonusNodesList.Count];
				for (int whichBonus = 0; whichBonus < wealthBonuses.Length; ++whichBonus)
				{
					wealthBonuses[whichBonus] = new WealthBonus(bonusNodesList[whichBonus]);
				}
			}
			//
			public string Name
			{
				get { return name; }
			}
			public BuildingLibrary Buildings
			{
				get { return buildings; }
			}
			public int Food
			{
				get { return food; }
			}
			public int Order
			{
				get { return order; }
			}
			public int Sanitation
			{
				get { return sanitation; }
			}
			public int ReligiousInfluence
			{
				get { return religiousInfluence; }
			}
			public WealthBonus[] WealthBonuses
			{
				get { return wealthBonuses; }
			}
			//
			public void CurbUselessBuildings()
			{
				buildings.RemoveUselessAndResetUsefuliness();
			}
		}
	}
}