using System;
using System.Xml;
namespace TWAssistant
{
	namespace Rome2
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
				name = regionNode.Attributes.GetNamedItem("name").InnerText;
				isCoastal = Convert.ToBoolean(regionNode.Attributes.GetNamedItem("is_coastal").InnerText);
				Enum.TryParse(regionNode.Attributes.GetNamedItem("resource").InnerText, out resource);
			}
			//
			public string Name
			{
				get
				{
					return name;
				}
			}
			public Resource Resource
			{
				get
				{
					return resource;
				}
			}
			public bool IsCoastal
			{
				get { return isCoastal; }
			}
			public bool IsBig
			{
				get { return isBig; }
			}
			public byte NumberOfSlots
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