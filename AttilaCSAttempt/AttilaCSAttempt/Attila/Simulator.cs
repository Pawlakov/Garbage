using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace TWAssistant
{
	namespace Attila
	{
		public enum Resource { NONE, IRON, LEAD, GEMSTONES, OLIVE, FUR, WINE, SILK, MARBLE, SALT, GOLD, DYE, LUMBER };
		public enum BuildingType { TOWN, CENTERTOWN, CITY, CENTERCITY, COAST, RESOURCE };
		public enum BonusCategory { ALL, AGRICULTURE, HUSBANDRY, CULTURE, INDUSTRY, COMMERCE, MARITIME_COMMERCE, SUBSISTENCE, MAINTENANCE }; // Maintenance HAS TO BE THE LAST ONE
		public enum Religion { LACHRI, GRECHRI, ARICHRI, EACHRI, ROMPAG, CELPAG, GERPAG, SEMPAG, MANICH, ZORO, TENGRI, JUDA, MINO };
		public class Simulator
		{
			//int whichMod;
			Map map;
			ProvinceData province;
			Religion religion;
			bool useLegacyTechs;
			FactionsList factions;
			Faction faction;
			ProvinceCombination template;
			//
			int firstRoundTime;
			int roundSize;
			int maxListSize;
			double reductionRate;
			int parallelCount;
			object parallelLock = new object();
			//
			int minimalOrder;
			int minimalSanitation;
			int fertility;
			int osmosis;
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
			public static int ReligionTypesCount
			{
				get { return 13; }
			}
			//
			public void Act()
			{
				/*
				Console.Clear();
				Console.Write("0-Vanilla/1-Radious/2-Modpack: ");
				whichMod = Convert.ToInt32(Console.ReadLine());
				*/
				//
				Console.Clear();
				map = new Map("XMLs/twa_map.xml", 2);
				Console.WriteLine("List of provinces:");
				map.ShowList();
				Console.Write("Pick a province: ");
				province = map[Convert.ToUInt32(Console.ReadLine())];
				//
				Console.Clear();
				Console.WriteLine("Avaible religions: ");
				for (int whichReligion = 0; whichReligion < ReligionTypesCount; ++whichReligion)
					Console.WriteLine("{0}. {1}", whichReligion, (Religion)whichReligion);
				Console.Write("Pick a religion: ");
				religion = (Religion)Convert.ToInt32(Console.ReadLine());
				Console.Write("Use legacy techs? (if possible) true/false: ");
				useLegacyTechs = Convert.ToBoolean(Console.ReadLine());
				//
				Console.Clear();
				/*
				switch (whichMod)
				{
					case 0:
						factions = new FactionsList("XMLs/Vanilla/twa_factions.xml", religion, useLegacyTechs);
						break;
					case 1:
						factions = new FactionsList("XMLs/Radious/twa_rm_factions.xml", religion, useLegacyTechs);
						break;
					case 2:
						factions = new FactionsList("XMLs/ModPack/twa_mpack_factions.xml", religion, useLegacyTechs);
						break;
				}
				*/
				try
				{
					factions = new FactionsList("XMLs/ModPack/twa_mpack_factions.xml", religion, useLegacyTechs);
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception.Message);
					Console.WriteLine(exception.StackTrace);
					Console.ReadKey();
				}
				//
				Console.WriteLine("List of factions:");
				factions.ShowList();
				Console.Write("Pick a faction: ");
				faction = factions[Convert.ToInt32(Console.ReadLine())];

				//
				Console.Clear();
				Console.WriteLine("Default fertility in {0} is {1}", province.Name, province.Fertility);
				Console.Write("Choose fertility: ");
				fertility = Convert.ToInt32(Console.ReadLine());
				Console.Write("Choose incoming osmosis: ");
				osmosis = Convert.ToInt32(Console.ReadLine());
				Console.Write("Choose minimal public order: ");
				minimalOrder = Convert.ToInt32(Console.ReadLine());
				Console.Write("Choose minimal sanitation: ");
				minimalSanitation = Convert.ToInt32(Console.ReadLine());
				//
				template = new ProvinceCombination(province, faction, fertility, osmosis, 2, religion);
				//ForceBuildings(template);
				//
				Console.Clear();
				Console.Write("Choose first round time: ");
				firstRoundTime = 1000 * Convert.ToInt32(Console.ReadLine());
				Console.Write("Choose biggest list size: ");
				maxListSize = Convert.ToInt32(Console.ReadLine());
				Console.Write("Choose reduction rate per round: ");
				reductionRate = Convert.ToDouble(Console.ReadLine());
				Console.Write("Choose parallel lists count: ");
				parallelCount = Convert.ToInt32(Console.ReadLine());
				Generate(MinimalCondition);
			}
			public int BetterInWealth(ProvinceCombination left, ProvinceCombination right)
			{
				if (left.Wealth > right.Wealth)
					return 1;
				if (left.Wealth < right.Wealth)
					return -1;
				return 0;
			}
			/*public void ForceBuildings(ProvinceCombination template)
			{
				while (true)
				{
					Console.Clear();
					template.ShowContent();
					Console.Write("Force building/level? (0-No/1-Yes): ");
					if (Convert.ToInt32(Console.ReadLine()) == 0)
						break;
					template.ForceBuilding();
				}
			}*/
			public void Generate(Func<ProvinceCombination, bool> minimalCondition)
			{
				SortedSet<ProvinceCombination> valid = new SortedSet<ProvinceCombination>(new CombinationsComparator(BetterInWealth));
				ProvinceCombination bestValid = null;
				int capacity = maxListSize;
				int doneRounds = 0;
				int cursorPosition = 0;
				//
				Console.Clear();
				while (capacity > 1)
				{
					SortedSet<ProvinceCombination>[] results = new SortedSet<ProvinceCombination>[parallelCount];
					Parallel.For(0, parallelCount, (int whichResult) => results[whichResult] = Round(minimalCondition, capacity, (cursorPosition + whichResult)));
					for (int whichResult = 0; whichResult < parallelCount; ++whichResult)
						valid.UnionWith(results[whichResult]);
					while (valid.Count > capacity)
						valid.Remove(valid.Min);
					bestValid = valid.Max;
					foreach (ProvinceCombination combination in valid)
						combination.RewardBuildings();
					faction.EvaluateBuildings();
					Console.Clear();
					Console.WriteLine("COAST building left: {0}", faction.Buildings.GetCountByType(BuildingType.COAST));
					Console.WriteLine("CITY building left: {0}", faction.Buildings.GetCountByType(BuildingType.CITY));
					Console.WriteLine("TOWN building left: {0}", faction.Buildings.GetCountByType(BuildingType.TOWN));
					//
					Console.WriteLine("Best so far: ");
					bestValid.ShowContent();
					cursorPosition = Console.CursorTop;
					Console.WriteLine("Round: {0} Capacity: {1}", doneRounds, capacity);
					//
					if (capacity > 1)
					{
						capacity = (int)(capacity * reductionRate);
						while (valid.Count > capacity)
							valid.Remove(valid.Min);
					}
					++doneRounds;
				}
				Console.Clear();
				Console.WriteLine("AND THE WINNER IS...");
				bestValid.ShowContent();
				Console.ReadKey();
			}
			SortedSet<ProvinceCombination> Round(Func<ProvinceCombination, bool> minimalCondition, int capacity, int cursorLine)
			{
				SortedSet<ProvinceCombination> result = new SortedSet<ProvinceCombination>(new CombinationsComparator(BetterInWealth));
				Random random = new Random(cursorLine + minimalOrder + capacity);
				Stopwatch stopwatch = new Stopwatch();
				int doneCombinations = 0;
				int doneValid = 0;
				string report;
				//
				if (roundSize > 0)
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
								result.Add(subject);
								if (result.Count > capacity)
									result.Remove(result.Min);
								++doneValid;
								break;
							}
						}
						report = string.Format("Combinations per Valid: {0} | Valid Found: {1}/{2} | Current List: {3}/{4}----", doneCombinations / doneValid, doneValid, roundSize, result.Count, capacity);
						lock (parallelLock)
						{
							Console.SetCursorPosition(0, cursorLine);
							Console.Write(report);
						}
					} while (doneValid % roundSize != 0);
				}
				else
				{
					stopwatch.Start();
					do
					{
						while (true)
						{
							ProvinceCombination subject = new ProvinceCombination(template);
							++doneCombinations;
							subject.Fill(random);
							if (minimalCondition(subject))
							{
								result.Add(subject);
								if (result.Count > capacity)
									result.Remove(result.Min);
								++doneValid;
								break;
							}
						}
						report = string.Format("Combinations per Valid: {0} | Valid Found: {1}/{2} | Current List: {3}/{4}----", doneCombinations / doneValid, doneValid, roundSize, result.Count, capacity);
						lock (parallelLock)
						{
							Console.SetCursorPosition(0, cursorLine);
							Console.Write(report);
						}
					} while (stopwatch.ElapsedMilliseconds < firstRoundTime);
					stopwatch.Stop();
					roundSize = doneValid;
					capacity = result.Count;
				}
				//
				return result;
			}
			public bool MinimalCondition(ProvinceCombination subject)
			{
				return (subject.Order >= minimalOrder
						&& subject.Food >= 0
						&& subject.getSanitation(0) >= minimalSanitation
						&& subject.getSanitation(1) >= minimalSanitation
						&& subject.getSanitation(2) >= minimalSanitation);
			}
		}
		class CombinationsComparator : IComparer<ProvinceCombination>
		{
			readonly Comparison<ProvinceCombination> comparison;
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