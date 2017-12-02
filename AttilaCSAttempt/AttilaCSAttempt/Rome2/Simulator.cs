namespace TWAssistant
{
	namespace Rome2
	{
		enum Resource { AMBER, PURPLE_DYE, GLASS, GOLD, GRAIN, HORSE, IRON, LEAD, LEATHER, LUMBER, MARBLE, OLIVE, SILK, WINE, NONE };
		enum BuildingType { TOWN, CENTERTOWN, CITY, CENTERCITY, COAST, RESOURCE };
		enum BonusCategory { ALL, AGRICULTURE, CULTURE, INDUSTRY, LOCAL_COMMERCE, MARITIME_COMMERCE, SUBSISTENCE };
		class R2_Simulator : Simulator
		{
			public static int ResourceTypesCount
			{
				get { return 15; }
			}
			public static int BuildingTypesCount
			{
				get { return 6; }
			}
			public static int BonusCategoriesCount
			{
				get { return 7; }
			}
			//
			private ProvinceCombination template;
			//
			public void Act()
			{
				System.Console.WriteLine("Let's begin.");
				//
				System.Console.WriteLine("Loading map.");
				map = new Map("map.xml");
				System.Console.WriteLine("Map loadaded. List of provinces:");
				map.ShowList();
				System.Console.WriteLine("Pick province.");
				province = map[System.Convert.ToByte(System.Console.ReadLine())];
				System.Console.WriteLine("You picked: {0}", province.Name);
				//
				System.Console.WriteLine("Loading factions.");
				factions = new FactionsList("factions.xml");
				System.Console.WriteLine("Factions loadaded. List of factions:");
				factions.ShowList();
				System.Console.WriteLine("Pick faction.");
				faction = factions[System.Convert.ToByte(System.Console.ReadLine())];
				System.Console.WriteLine("You picked: {0}", faction.Name);
				//
				template = new ProvinceCombination(province, faction);
				System.Console.WriteLine("Generated template of slots.");
				ForceBuildings(template);
				System.Console.WriteLine("Constraints set.");
				//
				System.Console.WriteLine("Choose minimal public order.");
				minimalOrder = System.Convert.ToSByte(System.Console.ReadLine());
				//
				System.Console.WriteLine("Choose minimal food (if doesn't matter then type any number).");
				minimalFood = System.Convert.ToSByte(System.Console.ReadLine());
				//
				System.Console.WriteLine("Choose x in size of a round, which is 2^x.");
				powerRoundSize = System.Convert.ToSByte(System.Console.ReadLine());
				//
				System.Console.WriteLine("Choose x in size of a biggest list, which is 2^x.");
				powerMaxList = System.Convert.ToSByte(System.Console.ReadLine());
				//
				System.Console.WriteLine("Choose x in size of a smallest list, which is 2^x.");
				powerMinList = System.Convert.ToSByte(System.Console.ReadLine());
				//
				System.Console.WriteLine("Which parameter do you want to maximize?");
				System.Console.WriteLine("0. Wealth (any food)");
				System.Console.WriteLine("1. Wealth (non-negative food)");
				System.Console.WriteLine("2. Food");
				System.Console.WriteLine(">2. Quit");
				switch (System.Convert.ToByte(System.Console.ReadLine()))
				{
					case 0:
						Generate(new CombinationsComparator<ProvinceCombination>(BetterInWealth), (ProvinceCombination subject) => subject.Order >= minimalOrder);
						break;
					case 1:
						Generate(new CombinationsComparator<ProvinceCombination>(BetterInWealth), (ProvinceCombination subject) => (subject.Order >= minimalOrder && subject.Food >= minimalFood));
						break;
					case 2:
						Generate(new CombinationsComparator<ProvinceCombination>(BetterInFood), (ProvinceCombination subject) => subject.Order >= minimalOrder);
						break;
					default:
						break;
				}
			}
			public int BetterInWealth(ProvinceCombination left, ProvinceCombination right)
			{
				return (int)(left.Wealth - right.Wealth);
			}
			public int BetterInFood(ProvinceCombination left, ProvinceCombination right)
			{
				if (left.Food == right.Food)
				{
					return (int)(left.Wealth - right.Wealth);
				}
				else
					return (int)(left.Food - right.Food);
			}
			public void ForceBuildings(ProvinceCombination template)
			{
				System.Console.WriteLine("Now you can place some building constraints by yourself.");
				while (true)
				{
					template.ShowContent();
					System.Console.WriteLine("What would you like to do now?");
					System.Console.WriteLine("0. Finish placing constraints.");
					System.Console.WriteLine("1. Create new constraint.");
					if (System.Convert.ToByte(System.Console.ReadLine()) == 0)
						break;
					else
					{
						template.ForceConstraint();
					}
				}
			}
			public void Generate(System.Collections.Generic.IComparer<ProvinceCombination> comparer, System.Func<ProvinceCombination, bool> minimalCondition)
			{
				int roundSize = (int)System.Math.Pow(2, powerRoundSize);
				int firstListSize = (int)System.Math.Pow(2, powerMaxList);
				int lastListSize = (int)System.Math.Pow(2, powerMinList);
				System.Collections.Generic.SortedSet<ProvinceCombination> valid = new System.Collections.Generic.SortedSet<ProvinceCombination>(comparer);
				ProvinceCombination bestValid = null;
				int currentCapacity = firstListSize;
				uint doneRounds = 0;
				uint doneCombinations = 0;
				uint doneValid = 0;
				bool test = true;
				System.Console.Clear();
				while (true)
				{
					while (doneValid % roundSize != 0 || test == true)
					{
						ProvinceCombination subject = new ProvinceCombination(template);
						subject.Fill();
						if (minimalCondition(subject))
						{
							valid.Add(subject);
							{
								if (valid.Count > currentCapacity)
									valid.Remove(valid.Min);
							}
							doneValid++;
							test = false;
						}
						doneCombinations++;
						System.Console.WriteLine("Rounds: {0} | Combinations: {1} | Valid C.: {2}/{3} | Best List: {4}/{5}", doneRounds, doneCombinations, doneValid, roundSize, valid.Count, currentCapacity);
						System.Console.CursorTop -= 1;
					}
					bestValid = valid.Max;
					foreach (ProvinceCombination combination in valid)
					{
						combination.RewardUsefulBuildings();
					}
					bestValid.CurbUselessBuildings();
					System.Console.Clear();
					System.Console.WriteLine("Best after last round: ");
					bestValid.ShowContent();
					if (currentCapacity > lastListSize)
					{
						currentCapacity = (int)(currentCapacity / System.Math.Sqrt(2));
						for (int whichCombination = 0; whichCombination < currentCapacity; whichCombination++)
						{
							valid.Remove(valid.Min);
						}
					}
					doneCombinations = 0;
					doneValid = 0;
					doneRounds++;
					test = true;
				}
			}
		}
		class CombinationsComparator<T> : System.Collections.Generic.IComparer<T> where T : ProvinceCombination
		{
			private System.Comparison<T> comparison;
			public CombinationsComparator(System.Comparison<T> comparison)
			{
				this.comparison = comparison;
			}
			public int Compare(T x, T y)
			{
				return comparison(x, y);
			}
		}
		abstract class Simulator
		{
			protected Map map;
			protected ProvinceData province;
			protected FactionsList factions;
			protected Faction faction;
			//
			protected sbyte powerRoundSize;
			protected sbyte powerMaxList;
			protected sbyte powerMinList;
			//
			protected sbyte minimalOrder;
			protected sbyte minimalFood;
		}
	}
}