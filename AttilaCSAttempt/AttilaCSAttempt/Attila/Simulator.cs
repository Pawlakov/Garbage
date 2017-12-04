using System;
using System.Collections.Generic;
namespace TWAssistant
{
	namespace Attila
	{
		enum Resource { NONE, IRON, LEAD, GEMSTONES, OLIVE, FUR, WINE, SILK, MARBLE, SALT, GOLD, DYE, LUMBER };
		enum BuildingType { TOWN, CENTERTOWN, CITY, CENTERCITY, COAST, RESOURCE };
		enum BonusCategory { ALL, AGRICULTURE, HUSBANDRY, CULTURE, INDUSTRY, COMMERCE, MARITIME_COMMERCE, SUBSISTENCE, MAINTENANCE }; // Maintenance HAS TO BE THE LAST ONE
		class Simulator
		{
			private Map map;
			private ProvinceData province;
			private FactionsList factions;
			private Faction faction;
			private ProvinceCombination template;
			//
			private uint roundSize;
			private uint maxListSize;
			private double reductionRate;
			//
			private int minimalFood;
			private int minimalOrder;
			private int minimalSanitation;
			private uint fertility;
			//
			public static int ResourceTypesCount
			{
				get { return 13; }
			}
			public static int BuildingTypesCount
			{
				get { return 6; }
			}
			public static int BonusCategoriesCount
			{
				get { return 9; }
			}
			//
			public void Act()
			{
				Console.Clear();
				Console.WriteLine("Loading map.");
				map = new Map("twa_map.xml");
				Console.WriteLine("Map loadaded. List of provinces:");
				map.ShowList();
				Console.Write("Pick a province: ");
				province = map[Convert.ToUInt32(Console.ReadLine())];
				//
				Console.Clear();
				Console.WriteLine("Loading factions.");
				factions = new FactionsList("twa_factions.xml");
				Console.WriteLine("Factions loadaded. List of factions:");
				factions.ShowList();
				Console.Write("Pick a faction: ");
				faction = factions[Convert.ToInt32(Console.ReadLine())];
				//
				Console.Clear();
				Console.Write("Your province's default fertility is {0}. Choose fertility: ", province.Fertility);
				fertility = Convert.ToUInt32(Console.ReadLine());
				Console.Write("Choose minimal food: ");
				minimalFood = Convert.ToInt32(Console.ReadLine());
				Console.Write("Choose minimal public order: ");
				minimalOrder = Convert.ToInt32(Console.ReadLine());
				Console.Write("Choose minimal sanitation: ");
				minimalSanitation = Convert.ToInt32(Console.ReadLine());
				//
				template = new ProvinceCombination(province, faction, fertility);
				//System.Console.WriteLine("Generated template of slots.");
				//ForceBuildings(template);
				//System.Console.WriteLine("Constraints set
				//
				Console.Clear();
				Console.Write("Choose round size: ");
				roundSize = Convert.ToUInt32(Console.ReadLine());
				Console.Write("Choose biggest list size: ");
				maxListSize = Convert.ToUInt32(Console.ReadLine());
				Console.Write("Choose reduction rate per round: ");
				reductionRate = Convert.ToDouble(Console.ReadLine());
				Generate(MinimalCondition);
			}
			public int BetterInWealth(ProvinceCombination left, ProvinceCombination right)
			{
				if (left.Wealth > right.Wealth)
					return 1;
				else if (left.Wealth < right.Wealth)
					return -1;
				else
					return 0;
			}
			//public void ForceBuildings(ProvinceCombination template)
			//{
			//	System.Console.WriteLine("Now you can place some building constraints by yourself.");
			//	while (true)
			//	{
			//		template.ShowContent();
			//		System.Console.WriteLine("What would you like to do now?");
			//		System.Console.WriteLine("0. Finish placing constraints.");
			//		System.Console.WriteLine("1. Create new constraint.");
			//		if (System.Convert.ToByte(System.Console.ReadLine()) == 0)
			//			break;
			//		else
			//		{
			//			template.ForceConstraint();
			//		}
			//	}
			//}
			public void Generate(Func<ProvinceCombination, bool> minimalCondition)
			{
				Random random = new Random();
				SortedSet<ProvinceCombination> valid = new SortedSet<ProvinceCombination>(new CombinationsComparator(BetterInWealth));
				ProvinceCombination bestValid = null;
				uint currentCapacity = maxListSize;
				uint doneRounds = 0;
				uint doneCombinations = 0;
				uint doneValid = 0;
				Console.Clear();
				while (currentCapacity > 1)
				{
					do
					{
						while (true)
						{
							ProvinceCombination subject = new ProvinceCombination(template);
							++doneCombinations;
							subject.Fill(random);
							if (minimalCondition(subject))
							{
								valid.Add(subject);
								if (valid.Count > currentCapacity)
									valid.Remove(valid.Min);
								++doneValid;
								break;
							}
						}
						Console.WriteLine("Rounds: {0} | Combinations per Valid: {1} | Valid Found: {2}/{3} | Current List: {4}/{5}", doneRounds, doneCombinations/doneValid, doneValid, roundSize, valid.Count, currentCapacity);
						Console.CursorTop -= 1;
					} while (doneValid % roundSize != 0);
					bestValid = valid.Max;
					//
					foreach (ProvinceCombination combination in valid)
					{
						combination.RewardUsefulBuildings();
					}
					faction.CurbUselessBuildings();
					Console.Clear();
					Console.WriteLine("COAST building left: {0}", faction.Buildings.GetCountByType(BuildingType.COAST));
					Console.WriteLine("CITY building left: {0}", faction.Buildings.GetCountByType(BuildingType.CITY));
					Console.WriteLine("TOWN building left: {0}", faction.Buildings.GetCountByType(BuildingType.TOWN));
					//
					Console.WriteLine("Best so far: ");
					bestValid.ShowContent();
					//
					if (currentCapacity > 1)
					{
						currentCapacity = (uint)(currentCapacity * reductionRate);
						while (valid.Count > currentCapacity)
							valid.Remove(valid.Min);
					}
					doneCombinations = 0;
					doneValid = 0;
					++doneRounds;
				}
				Console.Clear();
				Console.WriteLine("AND THE WINNER IS...");
				bestValid.ShowContent();
				Console.ReadKey();
			}
			public bool MinimalCondition(ProvinceCombination subject)
			{
				return (subject.Order >= minimalOrder && subject.Food >= minimalFood && subject.getSanitation(0) >= minimalSanitation && subject.getSanitation(1) >= minimalSanitation && subject.getSanitation(2) >= minimalSanitation);
			}
		}
		class CombinationsComparator : IComparer<ProvinceCombination>
		{
			private Comparison<ProvinceCombination> comparison;
			public CombinationsComparator(Comparison<ProvinceCombination> comparison)
			{
				this.comparison = comparison;
			}
			public int Compare(ProvinceCombination x, ProvinceCombination y)
			{
				return comparison(x, y);
			}
		}
	}
}