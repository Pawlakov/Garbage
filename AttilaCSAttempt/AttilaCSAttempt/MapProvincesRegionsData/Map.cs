using System;
using System.Xml;
/// <summary>
/// Contains list of all provinces' data.
/// </summary>
struct Map
{
	private ProvinceData[] provinces;
	//
	/// <summary>
	/// Provides easy acces to province data.
	/// </summary>
	/// <param name="whichProvince">
	/// Zero based index of province.
	/// </param>
	/// <returns>
	/// Returns full set of province's specification.
	/// </returns>
	public ProvinceData this[uint whichProvince]
	{
		get
		{
			return provinces[whichProvince];
		}
	}
	/// <summary>
	/// Creates container for all provinces.
	/// </summary>
	/// <param name="filename">Name of XML file.</param>
	public Map(string filename)
	{
		XmlDocument sourceDocument = new XmlDocument();
		sourceDocument.Load("map.xml");
		XmlNodeList provinceNodeList = sourceDocument.GetElementsByTagName("province");
		provinces = new ProvinceData[provinceNodeList.Count];
		for (int whichProvince = 0; whichProvince < provinces.Length; whichProvince++)
		{
			provinces[whichProvince] = new ProvinceData(provinceNodeList.Item(whichProvince));
		}
	}
	/// <summary>
	/// Shows list of all provinces with their indexes.
	/// </summary>
	public void ShowList()
	{
		for(int whichProvince = 0; whichProvince < provinces.Length; whichProvince++)
		{
			Console.WriteLine("{0}. {1}", whichProvince, provinces[whichProvince].Name);
		}
	}
}
