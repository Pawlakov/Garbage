using System.Xml;
struct ProvinceData
{
	private string name;
	private RegionData[] regions;
	//
	/// <summary>
	/// Creates new instance of ProvinceData.
	/// </summary>
	/// <param name="provinceNode">
	/// XMLNode caontaining all required informations.
	/// </param>
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
	/// <summary>
	/// Name of this province.
	/// </summary>
	public string Name
	{
		get
		{
			return name;
		}
	}
	/// <summary>
	/// Number of regions in this province.
	/// </summary>
	public int NumberOfRegions
	{
		get
		{
			return regions.Length;
		}
	}
	/// <summary>
	/// Provides easy access to province's regions.
	/// </summary>
	/// <param name="whichRegion">
	/// Zero-based index.
	/// </param>
	/// <returns></returns>
	public RegionData this[byte whichRegion]
	{
		get
		{
			return regions[whichRegion];
		}
	}
}