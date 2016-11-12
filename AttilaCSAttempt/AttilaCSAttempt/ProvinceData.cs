using System;

class ProvinceData
{
	public string[] regionNames;
	public Resource[] resources;
	public bool[] isCoastal;
	public bool[] isBig;

	public string provinceName;

	public ProvinceData(string dataLine)
	{
		regionNames = new string[3];
		resources = new Resource[3];
		isCoastal = new bool[3];
		isBig = new bool[3];
		isBig[0] = true;
		isBig[1] = false;
		isBig[2] = false;

		// Przechowują stringi z danymi regionów.
		string[] lines = new string[3];
		//

		// Wyłuskuje nazwę prowicji.
		provinceName = dataLine.Substring(0, dataLine.IndexOf('.'));
		//

		// Wyłuskuje stringi regionów.
		int justAfterDotPos = dataLine.IndexOf('.') + 1;
		int nextDotPos;
		for (int i = 0; i < 3; i++)
		{
			nextDotPos = dataLine.IndexOf('.', justAfterDotPos);
			lines[i] = dataLine.Substring(justAfterDotPos, nextDotPos - justAfterDotPos);
			justAfterDotPos = nextDotPos + 1;
		}
		//
		
		// Przetwarza informacje dla regionów.
		for (int i = 0; i < 3; i++)
		{
			int justAfterCommaPos = 1;
			int nextCommaPos;

			// Wyłuskuje nazwę regionu.
			nextCommaPos = lines[i].IndexOf(',', justAfterCommaPos);
			regionNames[i] = lines[i].Substring(0, lines[i].IndexOf(','));
			justAfterCommaPos = nextCommaPos + 1;
			//

			// Wyłuskuje info czy ma port.
			nextCommaPos = lines[i].IndexOf(',', justAfterCommaPos);
			isCoastal[i] = Convert.ToBoolean(lines[i].Substring(justAfterCommaPos, nextCommaPos - justAfterCommaPos));
			justAfterCommaPos = nextCommaPos + 1;
			//

			// Wyłuskuje typ zasobu.
			nextCommaPos = lines[i].IndexOf(',', justAfterCommaPos);
			Enum.TryParse(lines[i].Substring(justAfterCommaPos, nextCommaPos - justAfterCommaPos), out resources[i]);
			justAfterCommaPos = nextCommaPos + 1;
			//
		}
		//
	}
}