﻿using System;
using System.Xml;
namespace TWAssistant
{
	namespace Attila
	{
		public class BuildingLevel
		{
			readonly public string name;
			readonly public int level;
			readonly public bool? isLegacy;
			//
			readonly int food;
			readonly public int irigation;
			readonly int foodPerFertility;
			//
			readonly public int order;
			readonly public WealthBonus[] wealthBonuses;
			//
			readonly public int regionalSanitation;
			readonly public int provincionalSanitation;
			//
			readonly public int religiousInfluence;
			readonly public int religiousOsmosis;
			//
			int usefuliness;
			bool isVoid;
			//
			public BuildingLevel(XmlNode levelNode)
			{
				isLegacy = null;
				//
				food = 0;
				irigation = 0;
				foodPerFertility = 0;
				//
				order = 0;
				//
				regionalSanitation = 0;
				provincionalSanitation = 0;
				//
				religiousInfluence = 0;
				religiousOsmosis = 0;
				//
				usefuliness = 0;
				isVoid = false;
				//
				XmlNode temporary = levelNode.Attributes.GetNamedItem("n");
				name = temporary.InnerText;
				temporary = levelNode.Attributes.GetNamedItem("l");
				level = Convert.ToInt32(temporary.InnerText);
				temporary = levelNode.Attributes.GetNamedItem("lcy");
				if (temporary != null)
					isLegacy = Convert.ToBoolean(temporary.InnerText);
				//
				temporary = levelNode.Attributes.GetNamedItem("f");
				if (temporary != null)
					food = Convert.ToInt32(temporary.InnerText);
				temporary = levelNode.Attributes.GetNamedItem("i");
				if (temporary != null)
					irigation = Convert.ToInt32(temporary.InnerText);
				temporary = levelNode.Attributes.GetNamedItem("_f");
				if (temporary != null)
					foodPerFertility = Convert.ToInt32(temporary.InnerText);
				//
				temporary = levelNode.Attributes.GetNamedItem("o");
				if (temporary != null)
					order = Convert.ToInt32(temporary.InnerText);
				XmlNodeList bonusNodeList = levelNode.ChildNodes;
				wealthBonuses = new WealthBonus[bonusNodeList.Count];
				for (int whichBonus = 0; whichBonus<wealthBonuses.Length; ++whichBonus)
				{
					wealthBonuses[whichBonus] = new WealthBonus(bonusNodeList.Item(whichBonus));
				}
				//
				temporary = levelNode.Attributes.GetNamedItem("s");
				if (temporary != null)
					regionalSanitation = Convert.ToInt32(temporary.InnerText);
				temporary = levelNode.Attributes.GetNamedItem("_s");
				if (temporary != null)
					provincionalSanitation = Convert.ToInt32(temporary.InnerText);
				//
				temporary = levelNode.Attributes.GetNamedItem("r");
				if (temporary != null)
					religiousInfluence = Convert.ToInt32(temporary.InnerText);
				temporary = levelNode.Attributes.GetNamedItem("ro");
				if (temporary != null)
					religiousOsmosis = Convert.ToInt32(temporary.InnerText);
			}
			//
			public int GetFood(int fertility)
			{
				return food + fertility * foodPerFertility;
			}
			//
			public int Usefuliness
			{
				get { return usefuliness; }
			}
			public bool IsVoid
			{
				get { return isVoid; }
			}
			//
			public void Reward()
			{
				++usefuliness;
			}
			public void Evaluate()
			{
				if (usefuliness == 0)
					isVoid = true;
				else
				{
					usefuliness = 0;
					isVoid = false;
				}
			}
			public void ForceVoid()
			{
				isVoid = true;
			}
		}
	}
}