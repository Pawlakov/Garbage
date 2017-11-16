using System;
static class Difficulty
{
	private static DifficultyLevel level;
	//
	static Difficulty()
	{
		level = DifficultyLevel.NORMAL;
	}
	//
	public static void ShowLevels()
	{
		Console.WriteLine("0. Easy");
		Console.WriteLine("1. Normal");
		Console.WriteLine("2. Hard");
		Console.WriteLine("3. Very hard");
		Console.WriteLine("4. Legendary");
	}
	//
	public static short PublicOrderBonus
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