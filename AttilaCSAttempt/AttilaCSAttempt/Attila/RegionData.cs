using System;
using System.Xml;
namespace TWAssistant
{
	namespace Attila
	{
		class RegionData
		{
			private string name;
			private Resource resource;
			private bool isCoastal;
			private bool isBig;
			//
			public RegionData(XmlNode regionNode, bool iniIsBig)
			{
				isBig = iniIsBig;
				name = regionNode.Attributes.GetNamedItem("n").InnerText;
				isCoastal = Convert.ToBoolean(regionNode.Attributes.GetNamedItem("c").InnerText);
				Enum.TryParse(regionNode.Attributes.GetNamedItem("r").InnerText, out resource);
			}
			//
			public string Name
			{
				get { return name; }
			}
			public Resource Resource
			{
				get { return resource; }
			}
			public bool IsCoastal
			{
				get { return isCoastal; }
			}
			public bool IsBig
			{
				get { return isBig; }
			}
			public uint SlotsCount
			{
				get
				{
					if (isBig)
					{
						if (isCoastal)
							return 6;
						else
							return 5;
					}
					else
					{
						if (isCoastal)
							return 4;
						else
							return 3;
					}
				}
			}
		}
	}
}