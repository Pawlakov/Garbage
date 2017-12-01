using System.Xml;
namespace TWAssistant
{
	namespace Rome2
	{
		class ProvinceData
		{
			private string name;
			private RegionData[] regions;
			//
			public ProvinceData(XmlNode provinceNode)
			{
				XmlNodeList regionNodeList = provinceNode.ChildNodes;
				regions = new RegionData[regionNodeList.Count];
				name = provinceNode.Attributes.GetNamedItem("name").InnerText;
				regions[0] = new RegionData(regionNodeList.Item(0), true);
				for (byte whichRegion = 1; whichRegion < regions.Length; whichRegion++)
				{
					regions[whichRegion] = new RegionData(regionNodeList.Item(whichRegion), false);
				}
			}
			//
			public string Name
			{
				get
				{
					return name;
				}
			}
			public int NumberOfRegions
			{
				get
				{
					return regions.Length;
				}
			}
			public RegionData this[byte whichRegion]
			{
				get
				{
					return regions[whichRegion];
				}
			}
		}
	}
}