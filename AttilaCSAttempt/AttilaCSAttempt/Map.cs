using System;
using System.Collections.Generic;
using System.IO;

class Map
{
	private List<ProvinceData> provinces;

	public ProvinceData this[int whichProvince]
	{
		get
		{
			return provinces[whichProvince];
		}
	}

	public Map()
	{
		List<string> lines = new List<string>();

		StreamReader reader = new StreamReader("map.txt");
		string line = reader.ReadLine();

		while (line != null)
		{
			lines.Add(line);
			line = reader.ReadLine();
		}
		reader.Close();

		for (int whichLine = 0; whichLine < lines.Count; whichLine++)
		{
			provinces.Add(new ProvinceData(lines[whichLine]));
		}
	}

	public void PrintList()
	{
		for(int whichProvince = 0; whichProvince < provinces.Count; whichProvince++)
		{
			Console.WriteLine("{0}. {1}", whichProvince, provinces[whichProvince].provinceName);
		}
	}
}
