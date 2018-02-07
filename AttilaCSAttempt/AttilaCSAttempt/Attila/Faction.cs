using System;
using System.Xml;
namespace TWAssistant
{
	namespace Attila
	{
		public class Faction
		{
			readonly string name;
			readonly int sanitation;
			readonly int fertility;
			readonly int growth;
			readonly WealthBonus[] wealthBonuses;
			readonly BuildingLibrary buildings;
			//
			public Faction(XmlNode factionNode)
			{
				XmlNode temporary = factionNode.Attributes.GetNamedItem("n");
				name = temporary.InnerText;
				sanitation = 0;
				temporary = factionNode.Attributes.GetNamedItem("s");
				if (temporary != null)
					sanitation = Convert.ToInt32(temporary.InnerText);
				fertility = 0;
				temporary = factionNode.Attributes.GetNamedItem("i");
				if (temporary != null)
					fertility = Convert.ToInt32(temporary.InnerText);
				temporary = factionNode.Attributes.GetNamedItem("g");
				if (temporary != null)
					growth = Convert.ToInt32(temporary.InnerText);
				//
				XmlNodeList bonusNodesList = factionNode.ChildNodes;
				wealthBonuses = new WealthBonus[bonusNodesList.Count];
				for (int whichBonus = 0; whichBonus < wealthBonuses.Length; ++whichBonus)
					wealthBonuses[whichBonus] = new WealthBonus(bonusNodesList[whichBonus]);
				//
				buildings = new BuildingLibrary(factionNode.Attributes.GetNamedItem("b").InnerText);
			}
			//
			public string Name
			{
				get { return name; }
			}
			public int Sanitation
			{
				get { return sanitation; }
			}
			public int Fertility
			{
				get { return fertility; }
			}
			public int Growth
			{
				get { return growth; }
			}
			public WealthBonus[] WealthBonuses
			{
				get { return wealthBonuses; }
			}
			public BuildingLibrary Buildings
			{
				get { return buildings; }
			}
			//
			public void EvaluateBuildings()
			{
				buildings.EvaluateBuildings();
			}
		}
	}
}