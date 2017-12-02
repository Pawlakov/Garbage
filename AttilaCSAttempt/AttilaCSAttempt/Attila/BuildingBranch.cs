using System;
using System.Xml;
namespace TWAssistant
{
	namespace Attila
	{
		class BuildingBranch //NOT DONE
		{
			private string name;
			private uint usefuliness;
			private BuildingType type;
			private Resource resource;
			private BuildingLevel[] levels;
			//
			public BuildingBranch(XmlNode branchNode)
			{
				usefuliness = 0;
				XmlNodeList levelNodeList = branchNode.ChildNodes;
				name = branchNode.Attributes.GetNamedItem("name").InnerText;
				Enum.TryParse(branchNode.Attributes.GetNamedItem("type").InnerText, out type);
				Enum.TryParse(branchNode.Attributes.GetNamedItem("resource").InnerText, out resource);
				levels = new BuildingLevel[levelNodeList.Count];
				for (byte whichLevel = 0; whichLevel < levels.Length; whichLevel++)
				{
					levels[whichLevel] = new BuildingLevel(levelNodeList.Item(whichLevel));
				}
			}
			//
			public Resource Resource
			{
				get { return resource; }
			}
			public short GetFood(byte level)
			{
				return levels[level].Food;
			}
			public short GetOrder(byte level)
			{
				return levels[level].Order;
			}
			public WealthBonus[] GetWealthBonuses(byte level)
			{
				return levels[level].WealthBonuses;
			}
			//
			public string Name
			{
				get { return name; }
			}
			public int NumberOfLevels
			{
				get { return levels.Length; }
			}
			public uint Usefuliness
			{
				get { return usefuliness; }
				set { usefuliness = value; }
			}
			//
			class BuildingLevel //DONE
			{
				private int food;
				private int foodPerFertility;
				private int order;
				private int regionalSanitation;
				private int provincionalSanitation;
				private int religiousInfluence;
				private WealthBonus[] wealthBonuses;
				//
				public BuildingLevel(XmlNode levelNode)
				{
					string temporary;
					//
					temporary = levelNode.Attributes.GetNamedItem("f").InnerText;
					if (temporary != null)
						food = Convert.ToInt32(temporary);
					else
						food = 0;
					//
					temporary = levelNode.Attributes.GetNamedItem("_f").InnerText;
					if (temporary != null)
						foodPerFertility = Convert.ToInt32(temporary);
					else
						foodPerFertility = 0;
					//
					temporary = levelNode.Attributes.GetNamedItem("o").InnerText;
					if (temporary != null)
						order = Convert.ToInt32(temporary);
					else
						order = 0;
					//
					temporary = levelNode.Attributes.GetNamedItem("s").InnerText;
					if (temporary != null)
						regionalSanitation = Convert.ToInt32(temporary);
					else
						regionalSanitation = 0;
					//
					temporary = levelNode.Attributes.GetNamedItem("_s").InnerText;
					if (temporary != null)
						provincionalSanitation = Convert.ToInt32(temporary);
					else
						provincionalSanitation = 0;
					//
					temporary = levelNode.Attributes.GetNamedItem("r").InnerText;
					if (temporary != null)
						religiousInfluence = Convert.ToInt32(temporary);
					else
						religiousInfluence = 0;
					//
					XmlNodeList bonusNodeList = levelNode.ChildNodes;
					wealthBonuses = new WealthBonus[bonusNodeList.Count];
					for (byte whichBonus = 0; whichBonus < wealthBonuses.Length; whichBonus++)
					{
						wealthBonuses[whichBonus] = new WealthBonus(bonusNodeList.Item(whichBonus));
					}
				}
				//
				public int GetFood(uint fertility)
				{
					return food + (int)(fertility * foodPerFertility);
				}
				public int Order
				{
					get { return order; }
				}
				public int RegionalSanitation
				{
					get { return regionalSanitation; }
				}
				public int ProvincionalSanitation
				{
					get { return provincionalSanitation; }
				}
				public int ReligiousInfluence
				{
					get { return religiousInfluence; }
				}
				public WealthBonus[] WealthBonuses
				{
					get { return wealthBonuses; }
				}
			}
		}
	}
}