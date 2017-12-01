using System;
using System.Xml;
namespace TWAssistant
{
	namespace Rome2
	{
		class BuildingBranch
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
			class BuildingLevel
			{
				private short food;
				private short order;
				private WealthBonus[] wealthBonuses;
				//
				public BuildingLevel(XmlNode levelNode)
				{
					XmlNodeList bonusNodeList = levelNode.ChildNodes;
					food = Convert.ToInt16(levelNode.Attributes.GetNamedItem("food").InnerText);
					order = Convert.ToInt16(levelNode.Attributes.GetNamedItem("order").InnerText);
					wealthBonuses = new WealthBonus[bonusNodeList.Count];
					for (byte whichBonus = 0; whichBonus < wealthBonuses.Length; whichBonus++)
					{
						wealthBonuses[whichBonus] = new WealthBonus(bonusNodeList.Item(whichBonus));
					}
				}
				//
				public short Food
				{
					get { return food; }
				}
				public short Order
				{
					get { return order; }
				}
				public WealthBonus[] WealthBonuses
				{
					get { return wealthBonuses; }
				}
			}
		}
	}
}