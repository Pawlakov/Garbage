using System;
/// <summary>
/// Static class containing informations about difficulty level and their bonuses.
/// </summary>
static class Difficulty
{
	private static DifficultyLevel level;
	//
	static Difficulty()
	{
		level = DifficultyLevel.NORMAL;
	}
	/// <summary>
	/// Shows list of possible difficulty levels with their numeric codes.
	/// </summary>
	public static void ShowLevels()
	{
		Console.WriteLine("1. Easy");
		Console.WriteLine("2. Normal");
		Console.WriteLine("3. Hard");
		Console.WriteLine("4. Very hard");
		Console.WriteLine("5. Legendary");
	}
	/// <summary>
	/// Public order bonus provided by currently chosen difficulty level.
	/// </summary>
	public static byte PublicOrderBonus
	{
		get
		{
			switch (level)
			{
				case DifficultyLevel.EASY:
					return 14;
				case DifficultyLevel.NORMAL:
					return 10;
				case DifficultyLevel.HARD:
					return 8;
				case DifficultyLevel.VERY_HARD:
					return 7;
				case DifficultyLevel.LEGENDARY:
					return 6;
				default:
					return 255;
			}
		}
	}
	/// <summary>
	/// Gets or sets currently chosen level of difficulty.
	/// </summary>
	public static DifficultyLevel Level
	{
		get
		{
			return level;
		}
		set
		{
			level = value;
		}
	}
}