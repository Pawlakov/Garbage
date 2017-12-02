using System;
using System.Xml;
namespace TWAssistant
{
	namespace Attila
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
				XmlNodeList levelNodeList = branchNode.ChildNodes;
				//
				name = branchNode.Attributes.GetNamedItem("n").InnerText;
				//
				usefuliness = 0;
				//
				Enum.TryParse(branchNode.Attributes.GetNamedItem("t").InnerText, out type);
				//
				XmlNode temporary = branchNode.Attributes.GetNamedItem("r");
				if (type == BuildingType.RESOURCE)
					Enum.TryParse(temporary.InnerText, out resource);
				else
					resource = Resource.NONE;
				if (type == BuildingType.RESOURCE && resource == Resource.NONE)
					Console.WriteLine("ERROR: Resource building with NONE resource({0})!", name);
				//
				levels = new BuildingLevel[levelNodeList.Count];
				for (int whichLevel = 0; whichLevel < levels.Length; ++whichLevel)
				{
					levels[whichLevel] = new BuildingLevel(levelNodeList.Item(whichLevel));
				}
			}
			//
			public string Name
			{
				get { return name; }
			}
			public uint Usefuliness
			{
				get { return usefuliness; }
				set { usefuliness = value; }
			}
			public BuildingType Type
			{
				get { return type; }
			}
			public Resource Resource
			{
				get { return resource; }
			}
			public int NumberOfLevels
			{
				get { return levels.Length; }
			}
			public BuildingLevel this[uint whichLevel]
			{
				get { return levels[whichLevel]; }
			}
			//
			public class BuildingLevel
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
					food = 0;
					foodPerFertility = 0;
					order = 0;
					regionalSanitation = 0;
					provincionalSanitation = 0;
					religiousInfluence = 0;
					XmlNode temporary;
					//
					if (levelNode.Attributes != null)
					{
						temporary = levelNode.Attributes.GetNamedItem("f");
						if (temporary != null)
							food = Convert.ToInt32(temporary.InnerText);
						//
						temporary = levelNode.Attributes.GetNamedItem("_f");
						if (temporary != null)
							foodPerFertility = Convert.ToInt32(temporary.InnerText);
						//
						temporary = levelNode.Attributes.GetNamedItem("o");
						if (temporary != null)
							order = Convert.ToInt32(temporary.InnerText);
						//
						temporary = levelNode.Attributes.GetNamedItem("s");
						if (temporary != null)
							regionalSanitation = Convert.ToInt32(temporary.InnerText);
						//
						temporary = levelNode.Attributes.GetNamedItem("_s");
						if (temporary != null)
							provincionalSanitation = Convert.ToInt32(temporary.InnerText);
						//
						temporary = levelNode.Attributes.GetNamedItem("r");
						if (temporary != null)
							religiousInfluence = Convert.ToInt32(temporary.InnerText);
						//
					}
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