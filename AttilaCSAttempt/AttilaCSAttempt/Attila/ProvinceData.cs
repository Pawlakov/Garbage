using System;
using System.Xml;
namespace TWAssistant
{
	namespace Attila
	{
		class ProvinceData
		{
			string name;
			uint fertility; //0-5
			readonly RegionData[] regions;
			//
			public ProvinceData(XmlNode provinceNode)
			{
				name = provinceNode.Attributes.GetNamedItem("n").InnerText;
				//
				fertility = Convert.ToUInt32(provinceNode.Attributes.GetNamedItem("f").InnerText);
				if (fertility > 5)
					Console.WriteLine("ERROR: Fertility should be under 6 (is {0})!", fertility);
				//
				XmlNodeList regionNodeList = provinceNode.ChildNodes;
				if (regionNodeList.Count != 3)
					Console.WriteLine("ERROR: {0} regions in province instead of 3!", regionNodeList.Count);
				regions = new RegionData[3];
				try
				{
				regions[0] = new RegionData(regionNodeList.Item(0), true);
				regions[1] = new RegionData(regionNodeList.Item(1), false);
				regions[2] = new RegionData(regionNodeList.Item(2), false);
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception.Message);
					Console.WriteLine("{0} fell of a bike.", name);
					Console.ReadKey();
				}
			}
			//
			public string Name
			{
				get { return name; }
			}
			public uint Fertility
			{
				get { return fertility; }
			}
			public RegionData this[uint whichRegion]
			{
				get { return regions[whichRegion]; }
			}
		}
	}
}