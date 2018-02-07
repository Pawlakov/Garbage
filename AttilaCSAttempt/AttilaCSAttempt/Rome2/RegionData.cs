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
				resource = (Resource)Enum.Parse(typeof(Resource), regionNode.Attributes.GetNamedItem("resource").InnerText);
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
						return 5;
					}
					if (isCoastal)
						return 4;
					return 3;
				}
			}
		}
	}
}