using System;

// object zawiera jeden konkret bonus danego budynku
class WealthBonus
{
	public BonusCategory category;
	public bool isMultiplier;
	public bool affectsWholeProvince;
	public decimal number;

	public WealthBonus(string line)
	{

		int justAfterDotPos = 0;
		int nextDotPos;

		// Wyłuskuje typ bonusu.
		nextDotPos = line.IndexOf('.', justAfterDotPos);
		Enum.TryParse(line.Substring(justAfterDotPos, nextDotPos - justAfterDotPos), out category);
		justAfterDotPos = nextDotPos + 1;
		//

		// Wyłuskuje info czy bonus jest mnożnikiem (jeśli nie, to jest kwotą).
		nextDotPos = line.IndexOf('.', justAfterDotPos);
		isMultiplier = Convert.ToBoolean(line.Substring(justAfterDotPos, nextDotPos - justAfterDotPos));
		justAfterDotPos = nextDotPos + 1;
		//

		// Wyłuskuje info czy bonus dotyczy całej prowincji (jeśli nie, to dotyczy tylko regionu).
		nextDotPos = line.IndexOf('.', justAfterDotPos);
		affectsWholeProvince = Convert.ToBoolean(line.Substring(justAfterDotPos, nextDotPos - justAfterDotPos));
		justAfterDotPos = nextDotPos + 1;
		//

		// Wyłuskuje liczbę bonusu.
		nextDotPos = line.IndexOf('.', justAfterDotPos);
		number = Convert.ToDecimal(line.Substring(justAfterDotPos, nextDotPos - justAfterDotPos));
		//
	}
}