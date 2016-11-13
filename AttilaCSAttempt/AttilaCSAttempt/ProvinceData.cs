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
		for (int whichRegion = 0; whichRegion < 3; whichRegion++)
		{
			nextDotPos = dataLine.IndexOf('.', justAfterDotPos);
			lines[whichRegion] = dataLine.Substring(justAfterDotPos, nextDotPos - justAfterDotPos);
			justAfterDotPos = nextDotPos + 1;
		}
		//

		// Przetwarza informacje dla regionów.
		for (int whichRegion = 0; whichRegion < 3; whichRegion++)
		{
			int justAfterCommaPos = 1;
			int nextCommaPos;

			// Wyłuskuje nazwę regionu.
			nextCommaPos = lines[whichRegion].IndexOf(',', justAfterCommaPos);
			regionNames[whichRegion] = lines[whichRegion].Substring(0, lines[whichRegion].IndexOf(','));
			justAfterCommaPos = nextCommaPos + 1;
			//

			// Wyłuskuje info czy ma port.
			nextCommaPos = lines[whichRegion].IndexOf(',', justAfterCommaPos);
			isCoastal[whichRegion] = Convert.ToBoolean(lines[whichRegion].Substring(justAfterCommaPos, nextCommaPos - justAfterCommaPos));
			justAfterCommaPos = nextCommaPos + 1;
			//

			// Wyłuskuje typ zasobu.
			nextCommaPos = lines[whichRegion].IndexOf(',', justAfterCommaPos);
			Enum.TryParse(lines[whichRegion].Substring(justAfterCommaPos, nextCommaPos - justAfterCommaPos), out resources[whichRegion]);
			//
		}
		//
	}
}